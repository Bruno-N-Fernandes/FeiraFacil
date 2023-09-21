namespace DontBase.Shareds.Sequences
{
    public interface ISequence
    {
        string Name { get; }
        long CurrentId { get; }
        long Next();
    }
}