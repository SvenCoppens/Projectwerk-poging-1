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
using DomainLibrary;

namespace Projectwerk_poging_1
{
    /// <summary>
    /// Interaction logic for Reservatie_details.xaml
    /// </summary>
    public partial class ReservatieDetailsWindow : Window
    {
        public ReservatieDetailsWindow(Reservatie reservatie)
        {
            InitializeComponent();
            DatumReserveringTextBlock.Text = reservatie.DatumVanReservering.ToString("dddd, dd MMMM yyyy");
            ReserveringNummerTextBlock.Text = reservatie.ReserveringsNummer.ToString();

            //Klant informatie:
            KlantNummerTextBlock.Text = reservatie.Klant.KlantNummer.ToString();
            KlantNaamTextBlock.Text = reservatie.Klant.Naam;
            KlantAdresTextBlock.Text = reservatie.Klant.Adres;
            if (reservatie.Klant.BtwNummer != null)
                KlantBtwNummerTextBlock.Text = reservatie.Klant.BtwNummer;

            //limo informatie
            LimoNaamTextBlock.Text = reservatie.Limousine.Naam;

            //Prijs afhandeling
            KortingTextBlock.Text = reservatie.AangerekendeKorting.ToString() +"%";
            TotaalZonderBtwTextBlock.Text = reservatie.TotaalExclusiefBtw.ToString() + "€";
            BtwBedragTextBlock.Text = reservatie.BtwBedrag.ToString() +"€";
            TotaalBedragTextBlock.Text = reservatie.TotaalTeBetalen.ToString() + "€";

            //AantalUrenAfhandeling
            VasteKostPrijsTextBlock.Text = reservatie.VastePrijs.ToString();
            if (reservatie.AantalEersteUur != 0)
            {
                EersteUurPrijsTextBlock.Text = reservatie.EersteUurPrijs.ToString();
            }
            if (reservatie.AantalStandaardUur != 0)
            {
                AantalStandaardUurLabel.ContentStringFormat = $"{reservatie.AantalStandaardUur} * " + AantalStandaardUurLabel.ContentStringFormat;
                StandaardUurPrijsTextBlock.Text = reservatie.StandaarUurPrijs.ToString();
                StandaardUurTotaalTextBlock.Text = (reservatie.StandaarUurPrijs * reservatie.AantalStandaardUur).ToString();
            }
            if (reservatie.AantalOverUur != 0)
            {
                AantalOverUurLabel.ContentStringFormat = $"{reservatie.AantalOverUur} * " + AantalOverUurLabel.ContentStringFormat;
                OverUurPrijsTextBlock.Text = reservatie.OverUurPrijs.ToString();
                OverUurTotaalTextBlock.Text = (reservatie.OverUurPrijs * reservatie.OverUurPrijs).ToString();
            }
            if(reservatie.AantalNachtUur != 0)
            {
                AantalNachtUrenLabel.ContentStringFormat = $"{reservatie.AantalNachtUur} * " + AantalNachtUrenLabel.ContentStringFormat;
                NachtUurPrijsTextBlock.Text = reservatie.NachtUurPrijs.ToString();
                NachtUurTotaalTextBlock.Text = (reservatie.NachtUurPrijs * reservatie.AantalNachtUur).ToString();
            }
        }




    }
}
