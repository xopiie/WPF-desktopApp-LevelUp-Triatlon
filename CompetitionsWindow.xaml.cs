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
using static WPF_admin.Results;

namespace WPF_admin
{
    /// <summary>
    /// Interaction logic for CompetitionsWindow.xaml
    /// </summary>
    public partial class CompetitionsWindow : Page
    {
        public CompetitionsWindow()
        {
            InitializeComponent();
            ReloadItems();
        }

        private async void ReloadItems()
        {
            CompetitionsList.ItemsSource = "";
            CompetitionsList.ItemsSource = await CompetitionHandler.GetAll();
        }

        private void CloseCompetitionMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenCompetitionMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CompetitionTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void AddCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionTypeComboBox.SelectedIndex != -1 && CompetitionGenderComboBox.SelectedIndex != -1 && RegionTextBlock.Text.Trim().Length > 1 && YearTextBlock.Value >= 1950 && YearTextBlock.Value <= DateTime.Now.Year)
            {
                Competitions tekmovanje = new Competitions();
                tekmovanje.CompetitionType = CompetitionTypeComboBox.Text;
                tekmovanje.Gender = CompetitionGenderComboBox.Text;
                tekmovanje.Location = RegionTextBlock.Text.Trim();
                tekmovanje.CompetitionYear = YearTextBlock.Value.ToString();

                await CompetitionHandler.Post(tekmovanje);
                CompetitionTypeComboBox.SelectedIndex = 0;
                CompetitionGenderComboBox.SelectedIndex = 0;
                RegionTextBlock.Text = "";
                YearTextBlock.Value = null;

                ReloadItems();
            }
            else MessageBox.Show("Please check the data you have entered.");
        }

        private async void EditCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionsList.SelectedIndex >= 0)
            {
                if (CompetitionTypeComboBox.SelectedIndex != -1 && CompetitionGenderComboBox.SelectedIndex != -1 && RegionTextBlock.Text.Trim().Length > 1 && YearTextBlock.Value >= 1950 && YearTextBlock.Value <= DateTime.Now.Year)
                {
                    Competitions tekmovanje = (Competitions)CompetitionsList.SelectedItem;
                    tekmovanje.CompetitionType = CompetitionTypeComboBox.Text;
                    tekmovanje.Gender = CompetitionGenderComboBox.Text;
                    tekmovanje.Location = RegionTextBlock.Text.Trim();
                    tekmovanje.CompetitionYear = YearTextBlock.Value.ToString();

                    await CompetitionHandler.Update(tekmovanje);
                    CompetitionTypeComboBox.SelectedIndex = 0;
                    CompetitionGenderComboBox.SelectedIndex = 0;
                    RegionTextBlock.Text = "";
                    YearTextBlock.Value = null;

                    ReloadItems();
                }

                else MessageBox.Show("Please check the data you have entered.");

            }


            else MessageBox.Show("Please select a competition.");
        }

        private async void DeleteCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionsList.SelectedIndex >= 0)
            {
                var potrditev = MessageBox.Show("Brisnje tekmovanja odstrani tudi vse rezultate, ki so s tem tekmovanjem povezani!\n\nSte prepričani, da želite nadaljevati? Po izbrisu podatkov več ni možno obnoviti!", "Opozorilo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (potrditev == MessageBoxResult.Yes)
                {
                    Competitions tekmovanje = (Competitions)CompetitionsList.SelectedItem;

                    await CompetitionHandler.Delete(tekmovanje.ID_Competition);
                    CompetitionTypeComboBox.SelectedIndex = 0;
                    CompetitionGenderComboBox.SelectedIndex = 0;
                    RegionTextBlock.Text = "";
                    YearTextBlock.Value = null;
                    ReloadItems();
                }
            }
            else MessageBox.Show("Nobeno tekmovanje ni označeno!");
        }

        private async void CompetitionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CompetitionsList.SelectedIndex >= 0)
            {
                Competitions tekmovanje = (Competitions)CompetitionsList.SelectedItem;
                ResultsWindow.tkCompetition = tekmovanje.ID_Competition;

                List<Results> r = new List<Results>();
                r = await ResultsHandler.GetAllFromCompetition(ResultsWindow.tkCompetition);

                foreach (var result in r)
                {
                    ResultsWindow.results.Add(result);
                }

                int tipIndex = 0;
                if (tekmovanje.CompetitionType == "im") tipIndex = 0;
                else if (tekmovanje.CompetitionType == "im703") tipIndex = 1;
                else if (tekmovanje.CompetitionType == "Double") tipIndex = 2;
                else if (tekmovanje.CompetitionType == "Triple") tipIndex = 3;

                int spolIndex = 0;
                if (tekmovanje.Gender == "ManWoman") spolIndex = 0;
                else if (tekmovanje.Gender == "Man") spolIndex = 1;
                else if (tekmovanje.Gender == "Woman") spolIndex = 2;



                CompetitionTypeComboBox.SelectedIndex = tipIndex;
                CompetitionGenderComboBox.SelectedIndex = spolIndex;
                RegionTextBlock.Text = tekmovanje.Location;
                YearTextBlock.Value = int.Parse(tekmovanje.CompetitionYear);

            }
            else
            {
                ResultsWindow.tkCompetition = -1;

                ResultsWindow.results.Clear();

                CompetitionTypeComboBox.SelectedIndex = 0;
                CompetitionGenderComboBox.SelectedIndex = 0;
                RegionTextBlock.Text = "";
                YearTextBlock.Text = null;
            }
        }

        private async void searchCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            if (searchCompetition.Text.Trim() == "")
            {
                ReloadItems();
            }
            else
            {
                CompetitionsList.ItemsSource = null;
                List<Competitions> zadetki = new List<Competitions>();

                List<Competitions> vsiRezultati = await CompetitionHandler.GetAll();

                foreach (var t in vsiRezultati)
                {
                    if (t.SearchTerm.ToUpper().Contains(searchCompetition.Text.Trim().ToUpper()))
                    {
                        zadetki.Add(t);
                    }
                }

                CompetitionsList.ItemsSource = zadetki;
            }
        }
    }
}

