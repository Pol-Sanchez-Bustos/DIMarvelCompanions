using ChampionsCompanion.Core.Model;
using System.Windows;

namespace ChampionsCompanion.Core.Data.InMemory;

/// <summary>Phase 1 implementation of <see cref="ICardDao"/>, over <see cref="InMemoryDataStore"/>.</summary>
public class InMemoryCardDao : ICardDao
{
    private static readonly Func<Card, bool> IsPlayerCard = CardQueries.IsPlayerCard.Compile();

    private readonly InMemoryDataStore _store;

    public InMemoryCardDao() : this(InMemoryDataStore.Instance) { }

    public InMemoryCardDao(InMemoryDataStore store) => _store = store;

    public List<Card> GetAll()
    {
        return _store.Cards.Where(IsPlayerCard).OrderBy(c => c.Code).ToList();
    }

    public List<Card> GetAllFiltrats(String nom = "", int aspect_id = -1, int type_id = -1,int hero_id = -1, int pack_id = -1, String cost = "---")
    {
        List<Card> filtrats = new List<Card>();

        foreach (Card carta in _store.Cards.Where(IsPlayerCard).OrderBy(c => c.Code).ToList())
        {
            bool comp = true;

            if (carta.Title.CompareTo(nom) != 0 && nom != "")
            {
                comp = false;
            }

            if(carta.AspectId != aspect_id && aspect_id != -1)
            {
                comp = false;
            }

            if (carta.TypeId != type_id && type_id != -1)
            {
                comp = false;
            }

            if (carta.HeroId != hero_id && hero_id != -1)
            {
                comp = false;
            }

            if (carta.PackId != pack_id && pack_id != -1)
            {
                comp = false;
            }

            if (cost.CompareTo("---") != 0)
            {
                int cost_int;
                
                if(Int32.TryParse(cost, out cost_int))
                {
                    if (carta.Cost != cost_int)
                    {
                        
                        comp = false;
                    }
                }


            }


            if (comp == true)
            {
                filtrats.Add(carta);
            }

        }


        return filtrats;
    }

    public void Add(Card carta)
    {
        carta.Id = _store.NextCardId();

        _store.Cards.Add(carta);

        _store.Link();
    }

    public void Update(Card carta)
    {
        Card carta_memoria = _store.Cards.FirstOrDefault(c => c.Id == carta.Id);

        if (carta_memoria != null)
        {
            // Actualizar las propiedades
            carta_memoria = carta;

            _store.Link();
        }
    }

    // TODO: implement the other methods of ICardDao. After a change that adds or modifies
    // a card, call _store.Link() so its Aspect, Type, Pack and Hero references are set.
}
