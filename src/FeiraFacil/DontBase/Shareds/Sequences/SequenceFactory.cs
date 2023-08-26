using DontBase.Shareds.Apis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DontBase.Shareds.Sequences
{
    public class SequenceFactory : ISequenceFactory, IDisposable
    {
        private bool _changed = false;
        private readonly Timer _timer;
        private readonly ConcurrentDictionary<string, ISequence> _sequences = new();
        private readonly string _resource = "DontBase.Shareds.Sequences.SequenceDto";
        private readonly IDontPadApi _dontPadApi;

        public SequenceFactory(IServiceProvider serviceProvider)
        {
            _timer = new Timer(x => Save(false), null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            _dontPadApi = serviceProvider.GetRequiredService<IDontPadApi>();
            LoadAsync().Wait();
        }

        public void SequenceChanged(ISequence sequence) => _changed = true;

        public ISequence GetFor<T>()
        {
            var type = typeof(T).FullName;
            return GetFor($"{type}_Sequence", 0);
        }

        private ISequence GetFor(string name, long currentId)
        {
            if (!_sequences.TryGetValue(name, out var sequence))
            {
                sequence = new Sequence(name, currentId, this);
                _sequences.TryAdd(name, sequence);
                _changed = true;
            }
            return sequence;
        }

        public async Task LoadAsync()
        {
            var jsonString = await _dontPadApi.Get(_resource, 0);
            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                var sequences = JsonSerializer.Deserialize<Sequence[]>(jsonString);
                foreach (var sequence in sequences)
                    GetFor(sequence.Name, sequence.CurrentId);
            }
        }

        public void Save(bool force)
        {
            if (_changed || force)
            {
                _changed = false;
                SaveAsync().Wait();
            }
        }

        public async Task SaveAsync()
        {
            var sequences = _sequences.Values.OrderBy(x => x.Name);
            var jsonString = JsonSerializer.Serialize(sequences);
            await _dontPadApi.Post(_resource, jsonString);
        }

        protected virtual void Dispose(bool disposing)
        {
            _timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            _timer.Dispose();
            Save(true);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~SequenceFactory() => Dispose(disposing: false);
    }
}