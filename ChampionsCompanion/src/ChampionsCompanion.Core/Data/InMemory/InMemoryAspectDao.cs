using ChampionsCompanion.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsCompanion.Core.Data.InMemory
{
    public class InMemoryAspectDao : IAspectDao
    {


        private readonly InMemoryDataStore _store;

        public InMemoryAspectDao() : this(InMemoryDataStore.Instance) { }

        public InMemoryAspectDao(InMemoryDataStore store) => _store = store;

        public List<Aspect> GetAll()
        {
            return _store.Aspects.ToList();
        }
    }
}
