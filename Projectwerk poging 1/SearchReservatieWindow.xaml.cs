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
    /// Interaction logic for SearchReservatieWindow.xaml
    /// </summary>
    public partial class SearchReservatieWindow : Window
    {
        public SearchReservatieWindow()
        {
            InitializeComponent();
        }

        private void ZoekOpKlantNaamButton_Click(object sender, RoutedEventArgs e)
        {
            ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
            List<Reservatie> reservaties = rM.FindReservatieVoorKlantNaam(KlantNaamTextBox.Text);
            SetDataGridSource(reservaties);
        }

        private void ZoekOpKlantNummerButton_Click(object sender, RoutedEventArgs e)
        {
            ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
            List<Reservatie> reservaties = rM.FindReservatieVoorKlantNummer(int.Parse(KlantNummerTextBox.Text));
            SetDataGridSource(reservaties);
        }

        private void ZoekOpDatumButton_Click(object sender, RoutedEventArgs e)
        {
            ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
            DateTime datum = (DateTime)ReservatieDatumCalender.SelectedDate;
            List<Reservatie> reservaties = rM.FindReservatieVoorDatum(datum);
            SetDataGridSource(reservaties);
        }

        private void ZoekOpNaamEnDatumButton_Click(object sender, RoutedEventArgs e)
        {
            ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
            DateTime datum = (DateTime)ReservatieDatumCalender.SelectedDate;
            List<Reservatie> reservaties = rM.FindReservatieVoorKlantNaamEnDatum(KlantNaamTextBox.Text,datum);
            SetDataGridSource(reservaties);
        }

        private void ZoekOpNummerEnDatumButton_Click(object sender, RoutedEventArgs e)
        {
            ReservatieManager rM = new ReservatieManager(new ReservatieDatabaseHandler());
            DateTime datum = (DateTime)ReservatieDatumCalender.SelectedDate;
            List<Reservatie> reservaties = rM.FindReservatieVoorKlantNummerEnDatum(int.Parse(KlantNummerTextBox.Text), datum);
            SetDataGridSource(reservaties);
        }
        private void SetDataGridSource(List<Reservatie> reservaties)
        {
            ReservatieDataGrid.ItemsSource = null;
            ReservatieDataGrid.ItemsSource = reservaties;
        }

        private void ToonReservaiteDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Reservatie res = (Reservatie)ReservatieDataGrid.SelectedItem;
            ReservatieDetailsWindow rdw = new ReservatieDetailsWindow(res);
            rdw.Show();
        }
    }
}
