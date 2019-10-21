using JudBizz;
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
    /// Interaction logic for UcTenderForms.xaml
    /// </summary>
    public partial class UcTenderForms : UserControl
    {
        #region Fields
        public Bizz CBZ;
        public UserControl UcMain;
        ListCollectionView lcv;
        #endregion

        #region Constructors
        public UcTenderForms(Bizz cbz, UserControl ucMain)
        {
            InitializeComponent();
            this.CBZ = cbz;
            this.UcMain = ucMain;

            // Get tender forms from db
            CBZ.TenderForms = CBZ.RefreshList("Tenderforms", CBZ.TenderForms);

            // New ListViewCollection containing the items from the TenderForms list in Bizz.
            lcv = new ListCollectionView(CBZ.TenderForms);
            // Immediately sets the itemssource of the ListBox to the ListViewCollection.
            ListBoxTenderForms.ItemsSource = lcv;
        }

        #endregion

        #region Buttons

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (CBZ.UcMainEdited)
            {
                //Warning about lost changes before closing
                if (MessageBox.Show("Vil du lukke Udbudsformer? Alle ugemte data mistes.", "Udbudsformen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    CBZ.CloseUcMain(UcMain);
                }
            }
            else
            {
                CBZ.CloseUcMain(UcMain);
            }

        }

        #endregion

        #region Events
        /// <summary>
        /// Filters the TenderForms in the ListBox to show only TenderForms with text containing
        /// text the user has searched for in TextBoxTenderFormSearch.
        /// Utilizes the ListCollectionView.Filter method.
        /// </summary>
        /// <param name="sender">ListBox</param>
        /// <param name="e">TextChangedEventArgs</param>
        private void TextBoxTenderFormSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If nothing is typed into TextBoxTenderFormSearch, the filter is removed and
            // all items are shown.
            if (String.IsNullOrEmpty(TextBoxTenderFormSearch.Text))
                lcv.Filter = null;

            else
                lcv.Filter = (t) =>
                {
                    TenderForm tf = t as TenderForm;
                    if (tf.Text.ToLower().Contains(TextBoxTenderFormSearch.Text.ToLower()))
                        return true;
                    return false;
                };
            // The ItemsSource is updated
            ListBoxTenderForms.ItemsSource = lcv;
        }

        private void ButtonAddTenderForm_Click(object sender, RoutedEventArgs e)
        {
            TenderForm tf = new TenderForm(TextBoxNewText.Text);
            CBZ.CreateInDb("Tenderforms", tf);
        }


        #endregion

        #region Methods

        #endregion
    }
}
