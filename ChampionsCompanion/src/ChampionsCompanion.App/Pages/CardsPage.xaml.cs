using ChampionsCompanion.Core.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChampionsCompanion.App.Pages;

/// <summary>C.1 Card list. TODO: everything; this only shows how to read from a DAO.</summary>
public partial class CardsPage : Page
{
    public CardsPage()
    {
        InitializeComponent();
    }
    List<Card> cards;
    List<Card> cards_filter;
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Pages never create DAOs: they use the ones in AppServices.
        var cards = AppServices.Cards.GetAll();
        StatusText.Text = $"{cards.Count} player cards loaded. First one: {cards[0]} ({cards[0].Aspect.Name} {cards[0].Type.Name}).";


        cards = AppServices.Cards.GetAll();
        dtgCards.ItemsSource = cards;
        lsvCards.ItemsSource = cards;




        List<Aspect> aspects = AppServices.Aspects.GetAll();

        Aspect asp = new Aspect();
        asp.Name = "All";
        asp.Id = -1;
        aspects.Add( asp );

        cbAspect.ItemsSource = aspects;
        cbAspect.SelectedItem = asp;


       
        List<CardType> types = AppServices.CardTypes.getAll();

        CardType typ = new CardType();
        typ.Name = "All";
        typ.Id = -1;

        types.Add(typ);
        cbType.ItemsSource = types;
        cbType.SelectedItem = typ;


        List<Hero> heros = AppServices.Heros.getAll();

        Hero hero = new Hero();
        hero.Name = "All";
        hero.Id = -1;

        heros.Add(hero);
        cbHero.ItemsSource = heros;
        cbHero.SelectedItem = hero;
        cbHero.IsEnabled = false;


        List<Pack> packs = AppServices.Packs.getAll();

        Pack pack = new Pack();
        pack.Name = "All";
        pack.Id = -1;

        packs.Add(pack);
        cbPack.ItemsSource = packs;
        cbPack.SelectedItem = pack;

        List<String> costos = new List<String>();
        costos.Add("---");

        for (int i = 0; i<10; i++)
        {
            costos.Add(""+i);
        }

        cbCost.ItemsSource = costos;
        cbCost.SelectedItem = "---";



        btLsv.IsEnabled = true;
        btDtg.IsEnabled = false;
    }

    private void btDtg_Click(object sender, RoutedEventArgs e)
    {
        if (dtgCards.IsLoaded == true)
        {
            dtgCards.Visibility = Visibility.Visible;
            btLsv.IsEnabled = true;
            btDtg.IsEnabled = false;
        }

        if (lsvCards.IsLoaded == true)
        {
            lsvCards.Visibility = Visibility.Collapsed;
            
        }
    }

    private void btLsv_Click(object sender, RoutedEventArgs e)
    {
        if (dtgCards.IsLoaded == true)
        {
            dtgCards.Visibility = Visibility.Collapsed;
        }

        if (lsvCards.IsLoaded == true)
        {
            lsvCards.Visibility = Visibility.Visible;
            btLsv.IsEnabled = false;
            btDtg.IsEnabled = true;
        }
        
    }

    private void filtrarCartes()
    {
        cards_filter = AppServices.Cards.GetAllFiltrats(txtName.Text,
                (cbAspect.SelectedItem as Aspect).Id,
                (cbType.SelectedItem as CardType).Id,
                (cbHero.SelectedItem as Hero).Id,
                (cbPack.SelectedItem as Pack).Id,
                (cbCost.SelectedItem as string));

        dtgCards.ItemsSource = cards_filter;
        lsvCards.ItemsSource = cards_filter;
        
    }

    private void cbSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbAspect.IsLoaded && cbAspect.HasItems && cbAspect.SelectedItem != null)
        {
            if((cbAspect.SelectedItem as Aspect).Id == -1 || (cbAspect.SelectedItem as Aspect).Id == 1)
            {
                cbHero.IsEnabled = true;
            }
            else
            {
                cbHero.IsEnabled=false;
            }
        }

        if (cbAspect.IsLoaded && cbAspect.HasItems && cbAspect.SelectedItem != null &&
            cbType.IsLoaded && cbType.HasItems && cbType.SelectedItem != null &&
            cbHero.IsLoaded && cbHero.HasItems && cbHero.SelectedItem != null &&
            cbPack.IsLoaded && cbPack.HasItems && cbPack.SelectedItem != null &&
            cbCost.IsLoaded && cbCost.HasItems && cbCost.SelectedItem != null )
        {
            filtrarCartes();
        }
    }

    
    

    private void btNewCard_Click(object sender, RoutedEventArgs e)
    {

        Card carta = null;
        if(dtgCards.IsLoaded == true && dtgCards.HasItems)
        {
            if (dtgCards.SelectedItem != null) carta = dtgCards.SelectedItem as Card;
        }
        
        NavigationService.Navigate(new NewCard(carta));
    }

    

    private void txtName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if(e.Key == Key.Enter) {
            filtrarCartes();
        }
    }
}
