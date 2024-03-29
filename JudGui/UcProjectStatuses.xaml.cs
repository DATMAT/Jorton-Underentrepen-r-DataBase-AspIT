﻿using JudBizz;
using JudRepository;
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

namespace JudGui
{
    /// <summary>
    /// Interaction logic for UcProjectStatuses.xaml
    /// </summary>
    public partial class UcProjectStatuses : UserControl
    {
        #region Fields
        public Bizz CBZ;
        public UserControl UcMain;
        public ProjectStatus TempNewProjectStatus = new ProjectStatus();

        List<IndexedProjectStatus> FilteredProjectStatuses = new List<IndexedProjectStatus>();
        #endregion

        #region Constructors
        public UcProjectStatuses(Bizz cbz, UserControl ucMain)
        {
            InitializeComponent();
            this.CBZ = cbz;
            this.UcMain = ucMain;

            CBZ.TempProjectStatus = new ProjectStatus();

            GetFilteredProjectStatuses();
            ListBoxProjectStatuses.ItemsSource = FilteredProjectStatuses;
            ListBoxProjectStatuses.SelectedIndex = -1;

        }
        public UcProjectStatuses()
        {
            InitializeComponent();
            

        }

        #endregion

        #region Buttons
        private void ButtonAddProjectStatus_Click(object sender, RoutedEventArgs e)
        {
            bool result = CreateProjectStatusInDb();

            //Display result
            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Projektstatussen blev tilføjet", "Projektstatusser", MessageBoxButton.OK, MessageBoxImage.Information);

                //Reset Boxes
                ListBoxProjectStatuses.SelectedIndex = -1;
                ListBoxProjectStatuses.ItemsSource = "";
                CBZ.RefreshIndexedList("ProjectStatuses");
                ListBoxProjectStatuses.ItemsSource = CBZ.IndexedCraftGroups;
                TextBoxProjectStatusSearch.Text = "";
                TextBoxText.Text = "";
                TextBoxNewText.Text = "";

                //Refresh Users list
                CBZ.RefreshList("ProjectStatuses");
                CBZ.TempProjectStatus = new ProjectStatus();
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Projektstatussen blev ikke tilføjet. Prøv igen.", "Projektstatusser", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (CBZ.UcMainEdited)
            {
                //Warning about lost changes before closing
                if (MessageBox.Show("Vil du lukke Projektstatusser? Alle ugemte data mistes.", "Projektstatusser", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    CBZ.CloseUcMain(UcMain);
                }
            }
            else
            {
                CBZ.CloseUcMain(UcMain);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            bool result = UpdateProjectStatusInDb;

            //Display result
            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Projektstatussen blev opdateret", "Projektstatusser", MessageBoxButton.OK, MessageBoxImage.Information);

                //Reset Boxes
                ListBoxProjectStatuses.SelectedIndex = -1;
                ListBoxProjectStatuses.ItemsSource = "";
                TextBoxProjectStatusSearch.Text = "";
                TextBoxText.Text = "";
                TextBoxNewText.Text = "";

                //Refresh Users list
                CBZ.RefreshList("ProjectStatuses");
                CBZ.TempProjectStatus = new ProjectStatus();
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Projektstatussen blev ikke opdateret. Prøv igen.", "Projektstatusser", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        #endregion

        #region Events
        private void ListBoxProjectStatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBZ.TempProjectStatus = new ProjectStatus((ProjectStatus)ListBoxProjectStatuses.SelectedItem);

            TextBoxText.Text = CBZ.TempProjectStatus.Text;

            this.TempNewProjectStatus = new IndexedProjectStatus();
            TextBoxNewText.Text = "";

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxNewText_TextChanged(object sender, TextChangedEventArgs e)
        {
            TempNewProjectStatus.Text = TextBoxNewText.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxProjectStatusSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetFilteredProjectStatuses();
            ListBoxProjectStatuses.SelectedIndex = -1;
            ListBoxProjectStatuses.ItemsSource = "";
            ListBoxProjectStatuses.ItemsSource = this.FilteredProjectStatuses;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxText_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempProjectStatus.Text = TextBoxText.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates a Project Status in Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CreateProjectStatusInDb()
        {
            bool result = false;

            int projectStatusId = CBZ.CreateInDb(TempNewProjectStatus);

            if (projectStatusId >= 1)
            {
                result = true;
            }

            return result;

        }

        /// <summary>
        /// Method, that retrieves a list of filtered Project Statusses for ListBoxProjectStatuses
        /// </summary>
        private void GetFilteredProjectStatuses()
        {
            CBZ.RefreshIndexedList("ProjectStatuses");
            this.FilteredProjectStatuses = new List<IndexedProjectStatus>();
            int length = TextBoxProjectStatusSearch.Text.Length;

            foreach (IndexedProjectStatus status in CBZ.IndexedProjectStatuses)
            {
                if (status.Text == TextBoxProjectStatusSearch.Text)
                {
                    this.FilteredProjectStatuses.Add(status);
                }
            }
        }

        /// <summary>
        /// Method, that updates an Craft Group in Db
        /// </summary>
        /// <returns>bool</returns>
        private bool UpdateProjectStatusInDb => CBZ.UpdateInDb(CBZ.TempProjectStatus);

        #endregion

    }
}
