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

namespace WPF_admin
{
    /// <summary>
    /// Interaction logic for EventsWindow.xaml
    /// </summary>
    public partial class EventsWindow : Page
    {
        public EventsWindow()
        {
            InitializeComponent();
            ReloadItems();
        }

        private void CloseEventMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenEventMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (eventTitleTextBox.Text != ""  && summaryTextBox.Text != "" && eventDateTextBox.Value.HasValue)
            {
                UpcomingEvents upcomingEvent = new UpcomingEvents();
                upcomingEvent.Title = eventTitleTextBox.Text;
                upcomingEvent.Summary = summaryTextBox.Text;
                upcomingEvent.EventDate = (DateTime)eventDateTextBox.Value;

                await UpcomingEventsHandler.Post(upcomingEvent);
                eventTitleTextBox.Text = "";
                summaryTextBox.Text = "";
                eventDateTextBox.Value = null;

                ReloadItems();
            }
            else MessageBox.Show("Please check the data you have entered.");
        }

        private async void EditEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventsList.SelectedIndex >= 0)
            {
                if (eventTitleTextBox.Text != "" && summaryTextBox.Text != "" && eventDateTextBox.Value.HasValue)
                {
                    UpcomingEvents upcomingEvent = (UpcomingEvents)EventsList.SelectedItem;
                    upcomingEvent.Title = eventTitleTextBox.Text;
                    upcomingEvent.Summary = summaryTextBox.Text;
                    upcomingEvent.EventDate = (DateTime)eventDateTextBox.Value;

                    await UpcomingEventsHandler.Update(upcomingEvent);
                    eventTitleTextBox.Text = "";
                    summaryTextBox.Text = "";
                    eventDateTextBox.Value = null;

                    ReloadItems();
                }

                else MessageBox.Show("Please check the data you have entered.");

            }


            else MessageBox.Show("Please select an event.");
        }

        private async void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventsList.SelectedIndex >= 0)
            {
                var potrditev = MessageBox.Show("Brisnje tekmovanja odstrani tudi vse rezultate, ki so s tem tekmovanjem povezani!\n\nSte prepričani, da želite nadaljevati? Po izbrisu podatkov več ni možno obnoviti!", "Opozorilo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (potrditev == MessageBoxResult.Yes)
                {
                    UpcomingEvents upcomingEvent = (UpcomingEvents)EventsList.SelectedItem;

                    await UpcomingEventsHandler.Delete(upcomingEvent.ID);
                    eventTitleTextBox.Text = "";
                    eventDateTextBox.Value = null;
                    summaryTextBox.Text = "";
                    ReloadItems();
                }
            }
            else MessageBox.Show("Nobeno tekmovanje ni označeno!");
        }

        private async void ReloadItems()
        {
            EventsList.ItemsSource = "";
            EventsList.ItemsSource = await UpcomingEventsHandler.GetAll();
        }

        private void EventsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EventsList.SelectedIndex >= 0)
            {
                UpcomingEvents upcomingEvent = (UpcomingEvents)EventsList.SelectedItem;

                eventTitleTextBox.Text = upcomingEvent.Title;
                eventDateTextBox.Value = upcomingEvent.EventDate;
                summaryTextBox.Text = upcomingEvent.Summary;
            } 
            else
            {
                eventTitleTextBox.Text = "";
                eventDateTextBox.Value = null;
                summaryTextBox.Text = "";
            }
            
        }

        private async void searchEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (searchEvent.Text.Trim() == "")
            {
                ReloadItems();
            }
            else
            {
                EventsList.ItemsSource = null;
                List<UpcomingEvents> zadetki = new List<UpcomingEvents>();

                List<UpcomingEvents> vsiRezultati = await UpcomingEventsHandler.GetAll();

                foreach (var t in vsiRezultati)
                {
                    if (t.SearchTerm.ToUpper().Contains(searchEvent.Text.Trim().ToUpper()))
                    {
                        zadetki.Add(t);
                    }
                }

                EventsList.ItemsSource = zadetki;
            }
        }
    }
}
