using ChampionsCompanion.Core.Model;
using System.Windows;
using System.Windows.Controls;

namespace ChampionsCompanion.App.Pages;

/// <summary>C.1 Card list. TODO: everything; this only shows how to read from a DAO.</summary>
public partial class CardsPage : Page
{
    public CardsPage()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Pages never create DAOs: they use the ones in AppServices.
        var cards = AppServices.Cards.GetAll();
        StatusText.Text = $"{cards.Count} player cards loaded. First one: {cards[0]} ({cards[0].Aspect.Name} {cards[0].Type.Name}).";
        
        dtgCards.ItemsSource = AppServices.Cards.GetAll();

        cbAspect.ItemsSource = Aspect.PlayerCodes;

        cbType.ItemsSource = CardType.PlayerCodes;

        
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
            lsvCards.Visibility = Visibility.Hidden;
            
        }
    }

    private void btLsv_Click(object sender, RoutedEventArgs e)
    {
        if (dtgCards.IsLoaded == true)
        {
            dtgCards.Visibility = Visibility.Hidden;
        }

        if (lsvCards.IsLoaded == true)
        {
            lsvCards.Visibility = Visibility.Visible;
            btLsv.IsEnabled = false;
            btDtg.IsEnabled = true;
        }
        
    }
}
