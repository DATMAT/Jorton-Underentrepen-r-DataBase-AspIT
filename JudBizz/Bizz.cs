using CvrApiServices;
using JudRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using JudDataAccess;
using UkZipApi;

namespace JudBizz
{
    public class Bizz
    {
        #region Fields

        private MyEntityFrameWork MEF = new MyEntityFrameWork();

        public CvrAPI CvrApi;
        public UkZipApi.UkZipApi UkZipApi;
        public bool UcMainEdited = false;
        private bool danishZip = false;
        private bool faroeseZip = false;
        private bool foreignZip = false;
        private bool kaalaalitZip = false;
        private bool validZip = false;
        private int intZip = 0;
        private string tempZip = "";
        public ImageBrush button = new ImageBrush();
        public ImageBrush smallButton = new ImageBrush();
        public ImageBrush broadButton = new ImageBrush();
        public ImageBrush greenIndicator = new ImageBrush();
        public ImageBrush redIndicator = new ImageBrush();
        private RepoCollections repos;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Bizz()
        {
            //RefreshAllInitialIndexedLists();
            CvrApi = new CvrAPI(Repos.ZipTowns);
            UkZipApi = new UkZipApi.UkZipApi();

            SetIndicators();
        }

        #endregion

        #region Properties

        public RepoCollections Repos
        {
            get { return repos; }
            set { repos = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method, that checks, wether a string can be converted to integer
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public bool CanConvertStringToInt(string zip)
        {
            intZip = 0;
            return Int32.TryParse(zip, out intZip);
        }

        /// <summary>
        /// Method, that checks credentials
        /// </summary>
        /// <param name="userName">TextBlock</param>
        /// <param name="menuItemChangePassWord">RibbonApplicationMenuItem</param>
        /// <param name="menuItemLogOut">RibbonApplicationMenuItem</param>
        /// <param name="initials">string</param>
        /// <param name="passWord">string</param>
        /// <returns>bool</returns>
        public bool CheckCredentials(Label userName, RibbonApplicationMenuItem menuItemChangePassWord, RibbonApplicationMenuItem menuItemLogOut, string initials, string passWord)
        {
            bool result = false;
            MEF.RefreshList("Users");
            if (MEF.CheckLogin(initials, passWord))
            {
                foreach (User user in Repos.Users)
                {
                    if (user.Initials == initials && user.UserLevel.Id >= 1)
                    {
                        MEF.CurrentUser = user;
                        userName.Content = user.Person.Name;
                        menuItemChangePassWord.IsEnabled = true;
                        menuItemLogOut.IsEnabled = true;
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that clears the UcMain UserControl in MainWindow
        /// </summary>
        /// <param name="ucMain"></param>
        public void CloseUcMain(UserControl ucMain)
        {
            UcMainEdited = false;
            ucMain.Content = new UserControl();
        }

        /// <summary>
        /// Method, that loads images for buttons
        /// </summary>
        private void SetIndicators()
        {
            Uri buttonUri = new Uri(@"Images\button.png", UriKind.Relative);
            Uri smallButtonUri = new Uri(@"Images\button-small.png", UriKind.Relative);
            Uri broadButtonUri = new Uri(@"Images\button-small-broad.png", UriKind.Relative);
            Uri greenUri = new Uri(@"Images\green-indicator.png", UriKind.Relative);
            Uri redUri = new Uri(@"Images\red-indicator.png", UriKind.Relative);

            StreamResourceInfo buttonStreamInfo = Application.GetResourceStream(buttonUri);
            StreamResourceInfo smallButtonStreamInfo = Application.GetResourceStream(smallButtonUri);
            StreamResourceInfo broadButtonStreamInfo = Application.GetResourceStream(broadButtonUri);
            StreamResourceInfo greenStreamInfo = Application.GetResourceStream(greenUri);
            StreamResourceInfo redStreamInfo = Application.GetResourceStream(redUri);

            BitmapFrame buttonFrame = BitmapFrame.Create(buttonStreamInfo.Stream);
            BitmapFrame smallButtonFrame = BitmapFrame.Create(smallButtonStreamInfo.Stream);
            BitmapFrame broadButtonFrame = BitmapFrame.Create(broadButtonStreamInfo.Stream);
            BitmapFrame greenFrame = BitmapFrame.Create(greenStreamInfo.Stream);
            BitmapFrame redFrame = BitmapFrame.Create(redStreamInfo.Stream);

            button.ImageSource = buttonFrame;
            smallButton.ImageSource = smallButtonFrame;
            broadButton.ImageSource = broadButtonFrame;
            greenIndicator.ImageSource = greenFrame;
            redIndicator.ImageSource = redFrame;

        }

        #region Zips
        /// <summary>
        /// Method, that detects obsolete faroese zips
        /// - obsolete faroese zips are zips within the range [3800;3899] written without prefix
        /// </summary>
        /// <param name="zip">string</param>
        private void DetectObsoleteFaroeseZip()
        {
            if (tempZip.Length == 4)
            {
                if (CanConvertStringToInt(tempZip))
                {
                    if (intZip >= 3800 && intZip <= 3899)
                    {
                            tempZip = RetrieveNewFaroeseZip(tempZip);
                    }

                }

            }

        }

        /// <summary>
        /// Method, that exchanges obsolete faroese zip to valid zip
        /// </summary>
        /// <param name="zip">string</param>
        /// <returns>string</returns>
        private string RetrieveNewFaroeseZip(string zip)
        {
            string result = "FO-999";

            switch (zip)
            {
                case "3800":
                    result = "FO-100"; //Tórshavn
                    break;
                case "3813":
                    result = "FO-530"; //Fuglafjørdur 
                    break;
                case "3820":
                    result = "FO-240"; //Skopun
                    break;
                case "3821":
                    result = "FO-210"; //Sandur
                    break;
                case "3822":
                    result = "FO-220"; //Skálavík
                    break;
                case "3823":
                    result = "FO-235"; //Dalur
                    break;
                case "3826":
                    result = "FO-260"; //Skúvoy
                    break;
                case "3827":
                    result = "FO-270"; //Nólsoy
                    break;
                case "3828":
                    result = "FO-280"; //Hestur
                    break;
                case "3829":
                    result = "FO-285"; //Koltur
                    break;
                case "3830":
                    result = "FO-466"; //Ljósá
                    break;
                case "3831":
                    result = "FO-478"; //Elduvik
                    break;
                case "3832":
                    result = "FO-475"; //Funningur
                    break;
                case "3833":
                    result = "FO-476"; //Gjógv
                    break;
                case "3834":
                    result = "FO-470"; //Eiði
                    break;
                case "3835":
                    result = "FO-430"; //Hvalvik
                    break;
                case "3836":
                    result = "FO-420"; //Hósvík
                    break;
                case "3837":
                    result = "FO-440"; //Haldarsvik
                    break;
                case "3840":
                    result = "FO-530"; //Fuglafjørdur
                    break;
                case "3842":
                    result = "FO-F20"; //Leirvik
                    break;
                case "3843":
                    result = "FO-510"; //Gøta
                    break;
                case "3845":
                    result = "FO-485"; //Skálafjørður
                    break;
                case "3846":
                    result = "FO-495"; //Kolbanargjógv
                    break;
                case "3847":
                    result = "FO-496"; //Morskranes
                    break;
                case "3850":
                    result = "FO-600"; //Saltangará
                    break;
                case "3854":
                    result = "FO-640"; //Rituvík
                    break;
                case "3855":
                    result = "FO-655"; //Nes, Eysturoy
                    break;
                case "3856":
                    result = "FO-660"; //Søldarfjørður
                    break;
                case "3857":
                    result = "FO-666"; //Gøtueiði
                    break;
                case "3859":
                    result = "FO-695"; //Hellur
                    break;
                case "3860":
                    result = "FO-350"; //Vestmanna
                    break;
                case "3862":
                    result = "FO-410"; //Kollafjørður
                    break;
                case "3863":
                    result = "FO-335"; //Leynar
                    break;
                case "3864":
                    result = "FO-340"; //Kvívík
                    break;
                case "3865":
                    result = "FO-360"; //Sandavágur
                    break;
                case "3866":
                    result = "FO-370"; //Miðvágur
                    break;
                case "3867":
                    result = "FO-386"; //Bøur
                    break;
                case "3868":
                    result = "FO-385"; //Vatnsoyrar
                    break;
                case "3869":
                    result = "FO-388"; //Mykines
                    break;
                case "3870":
                    result = "FO-700"; //Klaksvík
                    break;
                case "3874":
                    result = "FO-726"; //Ánir
                    break;
                case "3875":
                    result = "FO-750"; //Viðareiði
                    break;
                case "3876":
                    result = "FO-765"; //Svínoy
                    break;
                case "3877":
                    result = "FO-767"; //Hattarvík
                    break;
                case "3878":
                    result = "FO-785"; //Haraldssund
                    break;
                case "3879":
                    result = "FO-796"; //Húsar
                    break;
                case "3880":
                    result = "FO-800"; //Tvøroyri
                    break;
                case "3885":
                    result = "FO-850"; //Hvalba
                    break;
                case "3886":
                    result = "FO-860"; //Sandvík
                    break;
                case "3887":
                    result = "FO-870"; //Fámjin
                    break;
                case "3890":
                    result = "FO-900"; //Vágur
                    break;
                case "3895":
                    result = "FO-950"; //Porkeri
                    break;
                case "3896":
                    result = "FO-960"; //Hov
                    break;
                case "3897":
                    result = "FO-970"; //Sumba
                    break;
            }

            return result;
        }

        /// <summary>
        /// Method, that retrieves a town based on the zip
        /// </summary>
        /// <param name="zip">string</param>
        /// <returns>string</returns>
        public string RetrieveTownFromZip(string zip)
        {
            RetrieveTempZipTown(zip);
            return MEF.TempZipTown.Town;
        }

        /// <summary>
        /// Method, that retrieves TempZipTown based on the zip
        /// </summary>
        /// <param name="zip">string</param>
        public void RetrieveTempZipTown(string zip)
        {
            MEF.TempZipTown = new ZipTown();
            bool zipFound = false;
            tempZip = zip;

            //Analyse zip
            if (zip.Length >= 3)
            {
                //Validate Zips
                ValidateZip();

                //Search valid in ZipTown list
                if (validZip)
                {
                    foreach (ZipTown zipTown in Repos.ZipTowns)
                    {
                        if (zipTown.Zip == zip)
                        {
                            MEF.TempZipTown = zipTown;
                            zipFound = true;
                            break;
                        }
                    }

                }
            }

            if (!zipFound)
            {
                MEF.TempZipTown = MEF.GetZipTown(1100);
            }

        }

        /// <summary>
        /// Method, that validates danish zips
        /// . Danish zips are 4 digits written witout prefix
        /// ; within the ranges
        /// : [0100;2411], [2413;3799] and [4000;9999] 
        /// </summary>
        /// <param name="zip">string</param>
        private void ValidateDanishZip()
        {
            bool canConvertTempZipToInt = CanConvertStringToInt(tempZip);

            if (tempZip.Length == 4)
            {
                intZip = 0;

                if (CanConvertStringToInt(tempZip))
                {
                    if (tempZip.Remove(1) == "0")
                    {
                        danishZip = true;
                    }
                    else if (intZip >= 1000 && intZip <= 2411 || intZip >= 2413 && intZip <= 3799 || intZip >= 4000 && intZip <= 9999)
                    {
                        danishZip = true;
                    }
                }

            }
            else
            {
                danishZip = false;
            }

        }

        /// <summary>
        /// Method, that validates faroese zips
        /// . Faroese zips are 3 digits
        /// , and always written with prefix in Denmark
        /// ; e.g. FO-100 Tórshavn
        /// </summary>
        private void ValidateFaroeseZip()
        {
            DetectObsoleteFaroeseZip();

            switch (tempZip.Length)
            {
                case 3:
                    intZip = 0;

                    if (CanConvertStringToInt(tempZip))
                    {
                        if (intZip >= 100 && intZip <= 999)
                        {
                            faroeseZip = true;
                            tempZip = "FO-" + tempZip;
                        }
                        else
                        {
                            faroeseZip = false;
                        }
                    }
                    break;
                case 6:
                    if (tempZip.Remove(2) == "Fo" || tempZip.Remove(2) == "fo")
                    {
                        faroeseZip = true;
                        tempZip = "FO" + tempZip.Remove(2, 5);
                    }
                    else if (tempZip.Remove(2) == "FO")
                    {
                        faroeseZip = true;
                    }
                    else
                    {
                        faroeseZip = false;
                    }
                    break;
                default:
                    faroeseZip = false;
                    break;
            }
        }

        /// <summary>
        /// Method, that validates foreign zips 
        /// . Foreign zips differ in format 
        /// . In Denmark they are mostly written with prefix 
        /// ; e.g. D-24955 or NL-7891 WS
        /// . Excludes danish, faroese and kaalaalit zips
        /// . Specifically validate british postcodes
        /// ; e.g. EC1A 1BB or W1A 0AX
        /// </summary>
        private void ValidateForeignZip()
        {
            if (tempZip.Length >= 3 && !danishZip && !faroeseZip && !kaalaalitZip)
            {
                if (UkZipApi.ValidateUkZip(tempZip))
                {
                    foreignZip = true;
                }
                else if (!CanConvertStringToInt(tempZip.Remove(1)))
                {
                    foreignZip = true;
                }
            }
            else
            {
                foreignZip = false;
            }
        }

        /// <summary>
        /// Method, that validates kaalaalit zips
        /// . Kaalaalit zips are 4 digits 
        /// , and usually written with prefix in Denmark 
        /// ; e.g. GL-3900 Nuuk
        /// . An ordinary valid zip is within the range [3900;3999] 
        /// . 2412 is an exclusive zip for Juullip Inua 
        /// (Santa Claus - December 24th is Christmas Eve in Kingdom of Denmark)
        /// </summary>
        private void ValidateKaalaalitZip()
        {
            switch (tempZip.Length)
            {
                case 4:
                    intZip = 0;

                    if (CanConvertStringToInt(tempZip))
                    {
                        if (intZip >= 3900 && intZip <= 3999)
                        {
                            kaalaalitZip = true;
                            tempZip = "GL-" + tempZip;
                        }
                        else if (intZip == 2412)
                        {
                            kaalaalitZip = true;
                            tempZip = "GL-2412";
                        }
                        else
                        {
                            kaalaalitZip = false;
                        }
                    }
                    else
                    {
                        kaalaalitZip = false;
                    }

                    break;
                case 7:
                    if (tempZip.Remove(2) == "Fo" || tempZip.Remove(2) == "fo")
                    {
                        faroeseZip = true;
                        tempZip = "FO" + tempZip.Remove(2, 6);
                    }
                    else if (tempZip.Remove(2) == "FO")
                    {
                        faroeseZip = true;
                    }
                    else
                    {
                        faroeseZip = false;
                    }
                    break;
                default:
                    kaalaalitZip = false;
                    break;
            }
            if (tempZip.Length == 4)
            {

            }
            else
            {
                kaalaalitZip = false;
            }

        }

        /// <summary>
        /// Method, that validates zips
        /// . A valid zip is at least 3 CHARs
        /// ; is written with or without prefix in Denmark.
        /// ; e.g. 6230 Rødekro, FO-100 Torshavn, GL-3900 Nuuk or D-24955 Harrislee
        /// </summary>
        /// <param name="zip">string</param>
        private void ValidateZip()
        {
            if (tempZip.Length > 2)
            {
                ValidateDanishZip();

                if (!danishZip)
                {
                    ValidateFaroeseZip();

                    if (!faroeseZip)
                    {
                        ValidateKaalaalitZip();

                        if (!kaalaalitZip)
                        {
                            ValidateForeignZip();
                        }
                    }
                }

                if (danishZip || faroeseZip || foreignZip || kaalaalitZip)
                {
                    validZip = true;
                }
            }
            else
            {
                validZip = false;
            }

        }

        #endregion

        #endregion

    }
}
