using System.Threading;

namespace DontBase.Shareds.Sequences
{
    public class Sequence : ISequence
    {
        private readonly ISequenceFactory _sequenceFactory;

        private long _id;
        public string Name { get; }
        public long CurrentId => _id;

        public Sequence(string name, long currentId) : this(name, currentId, null) { }

        internal Sequence(string name, long currentId, ISequenceFactory sequenceFactory)
        {
            Name = name;
            _id = currentId;
            _sequenceFactory = sequenceFactory;
        }

        public long Next()
        {
            var id = Interlocked.Increment(ref _id);
            _sequenceFactory.SequenceChanged(this);
            return id;
        }

        public override string ToString() => $"{Name}: {CurrentId}";
    }
}