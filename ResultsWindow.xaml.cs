using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static WPF_admin.Results;

namespace WPF_admin
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Page
    {
        public static int tkCompetition = -1;
        public static ObservableCollection<Results> results = new ObservableCollection<Results>();
        public ResultsWindow()
        {

            InitializeComponent();
            ReloadItems();
            ResultsList.ItemsSource = results;
            
        }

        private async void ReloadItems()
        {
            if(tkCompetition > 0)
            {
                results.Clear();
                List<Results> r = new List<Results>();
                r = await ResultsHandler.GetAllFromCompetition(tkCompetition);

                foreach(var result in r)
                {
                    results.Add(result);
                }

            }
        }

        public  void LoadItems()
        {
            ResultsList.ItemsSource = "";
            ResultsList.ItemsSource = results;
        }

        private void CloseResultsMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenResultsMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResultsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResultsList.SelectedIndex >= 0)
            {
                try
                {
                    Results rezultat = (Results)ResultsList.SelectedItem;
                    if (rezultat.DivRank == "") rezultat.DivRank = "0";
                    if (rezultat.Bib == "") rezultat.Bib = "0";
                    if (rezultat.Age == "") rezultat.Age = "0";
                    if (rezultat.SwimTime == "") rezultat.SwimTime = "0";
                    if (rezultat.SwimDistance == "") rezultat.SwimDistance = "0";
                    if (rezultat.RunTime == "") rezultat.RunTime = "0";
                    if (rezultat.RunDistance == "") rezultat.RunDistance = "0";
                    if (rezultat.BikeTime == "") rezultat.BikeTime = "0";
                    if (rezultat.BikeDistance == "") rezultat.BikeDistance = "0";
                    if (rezultat.T1 == "") rezultat.T1 = "0";
                    if (rezultat.T2 == "") rezultat.T2 = "0";

                    nameTextBox.Text = rezultat._Name;
                    GenderRankTextBoc.Text = rezultat.GenderRank;
                    DivisionRankTextBox.Value = Int32.Parse(rezultat.DivRank);
                    OverallRankTextBox.Text = rezultat.OverallRank;
                    bibTExtBox.Value = Int32.Parse(rezultat.Bib);
                    rezultatDivision.Text = rezultat.Division;
                    ageTextBox.Value = Int32.Parse(rezultat.Age);
                    ageCategoryTextBox.Text = rezultat.AgeCategory;
                    stateTextBox.Text = rezultat.State;
                    countryTextBox.Text = rezultat.Country;
                    proffesionTextBox.Text = rezultat.Profession;
                    pointsTextBox.Text = rezultat.Points;
                    swimTimeTextBox.Value = TimeSpan.Parse(rezultat.SwimTime);
                    swimDistanceTextBox.Value = Double.Parse(rezultat.SwimDistance.Split(' ').First().Replace('.', ','));
                    runTimeTextBox.Value = TimeSpan.Parse(rezultat.RunTime);
                    runDistanceTextBox.Value = Double.Parse(rezultat.RunDistance.Split(' ').First().Replace('.', ','));
                    bikeTimeTextBox.Value = TimeSpan.Parse(rezultat.BikeTime);
                    bikeDistanceTextBox.Value = Double.Parse(rezultat.BikeDistance.Split(' ').First().Replace('.', ','));
                    t1TextBox.Value = TimeSpan.Parse(rezultat.T1);
                    t2TextBox.Value = TimeSpan.Parse(rezultat.T2);
                    OverallTimeTextBox.Text = rezultat.OverallTime;
                    commentTextBox.Text = rezultat.Comments;
                }
                catch (Exception)
                {
                    nameTextBox.Text = "";
                    GenderRankTextBoc.Text = "";
                    DivisionRankTextBox.Value = 0;
                    OverallRankTextBox.Text = "";
                    bibTExtBox.Value = 0;
                    rezultatDivision.Text = "";
                    ageTextBox.Value = 0;
                    ageCategoryTextBox.Text = "";
                    stateTextBox.Text = "";
                    countryTextBox.Text = "";
                    proffesionTextBox.Text = "";
                    pointsTextBox.Text = "";
                    swimTimeTextBox.Value = new TimeSpan();
                    swimDistanceTextBox.Value = 0;
                    runTimeTextBox.Value = new TimeSpan();
                    runDistanceTextBox.Value = 0;
                    bikeTimeTextBox.Value = new TimeSpan();
                    bikeDistanceTextBox.Value = 0;
                    t1TextBox.Value = new TimeSpan();
                    t2TextBox.Value = new TimeSpan();
                    OverallTimeTextBox.Text = "";
                    commentTextBox.Text = "";
                    MessageBox.Show("Data error!");

                }

            }
            else
            {
                nameTextBox.Text = "";
                GenderRankTextBoc.Text = "";
                DivisionRankTextBox.Value = 0;
                OverallRankTextBox.Text = "";
                bibTExtBox.Value = 0;
                DivisionRankTextBox.Text = "";
                ageTextBox.Value = 0;
                ageCategoryTextBox.Text = "";
                stateTextBox.Text = "";
                countryTextBox.Text = "";
                proffesionTextBox.Text = "";
                pointsTextBox.Text = "";
                swimTimeTextBox.Value = new TimeSpan();
                swimDistanceTextBox.Value = 0;
                runTimeTextBox.Value = new TimeSpan();
                runDistanceTextBox.Value = 0;
                bikeTimeTextBox.Value = new TimeSpan();
                bikeDistanceTextBox.Value = 0;
                t1TextBox.Value = new TimeSpan();
                t2TextBox.Value = new TimeSpan();
                OverallTimeTextBox.Text = "";
                commentTextBox.Text = "";
            }
        }

        private async void AddResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (tkCompetition >= 0)
            {
                Results rezultat = new Results();
                rezultat._Name = nameTextBox.Text;
                rezultat.GenderRank = GenderRankTextBoc.Text;
                rezultat.DivRank = DivisionRankTextBox.Text;
                rezultat.OverallRank = OverallRankTextBox.Text;
                rezultat.Bib = bibTExtBox.Value.ToString();
                rezultat.Division = rezultatDivision.Text;
                rezultat.Age = ageTextBox.Value.ToString();
                rezultat.AgeCategory = ageCategoryTextBox.Text;
                rezultat.State = stateTextBox.Text;
                rezultat.Country = countryTextBox.Text;
                rezultat.Profession = proffesionTextBox.Text;
                rezultat.Points = pointsTextBox.Text;
                rezultat.SwimTime = swimTimeTextBox.Value.ToString();
                rezultat.SwimDistance = String.Format($"{swimDistanceTextBox.Value.ToString().Replace('.', ',')} km");
                rezultat.RunTime = runTimeTextBox.Value.ToString();
                rezultat.RunDistance = String.Format($"{runDistanceTextBox.Value.ToString().Replace('.', ',')} km");
                rezultat.BikeTime = bikeTimeTextBox.Value.ToString();
                rezultat.BikeDistance = String.Format($"{bikeDistanceTextBox.Value.ToString().Replace('.', ',')} km");
                rezultat.T1 = t1TextBox.Value.ToString();
                rezultat.T2 = t2TextBox.Value.ToString();
                rezultat.OverallTime = OverallTimeTextBox.Text;
                rezultat.Comments = commentTextBox.Text;
                rezultat.TK_Competition = tkCompetition;

                await ResultsHandler.Post(rezultat);
                ReloadItems();

                nameTextBox.Text = "";
                GenderRankTextBoc.Text = "";
                DivisionRankTextBox.Value = 0;
                OverallRankTextBox.Text = "";
                bibTExtBox.Value = 0;
                DivisionRankTextBox.Text = "";
                ageTextBox.Value = 0;
                ageCategoryTextBox.Text = "";
                stateTextBox.Text = "";
                countryTextBox.Text = "";
                proffesionTextBox.Text = "";
                pointsTextBox.Text = "";
                swimTimeTextBox.Value = new TimeSpan();
                swimDistanceTextBox.Value = 0;
                runTimeTextBox.Value = new TimeSpan();
                runDistanceTextBox.Value = 0;
                bikeTimeTextBox.Value = new TimeSpan();
                bikeDistanceTextBox.Value = 0;
                t1TextBox.Value = new TimeSpan();
                t2TextBox.Value = new TimeSpan();
                OverallTimeTextBox.Text = "";
                commentTextBox.Text = "";
            }
            else MessageBox.Show("V zavihku Tekmovanja je potrebno označiti tekmovanje, kateremu bo dodeljen rezultat!");
        }

        private async void EditResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultsList.SelectedIndex >= 0)
            {
                Results rezultat = (Results)ResultsList.SelectedItem;
                rezultat._Name = nameTextBox.Text;
                rezultat.GenderRank = GenderRankTextBoc.Text;
                rezultat.DivRank = DivisionRankTextBox.Text;
                rezultat.OverallRank = OverallRankTextBox.Text;
                rezultat.Bib = bibTExtBox.Value.ToString();
                rezultat.Division = rezultatDivision.Text;
                rezultat.Age = ageTextBox.Value.ToString();
                rezultat.AgeCategory = ageCategoryTextBox.Text;
                rezultat.State = stateTextBox.Text;
                rezultat.Country = countryTextBox.Text;
                rezultat.Profession = proffesionTextBox.Text;
                rezultat.Points = pointsTextBox.Text;
                rezultat.SwimTime= swimTimeTextBox.Value.ToString();
                rezultat.SwimDistance = String.Format($"{swimDistanceTextBox.Value.ToString().Replace('.', ',')} km");
                rezultat.RunTime = runTimeTextBox.Value.ToString();
                rezultat.RunDistance = String.Format($"{runDistanceTextBox.Value.ToString().Replace('.', ',')} km");
                rezultat.BikeTime = bikeTimeTextBox.Value.ToString();
                rezultat.BikeDistance = String.Format($"{bikeDistanceTextBox.Value.ToString().Replace('.', ',')} km");
                rezultat.T1 = t1TextBox.Value.ToString();
                rezultat.T2 = t2TextBox.Value.ToString();
                rezultat.OverallTime = OverallTimeTextBox.Text;
                rezultat.Comments = commentTextBox.Text;

                await ResultsHandler.Update(rezultat);
                ReloadItems();

                nameTextBox.Text = "";
                GenderRankTextBoc.Text = "";
                DivisionRankTextBox.Value = 0;
                OverallRankTextBox.Text = "";
                bibTExtBox.Value = 0;
                DivisionRankTextBox.Text = "";
                ageTextBox.Value = 0;
                ageCategoryTextBox.Text = "";
                stateTextBox.Text = "";
                countryTextBox.Text = "";
                proffesionTextBox.Text = "";
                pointsTextBox.Text = "";
                swimTimeTextBox.Value = new TimeSpan();
                swimDistanceTextBox.Value = 0;
                runTimeTextBox.Value = new TimeSpan();
                runDistanceTextBox.Value = 0;
                bikeTimeTextBox.Value = new TimeSpan();
                bikeDistanceTextBox.Value = 0;
                t1TextBox.Value = new TimeSpan();
                t2TextBox.Value = new TimeSpan();
                OverallTimeTextBox.Text = "";
                commentTextBox.Text = "";

            }
            else MessageBox.Show("Noben rezultat za urejanje ni označen!");
        }

        private async void DeleteResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultsList.SelectedIndex >= 0)
            {
                var potrditev = MessageBox.Show("Ste prepričani, da želite izbrisati rezultat? Podatkov po izbrisu ni več možno obnoviti!", "Opozorilo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (potrditev == MessageBoxResult.Yes)
                {
                    Results rezultat = (Results)ResultsList.SelectedItem;

                    await ResultsHandler.Delete(rezultat.ID_Result);
                    ReloadItems();
                }
            }
            else MessageBox.Show("Noben rezultat ni označen!");
        }

        private void competitionWindowButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private async void searchResultsButton_Click(object sender, RoutedEventArgs e)
        {
            if (searchResults.Text.Trim() == "")
            {
                ReloadItems();
            }
            else
            {
                ResultsList.ItemsSource = null;
                List<Results> zadetki = new List<Results>();

                List<Results> vsiRezultati = await ResultsHandler.GetAllFromCompetition(tkCompetition);

                foreach (var t in vsiRezultati)
                {
                    if (t.SearchTerm.ToUpper().Contains(searchResults.Text.Trim().ToUpper()))
                    {
                        zadetki.Add(t);
                    }
                }

                ResultsList.ItemsSource = zadetki;
            }
        }
    }
}
