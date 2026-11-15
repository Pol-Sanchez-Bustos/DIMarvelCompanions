using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChampionsCompanion.Core.Model;
using System.Collections.ObjectModel;


namespace ChampionsCompanion.App.Pages
{
    /// <summary>
    /// Lógica de interacción para NewCard.xaml
    /// </summary>
    public partial class NewCard : Page
    {
        Card carta = null;

        enum Estat
        {
            Edicio,
            Creacio
        }

        Estat estat = Estat.Creacio;
        public NewCard(Card carta_arg)
        {
            
            InitializeComponent();

            if (carta_arg != null)
            {
                carta = carta_arg;
                estat = Estat.Edicio;

                loadCardData();

                loadCardPreview();
            }
            else
            {
                txtCode.IsEnabled = true;
            }

        }

        private void loadCardData()
        {

            txtTitle.Text = carta.Title;
            txtSubtitle.Text = carta.Subtitle;
            txtCode.Text = carta.Code;
            txtPack.Text = carta.Pack?.Name;

            // Classification
            txtType.Text = carta.Type?.Name;
            txtAspect.Text = carta.Aspect?.Name;
            txtHero.Text = carta.Hero?.Name;

            // Cost and resources
            txtCost.Text = carta.CostText;

            if(carta.ResourceEnergy == null)
            {
                txtEnergy.Text = "0";
            }
            else
            {
                txtEnergy.Text = carta.ResourceEnergy?.ToString();
            }

            if (carta.ResourcePhysical == null)
            {
                txtPhysical.Text = "0";
            }
            else
            {
                txtPhysical.Text = carta.ResourcePhysical?.ToString();
            }

            if (carta.ResourceMental == null)
            {
                txtMental.Text = "0";
            }
            else
            {
                txtMental.Text = carta.ResourceMental?.ToString();
            }

            if (carta.ResourceWild == null)
            {
                txtWild.Text = "0";
            }
            else
            {
                txtWild.Text = carta.ResourceWild?.ToString();
            }


            // Ally stats
            if(carta.Type.Name.CompareTo("Ally") == 0)
            {
                if (carta.Attack == null)
                    txtAtkCost.Text = "0";
                else
                    txtAtkCost.Text = carta.Attack.ToString();

                if (carta.AttackCost == null)
                    txtArkDmg.Text = "0";
                else
                    txtArkDmg.Text = carta.AttackCost.ToString();

                if (carta.Thwart == null)
                    txtThwCost.Text = "0";
                else
                    txtThwCost.Text = carta.Thwart.ToString();

                if (carta.ThwartCost == null)
                    txtThwDmg.Text = "0";
                else
                    txtThwDmg.Text = carta.ThwartCost.ToString();

                if (carta.Health == null)
                    txtHp.Text = "0";
                else
                    txtHp.Text = carta.Health.ToString();
            }
            else
            {
                txtAtkCost.Text = "";
                txtArkDmg.Text = "";
                txtThwCost.Text = "";
                txtThwDmg.Text = "";
                txtHp.Text = "";

                brAllyStats.Visibility = Visibility.Collapsed;
            }
            

            // Deck building
            
            txtDeckLimit.Text = carta.DeckLimit?.ToString();

            // Text
            txtTraits.Text = carta.Traits;
            txtRules.Text = carta.Text;
            TxtFlavour.Text = carta.Flavor;

            // Image
            if (!string.IsNullOrEmpty(carta.ImagePath))
            {
                imgCard.Source = new BitmapImage(
                    new Uri(carta.ImagePath, UriKind.Relative));

                lbImagePath.Content = "Current file: " + carta.ImagePath;
            }
        }

        private void loadCardPreview()
        {
            imgPrev.Source = new BitmapImage(
                new Uri(carta.ImagePath, UriKind.Relative));

            lbTitle.Content = carta.Title+" - "+carta.Id;
            lbPrevTitle.Content = "● "+carta.Title;
            lbPrevSubTitle.Content = " " + carta.Subtitle;

            lbPrevCost.Content = "Cost "+carta.Cost + " ";
            lbPrevType.Content = carta.Type.Name + " ";
            lbPrevAspect.Content = carta.Aspect.Name + " ";
            

            lbPrevAtk.Content = carta.Attack;
            lbPrevTHW.Content = carta.Thwart;
            lbPrevHP.Content = carta.Health;

            lbPrevTraits.Content = carta.Traits;

            lbPrevText.Content = carta.Text;
            lbPrevFlavor.Content = carta.Flavor;
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CardsPage());
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
