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
            DatumVanAanmaakReservatieTextBlock.Text = reservatie.DatumVanReservering.ToString("HH:mm dddd, dd MMMM yyyy");
            DatumReserveringTextBlock.Text = reservatie.StartMoment.ToString("HH:mm dddd, dd MMMM yyyy");
            ReserveringNummerTextBlock.Text = reservatie.ReserveringsNummer.ToString();

            //Klant informatie:
            KlantNummerTextBlock.Text = reservatie.Klant.KlantNummer.ToString();
            KlantNaamTextBlock.Text = reservatie.Klant.Naam;
            KlantAdresTextBlock.Text = reservatie.Klant.Adres;
            KlantCategorieTextBlock.Text = reservatie.Klant.Categorie.Naam;
            if (reservatie.Klant.BtwNummer != null)
                KlantBtwNummerTextBlock.Text = reservatie.Klant.BtwNummer;
            else BtwNummerLabel.Text = null;
            

            //limo informatie
            LimoNaamTextBlock.Text = reservatie.Limousine.Naam;
            StartLocatieTextBlock.Text = reservatie.StartStalLocatie.ToString();
            EindLocatieTextBlock.Text = reservatie.AankomstStalLocatie.ToString();

            if (reservatie.VerwachtAdres != null && reservatie.VerwachtAdres != "")
                VerwachtAdresTextBlock.Text = reservatie.VerwachtAdres;
            else
                VerwachtAdresLabel.Text = null;

            //Prijs afhandeling
            KortingTextBlock.Text = reservatie.AangerekendeKorting.ToString() +"%";
            TotaalZonderBtwTextBlock.Text = reservatie.TotaalMetKortingExclusiefBtw.ToString() + "€";
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
                StandaardUurPrijsTextBlock.Text = reservatie.StandaarUurPrijs.ToString()+"/h";
                StandaardUurTotaalTextBlock.Text = (reservatie.StandaarUurPrijs * reservatie.AantalStandaardUur).ToString();
            }
            if (reservatie.AantalOverUur != 0)
            {
                AantalOverUurLabel.ContentStringFormat = $"{reservatie.AantalOverUur} * " + AantalOverUurLabel.ContentStringFormat;
                OverUurPrijsTextBlock.Text = reservatie.OverUurPrijs.ToString() + "/h";
                OverUurTotaalTextBlock.Text = (reservatie.OverUurPrijs * reservatie.OverUurPrijs).ToString();
            }
            if(reservatie.AantalNachtUur != 0)
            {
                AantalNachtUrenLabel.ContentStringFormat = $"{reservatie.AantalNachtUur} * " + AantalNachtUrenLabel.ContentStringFormat;
                NachtUurPrijsTextBlock.Text = reservatie.NachtUurPrijs.ToString() + "/h";
                NachtUurTotaalTextBlock.Text = (reservatie.NachtUurPrijs * reservatie.AantalNachtUur).ToString();
            }
        }




    }
}
