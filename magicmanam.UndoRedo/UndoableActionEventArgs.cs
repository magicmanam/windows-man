using System;

namespace magicmanam.UndoRedo
{
    public class UndoableActionEventArgs : EventArgs
    {
        public UndoableActionEventArgs(string action)
        {
            Action = action;
        }

        public string Action { get; private set; }
    }
}
