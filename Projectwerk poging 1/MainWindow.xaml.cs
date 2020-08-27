using DataLayer;
using DomainLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projectwerk_poging_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ReservatieManager rm = new ReservatieManager(new ReservatieDatabaseHandler());
            int limousines = rm.GeefAantalLimousines();
            if (limousines == 0)
            {
                InitialRun.CreateInitialDatabaseLimousines();
            }
            if (rm.GeefAantalKlanten() == 0)
            {
                InitialRun.CreateInitialDatabaseKlanten();
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Window CreateWindow = new CreateReservatieWindow();
            CreateWindow.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Window SearchWindow = new SearchReservatieWindow();
            SearchWindow.Show();
        }
    }
}
