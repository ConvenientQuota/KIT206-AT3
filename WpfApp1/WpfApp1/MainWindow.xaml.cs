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
                //publications = PublicationControllers.LoadPublications();
                return null;
            }
        }

        public ObservableCollection<Researcher> Researcher
        {
            get { return new ObservableCollection<Researcher>(ResearcherController.LoadResearchers()); }
        }

        public ObservableCollection<Publication> Publication
        {
            get
            {
                if(ResearcherListView.SelectedItem != null)
                {
                    List<Publication> publications = (List<Publication>)PublicationControllers.SearchByResearcher((Researcher)ResearcherListView.SelectedItem);
                    ObservableCollection<Publication> publist = new ObservableCollection<Publication>(publications);
                    return publist;
                }
                else
                {
                    return null;
                }
            }
        }


        private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                ResearcherDetails.DataContext = e.AddedItems[0];
                String name = ((Researcher)e.AddedItems[0]).Name;
                // get the publications for the researcher
                List<Publication> publications = PublicationControllers.ResearchersPublications(name);
                PublicationListView.ItemsSource = publications;
            }

        }

        private void ResearcherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null)
            {
                if (e.AddedItems[0].ToString().EndsWith("Student"))
                {
                    ResearcherListView.ItemsSource = ResearcherController.FilterByType(true, Researcher);
                    PublicationListView.ItemsSource = null;
                    ResearcherDetails.DataContext = null;
                }
                else if (e.AddedItems[0].ToString().EndsWith("Staff"))
                {
                    ResearcherListView.ItemsSource = ResearcherController.FilterByType(false, Researcher);
                    PublicationListView.ItemsSource = null;
                    ResearcherDetails.DataContext = null;
                }
                else
                {
                    ResearcherListView.ItemsSource = Researcher;
                    PublicationListView.ItemsSource = null;
                    ResearcherDetails.DataContext = null;
                }
            }
        }

        private void PublicationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResearcherComboBox.SelectedItem is Researcher selectedResearcher)
            {
                string researcherName = selectedResearcher.Name; // Name of selected researcher 

                 List<Publication> researcherPublications = PublicationControllers.ResearchersPublications(researcherName); // Prints out publications of the selected researcher

             } 
        }
    }
}
