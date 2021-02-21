using magicmanam.UndoRedo.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace magicmanam.UndoRedo
{
    public class UndoableContext<T> : IUndoableContext<T> where T : class
    {
        public event EventHandler<UndoableActionEventArgs> UndoableAction;
        public event EventHandler<UndoableActionEventArgs> UndoAction;
        public event EventHandler<UndoableActionEventArgs> RedoAction;
        public event EventHandler<UndoableContextChangedEventArgs> StateChanged;

        private readonly Stack<T> _states = new Stack<T>();
        private readonly Stack<T> _undoableStates = new Stack<T>();
        private readonly Stack<UndoableAction<T>> _actionsStack = new Stack<UndoableAction<T>>();
        private IStatefulComponent<T> _stateKeeper;

        public UndoableContext(IStatefulComponent<T> stateKeeper)
        {
            this._stateKeeper = stateKeeper;
        }

        public static IUndoableContext<T> Current { get; set; }

        public UndoableAction<T> StartAction(string actionName = "")
        {
            var action = new UndoableAction<T>(actionName, this._stateKeeper.UndoableState, this._actionsStack.Any());
            action.ActionEnd += OnActionEnd;

            this._actionsStack.Push(action);

            return action;
        }

        private void OnActionEnd(object sender, UndoableActionEndEventArgs<T> e)
        {
            var action = this._actionsStack.Pop();

            if (action.ActionId != e.Action.ActionId)
            {
                throw new InvalidOperationException("Nested actions must be disposed in stack order.");
            }

            if (action.IsCancelled)
            {
                this._stateKeeper.UndoableState = action.StateOnStart;
            }

            if (!this.IsOperationInProgress)
            {
                this._states.Push(action.StateOnStart);
                this.OnUndoableAction(action.Name);
            }
        }

        protected virtual void OnUndoableAction(string actionName)
        {
            this._undoableStates.Clear();
            this.UndoableAction?.Invoke(this, new UndoableActionEventArgs(actionName));
            this.StateChanged?.Invoke(this, new UndoableContextChangedEventArgs(true, false));
        }

        public bool IsOperationInProgress
        {
            get
            {
                return this._actionsStack.Any();
            }
        }

        public void Undo()
        {
            if (this.IsOperationInProgress)
            {
                throw new InvalidOperationException($"Operation is in progress. Check {nameof(IsOperationInProgress)} property before calling this method");
            }

            if (this._states.Count > 0)
            {
                this._undoableStates.Push(this._stateKeeper.UndoableState);

                var lastState = this._states.Pop();
                this._stateKeeper.UndoableState = lastState;
                this.UndoAction?.Invoke(this, new UndoableActionEventArgs(Resource.BuferOperationCancelled));
                this.StateChanged?.Invoke(this, new UndoableContextChangedEventArgs(this._states.Any(), true));
            }
        }
        public void Redo()
        {
            if (this.IsOperationInProgress)
            {
                throw new InvalidOperationException($"Operation is in progress. Check {nameof(IsOperationInProgress)} property before calling this method");
            }

            if (this._undoableStates.Count > 0)
            {
                this._states.Push(this._stateKeeper.UndoableState);

                var undoState = this._undoableStates.Pop();
                this._stateKeeper.UndoableState = undoState;
                this.RedoAction?.Invoke(this, new UndoableActionEventArgs(Resource.BuferOperationRestored));
                this.StateChanged?.Invoke(this, new UndoableContextChangedEventArgs(true, this._undoableStates.Any()));
            }
        }
    }
}
