using ChampionsCompanion.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsCompanion.Core.Data.InMemory
{
    public class InMemoryCardTypeDao : ICardTypeDao
    {
        private readonly InMemoryDataStore _store;

        public InMemoryCardTypeDao() : this(InMemoryDataStore.Instance) { }

        public InMemoryCardTypeDao(InMemoryDataStore store) => _store = store;

        // TODO: implement the methods of IDeckDao over _store.Decks.
        public List<CardType> getAll()
        {
            return _store.CardTypes.ToList();
        }


    }
}
