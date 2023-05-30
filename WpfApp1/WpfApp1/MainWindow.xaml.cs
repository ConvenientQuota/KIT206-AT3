using System;
using System.Collections.Generic;
using AT3.Controllers;
using AT3.Entity;
using AT3.DataSources;
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
using System.Collections.ObjectModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

        }

        private List<Researcher> researchers;

        public List<Researcher> Researchers
        {
            get
            {
                researchers = ResearcherController.LoadResearchers();
                return researchers;
            }
        }

        private List<Publication> publications;

        public List<Publication> Publications
        {
            get
            {
                publications = PublicationControllers.LoadPublications();
                return publications;
            }
        }

        public ObservableCollection<Researcher> Re
        {
            get { return new ObservableCollection<Researcher>(DbAdaptor.LoadAll()); }
        }

        private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void ResearcherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null)
            {
                if (e.AddedItems[0].ToString().EndsWith("Student"))
                {
                    ResearcherListView.ItemsSource = ResearcherController.FilterByType(true, Re);
                }
                else if (e.AddedItems[0].ToString().EndsWith("Staff"))
                {
                    ResearcherListView.ItemsSource = ResearcherController.FilterByType(false, Re);
                }
                else
                {
                    ResearcherListView.ItemsSource = Re;
                }
            }
        }

        private void PublicationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PublicationListView.SelectedItem is Researcher researcher)
            {
                List<Publication> publications = PublicationControllers.ResearchersPublications(researcher.Name);

                if (publications.Count > 0)
                {
                    Console.WriteLine("\nPublications for Researcher: " + researcher.Name + "\n");
                    foreach (Publication publication in publications)
                    {
                        Console.WriteLine(publication.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("\nNo publications found for Researcher: " + researcher.Name + "\n");
                }
            }

        }
    }
}
