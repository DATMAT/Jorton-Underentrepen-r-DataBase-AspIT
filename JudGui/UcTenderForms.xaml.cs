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
    /// Interaction logic for UcTenderForms.xaml
    /// </summary>
    public partial class UcTenderForms : UserControl
    {
        #region Fields
        public Bizz CBZ;
        public UserControl UcMain;
        #endregion

        #region Constructors
        public UcTenderForms(Bizz cbz, UserControl ucMain)
        {
            InitializeComponent();
            this.CBZ = cbz;
            this.UcMain = ucMain;

            // Get tender forms from db
        }

        #endregion

        #region Buttons
        
        #endregion

        #region Events


        #endregion

        #region Methods
        private async Task LoadTenderFormsAsync()
        {

        }

        #endregion

    }
}
