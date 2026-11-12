using ChampionsCompanion.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsCompanion.Core.Data.InMemory
{
    public class InMemoryPacksDao : IPackDao
    {
        private readonly InMemoryDataStore _store;

        public InMemoryPacksDao() : this(InMemoryDataStore.Instance) { }

        public InMemoryPacksDao(InMemoryDataStore store) => _store = store;

        // TODO: implement the methods of IDeckDao over _store.Decks.
        public List<Pack> getAll()
        {
            return _store.Packs.ToList();
        }
    }
}
