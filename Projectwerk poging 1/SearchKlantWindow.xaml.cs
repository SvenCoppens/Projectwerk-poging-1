using DataLayer;
using DomainLibrary;
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
using System.Windows.Shapes;

namespace Projectwerk_poging_1
{
    /// <summary>
    /// Interaction logic for SearchKlantWindow.xaml
    /// </summary>
    public partial class SearchKlantWindow : Window
    {
        public SearchKlantWindow()
        {
            InitializeComponent();
        }

        private void ClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(((Klant)KlantDataGrid.SelectedItem).KlantNummer.ToString());
        }

        private void BtwSearchButton(object sender, RoutedEventArgs e)
        {
            ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
            Klant klant = rM.VindKlantVoorBtwNummer(BtwNummerTextBox.Text);
            List<Klant> klanten = new List<Klant> { klant };
            ZetDataGridBron(klanten);
        }

        private void NaamSearchButton(object sender, RoutedEventArgs e)
        {
            ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
            List<Klant> klanten = rM.VindKlantVoorNaam(KlantNaamTextBox.Text);
            ZetDataGridBron(klanten);
        }
        private void ZetDataGridBron(List<Klant> klanten)
        {
            KlantDataGrid.ItemsSource = null;
            KlantDataGrid.ItemsSource = klanten;
            if (klanten.Count == 0)
                MessageBox.Show("Geen resultaten gevonden.", "No results", MessageBoxButton.OK);
        }
    }
}
