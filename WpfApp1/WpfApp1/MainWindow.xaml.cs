﻿using System;
using System.Collections.Generic;
using AT3.Controllers;
using AT3.Entity;
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

        private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}