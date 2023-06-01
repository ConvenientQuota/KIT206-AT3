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
                Researcher selectedResearcher = (Researcher)e.AddedItems[0];
                string name = selectedResearcher.Name;

                // Get the publications for the researcher
                List<Publication> publications = PublicationControllers.ResearchersPublications(name);
                UpdateResearcher(selectedResearcher, publications);

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

        public void UpdateResearcher(Researcher selectedResearcher, List<Publication> publications)
        {
            int ThreeYearCount = 0;
            int Q1Count = 0;
            double ThreeYA;
            double Q1P;
            double PP; 
            foreach (Publication publication in publications)
            {
                // count the avg number of publications in 3 year
                if (publication.Year >= DateTime.Now.Year - 3)
                {
                    ThreeYearCount++;
                }

                // count Q1 number of publication percentage
                if (publication.Ranking == OutputRanking.Q1) {
                    Q1Count++;
                }
            }

<<<<<<< HEAD
            selectedResearcher.ThreeYearAverage = Math.Round((double)ThreeYearCount / 3.0, 2);
            selectedResearcher.Q1Percentage = (int)((double)Q1Count / publications.Count * 100);
            selectedResearcher.performancePublication = Math.Round((double)publications.Count / selectedResearcher.Tenure, 2);
=======
            ThreeYA = selectedResearcher.ThreeYearAverage = Math.Round((double)ThreeYearCount / 3.0, 2);
            Q1P = selectedResearcher.Q1Percentage = (double)Q1Count / publications.Count * 100;
            PP = selectedResearcher.performancePublication = Math.Round((double)publications.Count / selectedResearcher.Tenure, 2);

            // TODO: other stuff


>>>>>>> 87501a4f4079fdd056a1b4fa159e9bd37edb4e29
        }
    }
}
