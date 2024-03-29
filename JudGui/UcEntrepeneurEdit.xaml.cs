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
    /// Interaction logic for UcEntrepeneurEdit.xaml
    /// </summary>
    public partial class UcEntrepeneurEdit : UserControl
    {
        #region Fields
        public Bizz CBZ;
        public UserControl UcMain;
        public CraftGroup CCG = new CraftGroup();

        public List<IndexedEntrepeneur> FilteredEntrepeneurs = new List<IndexedEntrepeneur>();

        #endregion

        #region Constructors
        public UcEntrepeneurEdit(Bizz cbz, UserControl ucMain)
        {
            InitializeComponent();
            this.CBZ = cbz;
            this.UcMain = ucMain;

            GetFilteredEntrepeneurs();
            ListBoxEntrepeneurs.ItemsSource = FilteredEntrepeneurs;

            GenerateCraftGroupItems();
            ComboBoxRegion.ItemsSource = "";
            ComboBoxRegion.ItemsSource = CBZ.IndexedRegions;

            ComboBoxCraftGroup1.SelectedIndex = -1;
            ComboBoxCraftGroup2.SelectedIndex = -1;
            ComboBoxCraftGroup3.SelectedIndex = -1;
            ComboBoxCraftGroup4.SelectedIndex = -1;
            ComboBoxRegion.SelectedIndex = -1;

        }

        public UcEntrepeneurEdit()
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
                TextBoxName.Text = "";
                TextBoxPhone.Text = "";
                TextBoxFax.Text = "";
                TextBoxMobile.Text = "";
                TextBoxEmail.Text = "";
                ComboBoxCraftGroup1.SelectedIndex = 0;
                ComboBoxCraftGroup2.SelectedIndex = 0;
                ComboBoxCraftGroup3.SelectedIndex = 0;
                ComboBoxCraftGroup4.SelectedIndex = 0;
                ComboBoxRegion.SelectedIndex = 0;

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

        private void ButtonUpdateCvr_Click(object sender, RoutedEventArgs e)
        {
            UpdateData updatedData = new UpdateData();
            CBZ.TempEntrepeneur.Entity = updatedData.Entity;
            bool updated = CBZ.UpdateInDb(updatedData.Entity);

            if (updated)
            {
                int index = ListBoxEntrepeneurs.SelectedIndex;
                ListBoxEntrepeneurs.SelectedIndex = -1;
                GetFilteredEntrepeneurs();
                ListBoxEntrepeneurs.SelectedIndex = index;
            }
        }

        #endregion

        #region Events
        private void ComboBoxCraftGroup1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCraftGroup1.SelectedIndex >= 1)
            {
                CBZ.TempEntrepeneur.CraftGroup1 = new CraftGroup((CraftGroup)ComboBoxCraftGroup1.SelectedItem);

                //Set CBZ.UcMainEdited
                if (!CBZ.UcMainEdited)
                {
                    CBZ.UcMainEdited = true;
                }
            }
            else
            {
                //Reset CBZ.UcMainEdited
                if (CBZ.UcMainEdited)
                {
                    CBZ.UcMainEdited = false;
                }

            }
        }

        private void ComboBoxCraftGroup2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCraftGroup2.SelectedIndex >= 1)
            {
                CBZ.TempEntrepeneur.CraftGroup2 = new CraftGroup((CraftGroup)ComboBoxCraftGroup2.SelectedItem);

                //Set CBZ.UcMainEdited
                if (!CBZ.UcMainEdited)
                {
                    CBZ.UcMainEdited = true;
                }
            }
            else
            {
                //Reset CBZ.UcMainEdited
                if (CBZ.UcMainEdited)
                {
                    CBZ.UcMainEdited = false;
                }

            }

        }

        private void ComboBoxCraftGroup3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCraftGroup3.SelectedIndex >= 1)
            {
                CBZ.TempEntrepeneur.CraftGroup3 = new CraftGroup((CraftGroup)ComboBoxCraftGroup3.SelectedItem);

                //Set CBZ.UcMainEdited
                if (!CBZ.UcMainEdited)
                {
                    CBZ.UcMainEdited = true;
                }
            }
            else
            {
                //Reset CBZ.UcMainEdited
                if (CBZ.UcMainEdited)
                {
                    CBZ.UcMainEdited = false;
                }

            }

        }

        private void ComboBoxCraftGroup4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCraftGroup4.SelectedIndex >= 1)
            {
                CBZ.TempEntrepeneur.CraftGroup4 = new CraftGroup((CraftGroup)ComboBoxCraftGroup4.SelectedItem);

                //Set CBZ.UcMainEdited
                if (!CBZ.UcMainEdited)
                {
                    CBZ.UcMainEdited = true;
                }
            }
            else
            {
                //Reset CBZ.UcMainEdited
                if (CBZ.UcMainEdited)
                {
                    CBZ.UcMainEdited = false;
                }

            }

        }

        private void ComboBoxRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBZ.TempEntrepeneur.Region = new Region((Region)ComboBoxRegion.SelectedItem);

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void ListBoxEntrepeneurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBZ.TempEntrepeneur = new Entrepeneur((Entrepeneur)ListBoxEntrepeneurs.SelectedItem);
            TextBoxName.Text = CBZ.TempEntrepeneur.Entity.Name;
            TextBoxPhone.Text = CBZ.TempEntrepeneur.Entity.ContactInfo.Phone;
            TextBoxFax.Text = CBZ.TempEntrepeneur.Entity.ContactInfo.Fax;
            TextBoxMobile.Text = CBZ.TempEntrepeneur.Entity.ContactInfo.Mobile;
            TextBoxEmail.Text = CBZ.TempEntrepeneur.Entity.ContactInfo.Email;

            ComboBoxCraftGroup1.SelectedIndex = GetCraftGroupIndexFromId(CBZ.TempEntrepeneur.CraftGroup1.Id);
            ComboBoxCraftGroup2.SelectedIndex = GetCraftGroupIndexFromId(CBZ.TempEntrepeneur.CraftGroup2.Id);
            ComboBoxCraftGroup3.SelectedIndex = GetCraftGroupIndexFromId(CBZ.TempEntrepeneur.CraftGroup3.Id);
            ComboBoxCraftGroup4.SelectedIndex = GetCraftGroupIndexFromId(CBZ.TempEntrepeneur.CraftGroup4.Id);
            ComboBoxRegion.SelectedIndex = GetRegionIndexFromId(CBZ.TempEntrepeneur.Region.Id);
            if (CBZ.TempEntrepeneur.CountryWide)
            {
                RadioButtonCountryWideNo.IsChecked = false;
                RadioButtonCountryWideYes.IsChecked = true;
            }
            else
            {
                RadioButtonCountryWideNo.IsChecked = true;
                RadioButtonCountryWideYes.IsChecked = false;
            }


            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }

        }

        private void RadioButtonCountryWideYes_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonCountryWideNo.IsChecked = false;
            RadioButtonCountryWideYes.IsChecked = true;

            if (!CBZ.TempEntrepeneur.CountryWide)
            {
                CBZ.TempEntrepeneur.ToggleCountryWide();
            }


            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }

        }

        private void RadioButtonCountryWideNo_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonCountryWideNo.IsChecked = true;
            RadioButtonCountryWideYes.IsChecked = false;

            if (CBZ.TempEntrepeneur.CountryWide)
            {
                CBZ.TempEntrepeneur.ToggleCountryWide();
            }


            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }

        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempEntrepeneur.Entity.Name = TextBoxName.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempEntrepeneur.Entity.ContactInfo.Email = TextBoxEmail.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxFax_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempEntrepeneur.Entity.ContactInfo.Fax = TextBoxFax.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxMobile_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempEntrepeneur.Entity.ContactInfo.Mobile = TextBoxMobile.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempEntrepeneur.Entity.ContactInfo.Phone = TextBoxPhone.Text;

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
        /// Method, that populate the CraftGroups ComboBoxes
        /// </summary>
        private void GenerateCraftGroupItems()
        {
            ComboBoxCraftGroup1.ItemsSource = "";
            ComboBoxCraftGroup2.ItemsSource = "";
            ComboBoxCraftGroup3.ItemsSource = "";
            ComboBoxCraftGroup4.ItemsSource = "";

            ComboBoxCraftGroup1.ItemsSource = CBZ.IndexedCraftGroups;
            ComboBoxCraftGroup2.ItemsSource = CBZ.IndexedCraftGroups;
            ComboBoxCraftGroup3.ItemsSource = CBZ.IndexedCraftGroups;
            ComboBoxCraftGroup4.ItemsSource = CBZ.IndexedCraftGroups;
        }

        /// <summary>
        /// Method, that retrieves an index from CraftGroup id
        /// </summary>
        /// <param name="craftGroupId"></param>
        /// <returns>int</returns>
        private int GetCraftGroupIndexFromId(int craftGroupId)
        {
            int result = 0;

            foreach (IndexedCraftGroup group in CBZ.IndexedCraftGroups)
            {
                if (group.Id == craftGroupId)
                {
                    result = group.Index;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Method, that retrieves a list of filtered Entrepeneurs for ListBoxEntrepeneurs
        /// </summary>
        private void GetFilteredEntrepeneurs()
        {
            int length = TextBoxSearch.Text.Length;

            if (length > 0)
            {
                CBZ.RefreshList("Entrepeneurs");
                this.FilteredEntrepeneurs.Clear();

                int i = 0;

                foreach (Entrepeneur entrepeneur in CBZ.ActiveEntrepeneurs)
                {
                    if (entrepeneur.Entity.Name.Remove(length).ToLower() == TextBoxSearch.Text.ToLower())
                    {
                        this.FilteredEntrepeneurs.Add(new IndexedEntrepeneur(i, entrepeneur));
                        i++;
                    }
                }

            }
            else
            {
                CBZ.RefreshList("Entrepeneurs");
                this.FilteredEntrepeneurs.Clear();
                int j = 0;
                foreach (Entrepeneur entrepeneur in CBZ.ActiveEntrepeneurs)
                {
                    this.FilteredEntrepeneurs.Add(new IndexedEntrepeneur(j, entrepeneur));
                }

            }

        }

        /// <summary>
        /// Method, that retrieves an index from Region id
        /// </summary>
        /// <param name="craftGroupId"></param>
        /// <returns>int</returns>
        private int GetRegionIndexFromId(int regionId)
        {
            int result = 0;

            foreach (IndexedRegion region in CBZ.IndexedRegions)
            {
                if (region.Id == regionId)
                {
                    result = region.Index;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Method, that creates an Entrepeneur in Db
        /// </summary>
        /// <returns>bool</returns>
        private bool UpdateEntrepeneurInDb => CBZ.UpdateInDb(CBZ.TempEntrepeneur);

        #endregion

    }
}
