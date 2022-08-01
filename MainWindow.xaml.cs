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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuButton.Visibility = Visibility.Collapsed;
            CloseMenuButton.Visibility = Visibility.Visible;
        }

        private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuButton.Visibility = Visibility.Visible;
            CloseMenuButton.Visibility = Visibility.Collapsed;
        }

        private void CompetitionsButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ResultsFrame.Visibility == Visibility.Visible || EventsFrame.Visibility == Visibility.Visible)
            {
                ResultsFrame.Visibility = Visibility.Hidden;
                EventsFrame.Visibility = Visibility.Hidden;
                CompetitionsFrame.Visibility = Visibility.Visible;
                CompetitionsFrame.Navigate(new System.Uri("CompetitionsWindow.xaml", UriKind.RelativeOrAbsolute));
            }
            CompetitionsFrame.Visibility = Visibility.Visible;
            CompetitionsFrame.Navigate(new System.Uri("CompetitionsWindow.xaml", UriKind.RelativeOrAbsolute));
        }

        private void HomeButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ResultsFrame.Visibility == Visibility.Visible || EventsFrame.Visibility == Visibility.Visible || CompetitionsFrame.Visibility == Visibility.Visible)
            {
                CompetitionsFrame.Visibility = Visibility.Hidden;
                ResultsFrame.Visibility = Visibility.Hidden;
                EventsFrame.Visibility = Visibility.Hidden;
              
            }
        }

        private void ResultsMenuButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(CompetitionsFrame.Visibility == Visibility.Visible || EventsFrame.Visibility == Visibility.Visible)
            {
                CompetitionsFrame.Visibility = Visibility.Hidden;
                EventsFrame.Visibility = Visibility.Hidden;
                ResultsFrame.Visibility = Visibility.Visible;
                ResultsFrame.Navigate(new System.Uri("ResultsWindow.xaml", UriKind.RelativeOrAbsolute));
            }

            ResultsFrame.Visibility = Visibility.Visible;
            ResultsFrame.Navigate(new System.Uri("ResultsWindow.xaml", UriKind.RelativeOrAbsolute));
        }

        private void EventsMenuButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CompetitionsFrame.Visibility == Visibility.Visible || ResultsFrame.Visibility == Visibility.Visible)
            {
                CompetitionsFrame.Visibility = Visibility.Hidden;
                ResultsFrame.Visibility = Visibility.Hidden;
                EventsFrame.Visibility = Visibility.Visible;
                EventsFrame.Navigate(new System.Uri("EventsWindow.xaml", UriKind.RelativeOrAbsolute));
            }
            EventsFrame.Visibility = Visibility.Visible;
            EventsFrame.Navigate(new System.Uri("EventsWindow.xaml", UriKind.RelativeOrAbsolute));
        }

        private void EventsMenuButton_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
