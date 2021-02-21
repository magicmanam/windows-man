using System;

namespace magicmanam.UndoRedo
{
    public class UndoableActionEndEventArgs<T> : EventArgs where T : class
    {
        internal UndoableActionEndEventArgs(UndoableAction<T> action)
        {
            this.Action = action;
        }

        public UndoableAction<T> Action { get; private set; }
    }
}
