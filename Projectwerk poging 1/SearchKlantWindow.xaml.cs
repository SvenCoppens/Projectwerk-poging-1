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
            List<Klant> klanten = rM.FindKlantVoorBtwNummer(BtwNummerTextBox.Text);
            SetDataGridSource(klanten);
        }

        private void NaamSearchButton(object sender, RoutedEventArgs e)
        {
            ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
            List<Klant> klanten = rM.FindKlantVoorBtwNummer(BtwNummerTextBox.Text);
            SetDataGridSource(klanten);
        }
        private void SetDataGridSource(List<Klant> klanten)
        {
            KlantDataGrid.ItemsSource = null;
            KlantDataGrid.ItemsSource = klanten;
        }
    }
}
