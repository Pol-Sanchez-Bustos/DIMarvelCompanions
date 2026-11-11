namespace ChampionsCompanion.Core.Model;

/// <summary>
/// The aspect of a card: Hero, Basic, Justice, Aggression, Protection or Leadership.
/// In the database this is the <c>faction</c> table.
/// </summary>
public class Aspect
{
    public const string Hero = "Hero";
    public const string Basic = "Basic";
    public const string Justice = "Justice";
    public const string Aggression = "Aggression";
    public const string Protection = "Protection";
    public const string Leadership = "Leadership";

    /// <summary>The aspect codes the app works with (rule C.0). Encounter, campaign and pool are left out.</summary>
    public static readonly string[] PlayerCodes = [Hero, Basic, Justice, Aggression, Protection, Leadership];

    /// <summary>The four aspects a deck can choose (rule S1).</summary>
    public static readonly string[] DeckCodes = [Justice, Aggression, Protection, Leadership];

    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";

    /// <summary>True for Justice, Aggression, Protection and Leadership.</summary>
    public bool IsDeckAspect => DeckCodes.Contains(Code);

    public override string ToString() => Name;
}
