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
    /// Interaction logic for UcBuilderCreate.xaml
    /// </summary>
    public partial class UcBuilderCreate : UserControl
    {
        #region Fields
        public Bizz CBZ;
        public UserControl UcMain;

        #endregion

        #region Constructors
        public UcBuilderCreate(Bizz cbz, UserControl ucMain)
        {
            InitializeComponent();
            this.CBZ = cbz;
            this.UcMain = ucMain;
            CBZ.TempBuilder = new Builder();
        }
        public UcBuilderCreate()
        {
            InitializeComponent();
            
        }

        #endregion

        #region Buttons
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (CBZ.TempBuilder != new Builder())
            {
                //Warning about lost changes before closing
                if (MessageBox.Show("Du er ved at lukke 'Opret Bygherre'. Alle ugemte data mistes", "Bygherrer", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //Refresh Builders
                    CBZ.RefreshList("Builders");
                    CBZ.TempBuilder = new Builder();

                    CBZ.CloseUcMain(UcMain);
                }
            }
            else
            {
                //Refresh Builders
                CBZ.RefreshList("Builders");
                CBZ.TempBuilder = new Builder();

                CBZ.CloseUcMain(UcMain);
            }

        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            bool result = CreateBuilderInDb();

            //Display result
            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Bygherren blev oprettet", "Bygherrer", MessageBoxButton.OK, MessageBoxImage.Information);

                //Refresh Builders list
                CBZ.RefreshList("Builders");
                CBZ.TempBuilder = new Builder();

                //Reset Boxes
                TextBoxName.Text = "";
                TextBoxCoName.Text = "";
                TextBoxStreet.Text = "";
                TextBoxPlace.Text = "";
                TextBoxZip.Text = "";
                TextBoxTown.Text = "";
                TextBoxPhone.Text = "";
                TextBoxFax.Text = "";
                TextBoxMobile.Text = "";
                TextBoxEmail.Text = "";

            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Bygherren blev ikke oprettet. Prøv igen.", "Bygherrer", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ButtonSearchCvr_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxCvr.Text != "")
            {
                if (TextBoxCvr.Text.Length == 8 || TextBoxCvr.Text.Length == 10)
                {
                    CBZ.TempBuilder.Entity = CBZ.CvrApi.GetCvrData(TextBoxCvr.Text);
                    //CBZ.TempAddress = CBZ.TempLegalEntity.Address;
                    TextBoxName.Text = CBZ.TempBuilder.Entity.Name;
                    TextBoxCoName.Text = CBZ.TempBuilder.Entity.CoName;
                    TextBoxStreet.Text = CBZ.TempBuilder.Entity.Address.Street;
                    TextBoxPlace.Text = CBZ.TempBuilder.Entity.Address.Place;
                    TextBoxZip.Text = CBZ.TempBuilder.Entity.Address.ZipTown.Zip;
                    TextBoxPhone.Text = CBZ.TempBuilder.Entity.ContactInfo.Phone;
                    TextBoxFax.Text = CBZ.TempBuilder.Entity.ContactInfo.Fax;
                    TextBoxMobile.Text = CBZ.TempBuilder.Entity.ContactInfo.Mobile;
                    TextBoxEmail.Text = CBZ.TempBuilder.Entity.ContactInfo.Email;
                }
            }
        }

        #endregion

        #region Events
        private void TextBoxCvr_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempBuilder.Entity.Cvr = TextBoxCvr.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CBZ.TempBuilder != new Builder())
            {
                CBZ.TempBuilder.Entity.Name = TextBoxName.Text;

                //Set CBZ.UcMainEdited
                if (!CBZ.UcMainEdited)
                {
                    CBZ.UcMainEdited = true;
                }
            }
        }

        private void TextBoxCoName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempBuilder.Entity.CoName = TextBoxCoName.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempBuilder.Entity.ContactInfo.Email = TextBoxEmail.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxFax_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempBuilder.Entity.ContactInfo.Fax = TextBoxFax.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxMobile_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempBuilder.Entity.ContactInfo.Mobile = TextBoxMobile.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempBuilder.Entity.ContactInfo.Phone = TextBoxPhone.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxPlace_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempBuilder.Entity.Address.Place = TextBoxPlace.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxStreet_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.TempBuilder.Entity.Address.Street = TextBoxStreet.Text;

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        private void TextBoxZip_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBZ.RetrieveTempZipTown(TextBoxZip.Text);
            if (CBZ.TempZipTown.Id != 0)
            {
                CBZ.TempBuilder.Entity.Address.ZipTown = CBZ.TempZipTown;
            }
            TextBoxTown.Text = CBZ.RetrieveTownFromZip(TextBoxZip.Text);

            //Set CBZ.UcMainEdited
            if (!CBZ.UcMainEdited)
            {
                CBZ.UcMainEdited = true;
            }
        }

        #endregion

        #region Properties

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates an Address in Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CreateAddressInDb()
        {
            bool result = false;

            try
            {
                int addressId = CBZ.CreateInDb(CBZ.TempBuilder.Entity.Address);
                CBZ.TempBuilder.Entity.Address.SetId(addressId);
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Addressen blev ikke gemt\n" + ex, "Bygherrer", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        /// <summary>
        /// Method, that creates Contact Info in Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CreateContactInfo()
        {
            bool result = false;

            try
            {
                int contactInfoId = CBZ.CreateInDb(CBZ.TempBuilder.Entity.ContactInfo);
                CBZ.TempBuilder.Entity.ContactInfo.SetId(contactInfoId);
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kontaktoplysningerne blev ikke gemt\n" + ex, "Bygherrer", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        /// <summary>
        /// Method, that creates an Builder in Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CreateBuilderInDb()
        {
            //Code that creates a new Builder
            bool result = false;

            //Create Address in Db
            bool addressExist = CreateAddressInDb();


            //Create ContactInfo in Db
            bool contactInfoExist = CreateContactInfo();

            //Create LegalEntity in Db
            bool legalEntityExists = false;

            if (addressExist && contactInfoExist)
            {
                legalEntityExists = CreateLegalEntity();
            }

            //Create Builder in Db
            int entrepeneurId = 0;

            if (legalEntityExists && addressExist && contactInfoExist)
            {
                entrepeneurId = CBZ.CreateInDb(CBZ.TempBuilder);
            }

            //Check result
            if (entrepeneurId >= 1)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Method, that creates a Legal Entity in Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CreateLegalEntity()
        {
            bool result = false;

            try
            {
                int entityId = CBZ.CreateInDb(CBZ.TempBuilder.Entity);
                CBZ.TempBuilder.Entity.SetId(entityId);
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firmaet blev ikke gemt\n" + ex, "Bygherrer", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        #endregion

    }
}
