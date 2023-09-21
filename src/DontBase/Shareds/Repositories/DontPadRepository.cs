using DontBase.Shareds.Apis;
using DontBase.Shareds.Sequences;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DontBase.Shareds.Repositories
{
    public abstract class DontPadRepository<TEntity> : IDontPadRepository<TEntity>, IDisposable where TEntity : IEntity, new()
    {
        private bool _changed = false;
        private readonly ISequence _sequence;
        private readonly IDontPadApi _dontPadApi;
        private readonly string _resource;
        private readonly List<TEntity> _entities;
        private readonly Timer _timer;

        protected DontPadRepository(IServiceProvider serviceProvider)
        {
            var sequenceFactory = serviceProvider.GetRequiredService<ISequenceFactory>();
            _sequence = sequenceFactory.GetFor<TEntity>();
            _dontPadApi = serviceProvider.GetRequiredService<IDontPadApi>();
            _resource = typeof(TEntity).FullName;
            _entities = new List<TEntity>();
            _timer = new Timer(x => Save(false), null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            LoadAsync().Wait();
        }

        public async Task<IEnumerable<TEntity>> ObterTodos()
        {
            await Task.CompletedTask;
            return _entities;
        }

        public async Task Incluir(TEntity entity)
        {
            entity.Id = _sequence.Next();
            _entities.Add(entity);
            _changed = true;
            await Task.CompletedTask;
        }

        public async Task Excluir(TEntity entity)
        {
            _entities.RemoveAll(x => x.Id == entity.Id);
            _changed = true;
            await Task.CompletedTask;
        }

        public async Task Alterar(TEntity entity)
        {
            _entities.RemoveAll(x => x.Id == entity.Id);
            _entities.Add(entity);
            _changed = true;
            await Task.CompletedTask;
        }

        public async Task LoadAsync()
        {
            var jsonString = await _dontPadApi.Get(_resource, 0);
            if (!string.IsNullOrWhiteSpace(jsonString))
                _entities.AddRange(JsonSerializer.Deserialize<TEntity[]>(jsonString));
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
            var jsonString = JsonSerializer.Serialize(_entities.OrderBy(x => x.Id));
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

        ~DontPadRepository() => Dispose(disposing: false);
    }
}