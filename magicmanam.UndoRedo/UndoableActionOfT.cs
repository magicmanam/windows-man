using System;

namespace magicmanam.UndoRedo
{
    public class UndoableAction<T> : IDisposable where T : class
    {
        public event EventHandler<UndoableActionEndEventArgs<T>> ActionEnd;

        internal UndoableAction(string name, T state, bool isNested)
        {
            this.Name = name;
            this.StateOnStart = state;
            this.ActionId = Guid.NewGuid();
            this.IsNested = isNested;
        }

        internal T StateOnStart { get; set; }

        public bool IsNested { get; set; }

        public string Name { get; private set; }

        public bool IsCancelled { get; private set; }

        public void Cancel() {
            this.IsCancelled = true;
        }

        public Guid ActionId { get; private set; }

        public void Dispose()
        {
            this.ActionEnd?.Invoke(this, new UndoableActionEndEventArgs<T>(this));
        }
    }
}
