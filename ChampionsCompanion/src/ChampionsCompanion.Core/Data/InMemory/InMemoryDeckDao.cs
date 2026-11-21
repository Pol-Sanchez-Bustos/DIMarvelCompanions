using ChampionsCompanion.Core.Model;

namespace ChampionsCompanion.Core.Data.InMemory;

/// <summary>Phase 1 implementation of <see cref="IDeckDao"/>, over <see cref="InMemoryDataStore"/>.</summary>
public class InMemoryDeckDao : IDeckDao
{
    private readonly InMemoryDataStore _store;

    public InMemoryDeckDao() : this(InMemoryDataStore.Instance) { }

    public InMemoryDeckDao(InMemoryDataStore store) => _store = store;

    // TODO: implement the methods of IDeckDao over _store.Decks.
    public List<Deck> getAll()
    {
        return _store.Decks.ToList();
    }

    public List<Deck> GetAllFiltrats(String nom = "", int hero_id = -1, int aspect_id = -1, int compl = -1)
    {
        List<Deck> filtrats = new List<Deck>();

        foreach (Deck deck in _store.Decks.ToList())
        {
            bool comp = true;

            if (deck.Name.CompareTo(nom) != 0 && nom != "")
            {
                comp = false;
            }

            if (deck.HeroId != hero_id && hero_id != -1)
            {
                comp = false;
            }

            if (deck.AspectId != aspect_id && aspect_id != -1)
            {
                comp = false;
            }


            if (compl != 0)
            {
                if(compl == -1 && deck.IsCardCountValid)
                {
                    comp = false;
                }
                if (compl == 1 && !deck.IsCardCountValid)
                {
                    comp = false;
                }


            }



            if (comp == true)
            {
                filtrats.Add(deck);
            }

        }


        return filtrats;
    }

    public void Add(Deck deck)
    {
        deck.Id = _store.NextDeckId();

        _store.Decks.Add(deck);

        _store.Link();
    }

    public void Update(Deck deck)
    {
        Deck deck_memoria = _store.Decks.FirstOrDefault(c => c.Id == deck.Id);

        if (deck_memoria != null)
        {
            // Actualizar las propiedades
            deck_memoria = deck;

            _store.Link();
        }
    }
}
