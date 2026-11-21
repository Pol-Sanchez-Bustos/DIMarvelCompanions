using ChampionsCompanion.Core.Model;

namespace ChampionsCompanion.Core.Data;

/// <summary>Data access object (DAO) for decks. See <see cref="ICardDao"/> for the idea behind it.</summary>
/// <remarks>TODO: add the methods your deck pages need, and implement them in <c>InMemoryDeckDao</c>.</remarks>
public interface IDeckDao
{
    public List<Deck> getAll();

    public List<Deck> GetAllFiltrats(String nom = "", int hero_id = -1, int aspect_id = -1, int compl = -1);

    public void Add(Deck deck);
    public void Update(Deck deck);
}
