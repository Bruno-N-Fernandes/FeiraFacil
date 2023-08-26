namespace DontBase.Shareds.Sequences
{
    public interface ISequenceFactory
    {
        ISequence GetFor<T>();
        void SequenceChanged(ISequence sequence);
    }
}