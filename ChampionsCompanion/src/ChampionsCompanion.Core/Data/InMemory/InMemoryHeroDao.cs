using ChampionsCompanion.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsCompanion.Core.Data.InMemory
{
    public class InMemoryHeroDao : IHeroDao
    {
        private readonly InMemoryDataStore _store;

        public InMemoryHeroDao() : this(InMemoryDataStore.Instance) { }

        public InMemoryHeroDao(InMemoryDataStore store) => _store = store;

        // TODO: implement the methods of IDeckDao over _store.Decks.
        public List<Hero> getAll()
        {
            return _store.Heroes.ToList();
        }
    }
}
