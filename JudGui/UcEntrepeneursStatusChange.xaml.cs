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
    /// Interaction logic for UcEntrepeneursStatusChange.xaml
    /// </summary>
    public partial class UcEntrepeneursStatusChange : UserControl
    {
        #region Fields
        public Bizz CBZ;
        public UserControl UcMain;

        public List<Entrepeneur> FilteredEntrepeneurs = new List<Entrepeneur>();

        #endregion

        #region Constructors
        public UcEntrepeneursStatusChange(Bizz cbz, UserControl ucMain)
        {
            InitializeComponent();
            this.CBZ = cbz;
            this.UcMain = ucMain;
        }
        public UcEntrepeneursStatusChange()
        {
            InitializeComponent();
        }

        #endregion

        #region Buttons
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (CBZ.UcMainEdited)
            {
                //Warning about lost changes before closing
                if (MessageBox.Show("Vil du lukke redigering af Entrepenører? Alle ugemte data mistes.", "Entrepenører", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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
            bool result = UpdateEntrepeneurInDb;

            //Display result
            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Entrepenøren blev opdateret", "Entrepenører", MessageBoxButton.OK, MessageBoxImage.Information);

                //Reset Boxes
                ListBoxEntrepeneurs.SelectedIndex = 0;
                CheckBoxActive.IsChecked = null;

                //Refresh Entrepeneurs list
                CBZ.RefreshList("Entrepeneurs");
                CBZ.TempEntrepeneur = new Entrepeneur();
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Entrepenøren blev ikke opdateret. Prøv igen.", "Entrepenører", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Events
        private void CheckBoxActive_Checked(object sender, RoutedEventArgs e)
        {
            if (CBZ.TempBuilder.Active && CheckBoxActive.IsChecked == false)
            {
                CBZ.TempBuilder.ToggleActive();
            }
            else if (!CBZ.TempBuilder.Active && CheckBoxActive.IsChecked == true)
            {
                CBZ.TempBuilder.ToggleActive();
            }


            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void ListBoxEntrepeneurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBZ.TempEntrepeneur = new Entrepeneur((Entrepeneur)ListBoxEntrepeneurs.SelectedItem);
            if (CBZ.TempEntrepeneur.Active)
            {
                CheckBoxActive.IsChecked = true;
            }
            else
            {
                CheckBoxActive.IsChecked = false;
            }


            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetFilteredEntrepeneurs();
            ListBoxEntrepeneurs.SelectedIndex = -1;
            ListBoxEntrepeneurs.ItemsSource = "";
            ListBoxEntrepeneurs.ItemsSource = this.FilteredEntrepeneurs;

        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that retrieves a list of filtered Categories for ListBoxCraftCategories
        /// </summary>
        private void GetFilteredEntrepeneurs()
        {
            int length = TextBoxSearch.Text.Length;

            if (length > 0)
            {
                CBZ.RefreshList("Entrepeneurs");
                this.FilteredEntrepeneurs.Clear();
                foreach (Entrepeneur entrepeneur in CBZ.Entrepeneurs)
                {
                    if (entrepeneur.Entity.Name.Remove(length).ToLower() == TextBoxSearch.Text.ToLower())
                    {
                        this.FilteredEntrepeneurs.Add(entrepeneur);
                    }
                }

            }
            else
            {
                CBZ.RefreshList("Entrepeneurs");
                this.FilteredEntrepeneurs.Clear();
                foreach (Entrepeneur entrepeneur in CBZ.Entrepeneurs)
                {
                    this.FilteredEntrepeneurs.Add(entrepeneur);
                }
            }
        }

        /// <summary>
        /// Method, that creates an Entrepeneur in Db
        /// </summary>
        /// <returns>bool</returns>
        private bool UpdateEntrepeneurInDb => CBZ.UpdateInDb(CBZ.TempEntrepeneur);

        #endregion

    }
}
