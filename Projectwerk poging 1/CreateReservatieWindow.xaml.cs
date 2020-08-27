using DataLayer;
using DomainLibrary;
using DomainLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projectwerk_poging_1
{
    /// <summary>
    /// Interaction logic for CreateReservatieWindow.xaml
    /// </summary>
    public partial class CreateReservatieWindow : Window
    {
        public CreateReservatieWindow()
        {
            InitializeComponent();
            StartLocatieComboBox.ItemsSource = Enum.GetValues(typeof(StalLocatie)).Cast<StalLocatie>().ToList();
            EindLocatieComboBox.ItemsSource = Enum.GetValues(typeof(StalLocatie)).Cast<StalLocatie>().ToList();
            ArrengementTypeList.ItemsSource = Enum.GetValues(typeof(Arrengement)).Cast<Arrengement>().ToList();
            ArrengementTypeList.SelectedIndex = 0;
            StartLocatieComboBox.SelectedIndex = 0;
            EindLocatieComboBox.SelectedIndex = 0;
            DatePickerCalender.SelectedDate = DateTime.Today;
        }

        private void BeschikbaarheidCheckButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateLimousines();
        }

        private void KlantZoekButton_Click(object sender, RoutedEventArgs e)
        {
            Window searchKlantWindow = new SearchKlantWindow();
            searchKlantWindow.Show();
        }


        private void ReservatieAanmakenButton_Click(object sender, RoutedEventArgs e)
        {
            if (KlantNrTextBox.Text != null && DataGridLimousines.SelectedItem != null)
            {
                int klantNr = int.Parse(KlantNrTextBox.Text);
                DateTime startDatum = (DateTime)DatePickerCalender.SelectedDate;
                Arrengement arrengement = (Arrengement)ArrengementTypeList.SelectedItem;
                int startUur = (int)BeschikbareUrenList.SelectedItem;
                int duur = (int)DuurLijst.SelectedItem;
                int limoId = ((Limousine)DataGridLimousines.SelectedItem).Id;
                string verwachtAdres = VerwachtAdresTextBox.Text;
                StalLocatie EindStalPlaats = (StalLocatie)EindLocatieComboBox.SelectedItem;
                StalLocatie StartStalPlaats = (StalLocatie)StartLocatieComboBox.SelectedItem;
                ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
                Reservatie toShow = rM.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, StartStalPlaats, EindStalPlaats, verwachtAdres);

                ReservatieDetailsWindow detailsWindow = new ReservatieDetailsWindow(toShow);
                detailsWindow.Show();
                this.Close();
            }
            else
                MessageBox.Show("Gelieve een klantenNummer in te vullen en een voertuig te selecteren.", "Onvolledige reservering", MessageBoxButton.OK);
            
        }

        private void ArrengementTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridLimousines.ItemsSource = null;
            string geselecteerdArrengement =((ComboBox)sender).SelectedItem.ToString();
            if (geselecteerdArrengement != null)
            {
                Arrengement arrengement = (Arrengement)Enum.Parse(typeof(Arrengement), geselecteerdArrengement);
                if(arrengement == Arrengement.Airport)
                {
                    BeschikbareUrenList.ItemsSource = null;
                    DuurLijst.ItemsSource = null;
                    BeschikbareUrenList.ItemsSource = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
                    BeschikbareUrenList.SelectedIndex = 0;
                    DuurLijst.ItemsSource = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                    DuurLijst.SelectedIndex = 0;
                }
                else if(arrengement == Arrengement.Business)
                {
                    BeschikbareUrenList.ItemsSource = null;
                    DuurLijst.ItemsSource = null;
                    BeschikbareUrenList.ItemsSource = new List<int> { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20, 21, 22, 23, 24 };
                    DuurLijst.ItemsSource = new List<int> { 1,2,3,4,5,6,7, 8, 9, 10, 11 };
                    BeschikbareUrenList.SelectedIndex = 0;
                    DuurLijst.SelectedIndex = 0;
                }
                else if (arrengement == Arrengement.NightLife)
                {
                    BeschikbareUrenList.ItemsSource = null;
                    DuurLijst.ItemsSource = null;
                    BeschikbareUrenList.ItemsSource = new List<int> { 20,21,22,23,24 };
                    DuurLijst.ItemsSource = new List<int> { 7, 8, 9, 10, 11 };
                    BeschikbareUrenList.SelectedIndex = 0;
                    DuurLijst.SelectedIndex = 0;
                }
                else if (arrengement == Arrengement.Wedding)
                {
                    BeschikbareUrenList.ItemsSource = new List<int> { 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                    DuurLijst.ItemsSource = new List<int> { 7, 8, 9, 10, 11 };
                    BeschikbareUrenList.SelectedIndex = 0;
                    DuurLijst.SelectedIndex = 0;
                }
                else if (arrengement == Arrengement.Wellness)
                {
                    BeschikbareUrenList.ItemsSource = new List<int> { 7, 8, 9, 10, 11, 12 };
                    DuurLijst.ItemsSource = new List<int>{ 10};
                    BeschikbareUrenList.SelectedIndex = 0;
                    DuurLijst.SelectedIndex = 0;
                }
            }
        }
        private void Tijdstip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridLimousines.ItemsSource = null;
        }

        private void DatePickerCalender_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridLimousines.ItemsSource = null;
        }
        private void UpdateLimousines()
        {
            ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
            DateTime start = (DateTime)DatePickerCalender.SelectedDate;
            start = start.AddHours((int)BeschikbareUrenList.SelectedItem);
            DateTime eind = start.AddHours((int)DuurLijst.SelectedItem);
            List<Limousine> limousines = rM.GeefBeschikbareLimousinesVoorPeriode(start, eind);
            Arrengement arrengement = (Arrengement)ArrengementTypeList.SelectedItem;
            if (arrengement == Arrengement.NightLife)
            {
                limousines = limousines.Where(l => l.NightlifePrijs != null).ToList();
            }
            else if (arrengement == Arrengement.Wedding)
            {
                limousines = limousines.Where(l => l.WeddingPrijs != null).ToList();
            }
            else if (arrengement == Arrengement.Wellness)
            {
                limousines = limousines.Where(l => l.WellnessPrijs != null).ToList();
            }

            DataGridLimousines.ItemsSource = null; ;
            DataGridLimousines.ItemsSource = limousines;
            if (limousines.Count == 0)
                MessageBox.Show("Geen resultaten gevonden.", "No results", MessageBoxButton.OK);
        }
    }
}
