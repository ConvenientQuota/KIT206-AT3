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

        public static string ToString(EmployeeLevel level)
        {
            switch (level)
            {
                case EmployeeLevel.A:
                    return "A123";
                case EmployeeLevel.B:
                    return "B123";
                case EmployeeLevel.C:
                    return "C123";
                case EmployeeLevel.D:
                    return "12D";
                case EmployeeLevel.E:
                    return "E123";
                case EmployeeLevel.Student:
                    return "Student";
                default:
                    return "Unknown";
            }
        }

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
                if (ResearcherListView.SelectedItem != null)
                {
                    List<Publication> publications = (List<Publication>)PublicationControllers.SearchByResearcher((Researcher)ResearcherListView.SelectedItem);
                    List<Publication> sortedPublications = SortPublicationsByRecentFirst(publications);
                    ObservableCollection<Publication> publist = new ObservableCollection<Publication>(sortedPublications);
                    return publist;
                }
                else
                {
                    return null;
                }
            }
        }

        public static List<Publication> SortPublicationsByRecentFirst(List<Publication> publications)
        {
            if (publications == null || publications.Count == 0)
            {
                return publications;
            }

            List<Publication> sortedPublications = publications.OrderByDescending(pub => pub.Year).ThenBy(pub => pub.Title).ToList();

            return sortedPublications;
        }


        private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ResearcherDetails.DataContext = e.AddedItems[0];
                string name = ((Researcher)e.AddedItems[0]).Name;

                // Get the publications for the researcher
                List<Publication> publications = PublicationControllers.ResearchersPublications(name);

                // Sort the publications by year and title
                List<Publication> sortedPublications = SortPublicationsByRecentFirst(publications);

                // Set the sorted publications as the item source
                PublicationListView.ItemsSource = sortedPublications;

                var photo = new Image();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = (e.AddedItems[0] as Researcher).Photo;
                bitmap.EndInit();
                ImageSource imageSource = bitmap;

                ResearcherPhoto.Source = imageSource;
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
                    PublicationDetails.DataContext = null;
                }
                else if (e.AddedItems[0].ToString().EndsWith("A"))
                {
                    ResearcherListView.ItemsSource = ResearcherController.awilterByType(EmployeeLevel.A, Researcher);
                    PublicationListView.ItemsSource = null;
                    ResearcherDetails.DataContext = null;
                    PublicationDetails.DataContext = null;
                }
                else if(e.AddedItems[0].ToString().EndsWith("B"))
                {
                    ResearcherListView.ItemsSource = ResearcherController.awilterByType(EmployeeLevel.B, Researcher);
                    PublicationListView.ItemsSource = null;
                    ResearcherDetails.DataContext = null;
                    PublicationDetails.DataContext = null;
                }
                else if (e.AddedItems[0].ToString().EndsWith("C"))
                {
                    ResearcherListView.ItemsSource = ResearcherController.awilterByType(EmployeeLevel.C, Researcher);
                    PublicationListView.ItemsSource = null;
                    ResearcherDetails.DataContext = null;
                    PublicationDetails.DataContext = null;
                }
                else if (e.AddedItems[0].ToString().EndsWith("D"))
                {
                    ResearcherListView.ItemsSource = ResearcherController.awilterByType(EmployeeLevel.D, Researcher);
                    PublicationListView.ItemsSource = null;
                    ResearcherDetails.DataContext = null;
                    PublicationDetails.DataContext = null;
                }
                else if (e.AddedItems[0].ToString().EndsWith("E"))
                {
                    ResearcherListView.ItemsSource = ResearcherController.awilterByType(EmployeeLevel.E, Researcher);
                    PublicationListView.ItemsSource = null;
                    ResearcherDetails.DataContext = null;
                    PublicationDetails.DataContext = null;
                }
                else
                {
                    ResearcherListView.ItemsSource = Researcher;
                    PublicationListView.ItemsSource = null;
                    ResearcherDetails.DataContext = null;
                    PublicationDetails.DataContext = null;
                }
            }
        }

        private void PublicationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                PublicationDetails.DataContext = e.AddedItems[0];
                Console.WriteLine(((Publication)e.AddedItems[0]).Title);
            }
        }
    }
}
