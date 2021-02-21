namespace magicmanam.UndoRedo
{
    public interface IStatefulComponent<T>
    {
        /// <summary>
        /// Copy of undoable component's state.
        /// </summary>
        T UndoableState { get; set; }
    }
}
