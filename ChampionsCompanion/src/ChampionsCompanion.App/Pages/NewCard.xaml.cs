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
using ChampionsCompanion.Core.Data.InMemory;


namespace ChampionsCompanion.App.Pages
{
    /// <summary>
    /// Lógica de interacción para NewCard.xaml
    /// </summary>
    public partial class NewCard : Page
    {
        Card carta = null;
        Card aux_card = null;

        
        public NewCard(Card carta_arg)
        {
            
            InitializeComponent();




            List<String> costos = new List<String>();
            costos.Add("---");

            for (int i = 0; i < 10; i++)
            {
                costos.Add("" + i);
            }

            cbCost.ItemsSource = costos;

            cbType.ItemsSource = AppServices.CardTypes.getAll();
            cbPack.ItemsSource = AppServices.Packs.getAll();
            cbHero.ItemsSource = AppServices.Heros.getAll();
            cbAspect.ItemsSource = AppServices.Aspects.GetAll();


            if (carta_arg != null)
            {
                carta = carta_arg;

                

                loadCardData();

                loadCardPreview();
            }
            else
            {
                btCancel.IsEnabled = false;
                txtCode.IsEnabled = true;
                cbCost.SelectedItem = "---";
            }

        }

        private void loadCardData()
        {

            txtTitle.Text = carta.Title;
            txtSubtitle.Text = carta.Subtitle;
            txtCode.Text = carta.Code;

            cbPack.SelectedItem = carta.Pack;
            cbType.SelectedItem = carta.Type;
            cbAspect.SelectedItem = carta.Aspect;
            cbHero.SelectedItem = carta.Hero;

            // Cost and resources
            cbCost.SelectedItem = carta.CostText;

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

            tgbtnUnique.IsChecked = carta.IsUnique;

            // Ally stats
            if (carta.Type.Name.CompareTo("Ally") == 0)
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

            if (tgbtnUnique.IsChecked == true)
            {
                txtDeckLimit.IsEnabled = false;
                txtDeckLimit.Text = "1";
            }
            else
            {
                txtDeckLimit.IsEnabled = true;

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
            aux_card = carta;
            loadCardData();
        }

        private int pujaIdentity()
        {
            int qt_errors = 0;

            if (carta == null)
            {
                if (txtCode.Text.CompareTo("") != 0)
                {
                    aux_card.Code = txtCode.Text;
                }
                else
                {
                    txtCode.BorderBrush = Brushes.IndianRed;
                    qt_errors++;
                }
            }

            if (txtTitle.Text.CompareTo("") != 0)
            {
                aux_card.Title = txtTitle.Text;
            }
            else
            {
                txtTitle.BorderBrush = Brushes.IndianRed;
                qt_errors++;
            }

            if (txtSubtitle.Text.CompareTo("") != 0)
            {
                aux_card.Subtitle = txtSubtitle.Text;
            }

            if (cbPack.SelectedItem != null)
            {
                aux_card.Pack = cbPack.SelectedItem as Pack;
            }
            else qt_errors++;

            if (cbType.SelectedItem != null)
            {
                aux_card.Type = cbType.SelectedItem as CardType;
            }
            else qt_errors++;

            if (cbAspect.SelectedItem != null)
            {
                aux_card.Aspect = cbAspect.SelectedItem as Aspect;

                if ((cbAspect.SelectedItem as Aspect).Name.CompareTo("Hero") == 0)
                {
                    if (cbHero.SelectedItem != null)
                    {
                        aux_card.Hero = cbHero.SelectedItem as Hero;
                    }
                    else qt_errors++;
                }
                else
                {
                    aux_card.Hero = null;
                }
            }
            else qt_errors++;

            aux_card.IsUnique = tgbtnUnique.IsChecked == true;

            return qt_errors;
        }

        private int pujaClass()
        {
            int qt_errors = 0;
            int aux_num = 0;

            if (cbCost.SelectedItem != null &&
                Int32.TryParse(cbCost.SelectedItem as String, out aux_num))
            {
                aux_card.Cost = aux_num;
            }
            else
            {
                aux_card.Cost = null;
            }

            qt_errors += pujaResources();
            qt_errors += pujaAllyStats();

            return qt_errors;
        }

        private int pujaResources()
        {
            int qt_errors = 0;
            int aux_num = 0;

            if (Int32.TryParse(txtEnergy.Text, out aux_num))
            {
                if (aux_num < 0 || aux_num > 9)
                {
                    qt_errors++;
                    txtEnergy.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.ResourceEnergy = aux_num;
                }
            }
            else
            {
                qt_errors++;
                txtEnergy.BorderBrush = Brushes.IndianRed;
            }

            if (Int32.TryParse(txtPhysical.Text, out aux_num))
            {
                if (aux_num < 0 || aux_num > 9)
                {
                    qt_errors++;
                    txtPhysical.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.ResourcePhysical = aux_num;
                }
            }
            else
            {
                qt_errors++;
                txtPhysical.BorderBrush = Brushes.IndianRed;
            }

            if (Int32.TryParse(txtMental.Text, out aux_num))
            {
                if (aux_num < 0 || aux_num > 9)
                {
                    qt_errors++;
                    txtMental.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.ResourceMental = aux_num;
                }
            }
            else
            {
                qt_errors++;
                txtMental.BorderBrush = Brushes.IndianRed;
            }

            if (Int32.TryParse(txtWild.Text, out aux_num))
            {
                if (aux_num < 0 || aux_num > 9)
                {
                    qt_errors++;
                    txtWild.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.ResourceWild = aux_num;
                }
            }
            else
            {
                qt_errors++;
                txtWild.BorderBrush = Brushes.IndianRed;
            }

            return qt_errors;
        }

        private int pujaAllyStats()
        {
            int qt_errors = 0;
            int aux_num = 0;

            if (aux_card.Type == null || aux_card.Type.Name.CompareTo("Ally") != 0)
            {
                aux_card.AttackCost = null;
                aux_card.Attack = null;
                aux_card.ThwartCost = null;
                aux_card.Thwart = null;
                aux_card.Health = null;

                return qt_errors;
            }

            if (Int32.TryParse(txtAtkCost.Text, out aux_num))
            {
                if (aux_num < 0)
                {
                    qt_errors++;
                    txtAtkCost.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.AttackCost = aux_num;
                }
            }
            else
            {
                qt_errors++;
                txtAtkCost.BorderBrush = Brushes.IndianRed;
            }

            if (Int32.TryParse(txtArkDmg.Text, out aux_num))
            {
                if (aux_num < 0)
                {
                    qt_errors++;
                    txtArkDmg.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.Attack = aux_num;
                }
            }
            else if (txtArkDmg.Text != "")
            {
                qt_errors++;
                txtArkDmg.BorderBrush = Brushes.IndianRed;
            }
            else
            {
                aux_card.Attack = null;
            }

            if (Int32.TryParse(txtThwCost.Text, out aux_num))
            {
                if (aux_num < 0)
                {
                    qt_errors++;
                    txtThwCost.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.ThwartCost = aux_num;
                }
            }
            else
            {
                qt_errors++;
                txtThwCost.BorderBrush = Brushes.IndianRed;
            }

            if (Int32.TryParse(txtThwDmg.Text, out aux_num))
            {
                if (aux_num < 0)
                {
                    qt_errors++;
                    txtThwDmg.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.Thwart = aux_num;
                }
            }
            else if (txtThwDmg.Text != "")
            {
                qt_errors++;
                txtThwDmg.BorderBrush = Brushes.IndianRed;
            }
            else
            {
                aux_card.Thwart = null;
            }

            if (Int32.TryParse(txtHp.Text, out aux_num))
            {
                if (aux_num < 0)
                {
                    qt_errors++;
                    txtHp.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.Health = aux_num;
                }
            }
            else
            {
                qt_errors++;
                txtHp.BorderBrush = Brushes.IndianRed;
            }

            return qt_errors;
        }

        private int pujaDeckBuilding()
        {
            int qt_errors = 0;
            int aux_num = 0;

            if (Int32.TryParse(txtDeckLimit.Text, out aux_num))
            {
                if (aux_num < 0)
                {
                    qt_errors++;
                    txtDeckLimit.BorderBrush = Brushes.IndianRed;
                }
                else
                {
                    aux_card.DeckLimit = aux_num;
                }
            }
            else if (txtDeckLimit.Text == "")
            {
                aux_card.DeckLimit = null;
            }
            else
            {
                qt_errors++;
                txtDeckLimit.BorderBrush = Brushes.IndianRed;
            }

            return qt_errors;
        }

        private void pujaText()
        {
            aux_card.Traits = txtTraits.Text;
            aux_card.Text = txtRules.Text;
            aux_card.Flavor = TxtFlavour.Text;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            int qt_errors = 0;

            



            if (carta != null) { 
                aux_card = carta;
            }
            else
            {
                aux_card = new Card();
                

            }

            qt_errors += pujaIdentity();

            qt_errors += pujaClass();

            qt_errors += pujaResources();

            qt_errors += pujaAllyStats();

            qt_errors += pujaDeckBuilding();

            pujaText();


            if (qt_errors == 0)
            {
                


                if (carta != null)
                {
                    AppServices.Cards.Update(aux_card);
                }
                else
                {
                    AppServices.Cards.Add(aux_card);
                }
                NavigationService.Navigate(new CardsPage());
            }
            else
            {
                if(qt_errors == 1)lbErrorToFix.Content = qt_errors + " error to fix before saving";
                else lbErrorToFix.Content = qt_errors + " errors to fix before saving";
            }

        }

        private void cbAspect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAspect.SelectedItem != null && cbAspect.IsLoaded && cbAspect.HasItems)
            {
                if ((cbAspect.SelectedItem as Aspect).Name.CompareTo("Hero") == 0) { 
                    cbHero.IsEnabled = true;
                }
                else
                {
                    cbHero.SelectedItem = null;
                    cbHero.IsEnabled=false;
                }
            }
        }

        private void tgbtnUnique_Click(object sender, RoutedEventArgs e)
        {
            if (tgbtnUnique.IsChecked == true)
            {
                txtDeckLimit.IsEnabled = false;
                txtDeckLimit.Text = "1";
            }
            else
            {
                txtDeckLimit.IsEnabled = true;
                
            }
                
        }
    }
}
