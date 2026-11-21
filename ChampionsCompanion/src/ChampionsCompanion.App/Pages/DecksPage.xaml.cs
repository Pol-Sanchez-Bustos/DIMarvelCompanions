using ChampionsCompanion.Core.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChampionsCompanion.App.Pages;

/// <summary>D.1 Deck list. TODO: everything, starting with the methods of IDeckDao.</summary>
public partial class DecksPage : Page
{
    public DecksPage()
    {
        InitializeComponent();
    }

    List<Deck> cards;
    List<Deck> decks_filter;
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Pages never create DAOs: they use the ones in AppServices.
        var decks = AppServices.Cards.GetAll();
        StatusText.Text = $"{decks.Count} player cards loaded. First one: {decks[0]} ({decks[0].Aspect.Name} {decks[0].Type.Name}).";


        decks = AppServices.Cards.GetAll();
        
        lsvDecks.ItemsSource = decks;

        lbDeckCount.Content = decks.Count + " decks - filters kept when switching modes";


        List<Aspect> aspects = AppServices.Aspects.GetAll();

        Aspect asp = new Aspect();
        asp.Name = "All";
        asp.Id = -1;
        aspects.Add(asp);

        cbAspect.ItemsSource = aspects;
        cbAspect.SelectedItem = asp;


        List<Hero> heros = AppServices.Heros.getAll();

        Hero hero = new Hero();
        hero.Name = "All";
        hero.Id = -1;

        heros.Add(hero);
        cbHero.ItemsSource = heros;
        cbHero.SelectedItem = hero;
        cbHero.IsEnabled = false;


        List<String> stat = new List<string>();
        stat.Add("All");
        stat.Add("Complete");
        stat.Add("Incomplete");

        cbStatus.ItemsSource = stat;
        cbStatus.SelectedItem = "All";


    }

    

    private void filtrarDecks()
    {
        int stat = 0;

        switch(cbStatus.SelectedItem as String)
        {
            case "All": stat = 0; break;
            case "Complete": stat = 1; break;
            case "Incomplete": stat = -1; break;
        }
        decks_filter = AppServices.Decks.GetAllFiltrats(txtName.Text,
            (cbHero.SelectedItem as Hero).Id,
            (cbAspect.SelectedItem as Aspect).Id,
            stat

            );

        lsvDecks.ItemsSource = decks_filter;

    }

    private void cbSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbAspect.IsLoaded && cbAspect.HasItems && cbAspect.SelectedItem != null)
        {
            if ((cbAspect.SelectedItem as Aspect).Id == -1 || (cbAspect.SelectedItem as Aspect).Id == 1)
            {
                cbHero.IsEnabled = true;
            }
            else
            {
                cbHero.IsEnabled = false;
            }
        }

        if (cbAspect.IsLoaded && cbAspect.HasItems && cbAspect.SelectedItem != null &&
            cbHero.IsLoaded && cbHero.HasItems && cbHero.SelectedItem != null &&
            cbStatus.IsLoaded && cbStatus.HasItems && cbStatus.SelectedItem != null)
        {
            filtrarDecks();
            lbDeckCount.Content = decks_filter.Count + " cards - filters kept when switching modes";
        }
    }




    private void btNewDeck_Click(object sender, RoutedEventArgs e)
    {

        NavigationService.Navigate(new NewDeck());
    }



    private void txtName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            filtrarDecks();
        }
    }

    private void btEdit_Click(object sender, RoutedEventArgs e)
    {

    }

    private void btDelete_Click(object sender, RoutedEventArgs e)
    {

    }
}
