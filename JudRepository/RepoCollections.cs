using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class RepoCollections
    {
        #region Fields
        private ObservableCollection<Builder> activeBuilders;
        private ObservableCollection<Entrepeneur> activeEntrepeneurs;
        private ObservableCollection<Project> activeProjects;
        private ObservableCollection<User> activeUsers;
        private ObservableCollection<Address> addresses;
        private ObservableCollection<Builder> builders;
        private ObservableCollection<Bullet> bullets;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Contact> contacts;
        private ObservableCollection<ContactInfo> contactInfoList;
        private ObservableCollection<CraftGroup> craftGroups;
        private ObservableCollection<EnterpriseForm> enterpriseForms;
        private ObservableCollection<Enterprise> enterprises;
        private ObservableCollection<Entrepeneur> entrepeneurs;
        private ObservableCollection<Builder> inactiveBuilders;
        private ObservableCollection<Entrepeneur> inactiveEntrepeneurs;
        private ObservableCollection<Project> inactiveProjects;
        private ObservableCollection<User> inactiveUsers;
        private ObservableCollection<IttLetter> ittLetters;
        private ObservableCollection<JobDescription> jobDescriptions;
        private ObservableCollection<LegalEntity> legalEntities;
        private ObservableCollection<LetterData> letterDataList;
        private ObservableCollection<Offer> offers;
        private ObservableCollection<Paragraf> paragrafs;
        private ObservableCollection<Person> persons;
        private ObservableCollection<ProjectDetail> projectDetails;
        private ObservableCollection<Project> projects;
        private ObservableCollection<ProjectStatus> projectStatuses;
        private ObservableCollection<Receiver> receivers;
        private ObservableCollection<Region> regions;
        private ObservableCollection<Request> requests;
        private ObservableCollection<RequestStatus> requestStatuses;
        private ObservableCollection<Shipping> shippings;
        private ObservableCollection<SubEntrepeneur> subEntrepeneurs;
        private ObservableCollection<TenderForm> tenderForms;
        private ObservableCollection<UserLevel> userLevels;
        private ObservableCollection<User> users;
        private ObservableCollection<ZipTown> zipTowns;
        #endregion
        public RepoCollections()
        {

        }

        #region Properties
        public ObservableCollection<ZipTown> ZipTowns
        {
            get { return zipTowns; }
            set { zipTowns = value; }
        }

        public ObservableCollection<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        public ObservableCollection<UserLevel> UserLevels
        {
            get { return userLevels; }
            set { userLevels = value; }
        }

        public ObservableCollection<TenderForm> TenderForms
        {
            get { return tenderForms; }
            set { tenderForms = value; }
        }

        public ObservableCollection<SubEntrepeneur> SubEntrepeneurs
        {
            get { return subEntrepeneurs; }
            set { subEntrepeneurs = value; }
        }


        public ObservableCollection<Shipping> Shippings
        {
            get { return shippings; }
            set { shippings = value; }
        }

        public ObservableCollection<RequestStatus> RequestStatuses
        {
            get { return requestStatuses; }
            set { requestStatuses = value; }
        }

        public ObservableCollection<Request> Requests
        {
            get { return requests; }
            set { requests = value; }
        }

        public ObservableCollection<Region> Regions
        {
            get { return regions; }
            set { regions = value; }
        }

        public ObservableCollection<Receiver> Receivers
        {
            get { return receivers; }
            set { receivers = value; }
        }

        public ObservableCollection<ProjectStatus> ProjectStatuses
        {
            get { return projectStatuses; }
            set { projectStatuses = value; }
        }

        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set { projects = value; }
        }

        public ObservableCollection<ProjectDetail> ProjectDetails
        {
            get { return projectDetails; }
            set { projectDetails = value; }
        }

        public ObservableCollection<Person> Persons
        {
            get { return persons; }
            set { persons = value; }
        }

        public ObservableCollection<Paragraf> Paragrafs
        {
            get { return paragrafs; }
            set { paragrafs = value; }
        }

        public ObservableCollection<Offer> Offers
        {
            get { return offers; }
            set { offers = value; }
        }

        public ObservableCollection<LetterData> LetterDataList
        {
            get { return letterDataList; }
            set { letterDataList = value; }
        }

        public ObservableCollection<LegalEntity> LegalEntities
        {
            get { return legalEntities; }
            set { legalEntities = value; }
        }

        public ObservableCollection<JobDescription> JobDescriptions
        {
            get { return jobDescriptions; }
            set { jobDescriptions = value; }
        }

        public ObservableCollection<IttLetter> IttLetters
        {
            get { return ittLetters; }
            set { ittLetters = value; }
        }

        public ObservableCollection<User> InactiveUsers
        {
            get { return inactiveUsers; }
            set { inactiveUsers = value; }
        }

        public ObservableCollection<Project> InactiveProjects
        {
            get { return inactiveProjects; }
            set { inactiveProjects = value; }
        }

        public ObservableCollection<Entrepeneur> InactiveEntrepeneurs
        {
            get { return inactiveEntrepeneurs; }
            set { inactiveEntrepeneurs = value; }
        }

        public ObservableCollection<Builder> InactiveBuilders
        {
            get { return inactiveBuilders; }
            set { inactiveBuilders = value; }
        }

        public ObservableCollection<Entrepeneur> Entrepeneurs
        {
            get { return entrepeneurs; }
            set { entrepeneurs = value; }
        }

        public ObservableCollection<Enterprise> Enterprises
        {
            get { return enterprises; }
            set { enterprises = value; }
        }

        public ObservableCollection<EnterpriseForm> EnterpriseForms
        {
            get { return enterpriseForms; }
            set { enterpriseForms = value; }
        }

        public ObservableCollection<CraftGroup> CraftGroups
        {
            get { return craftGroups; }
            set { craftGroups = value; }
        }

        public ObservableCollection<ContactInfo> ContactInfoList
        {
            get { return contactInfoList; }
            set { contactInfoList = value; }
        }

        public ObservableCollection<Contact> Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        public ObservableCollection<Bullet> Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }

        public ObservableCollection<Builder> Builders
        {
            get { return builders; }
            set { builders = value; }
        }

        public ObservableCollection<Address> Addresses
        {
            get { return addresses; }
            set { addresses = value; }
        }

        public ObservableCollection<User> ActiveUsers
        {
            get { return activeUsers; }
            set { activeUsers = value; }
        }

        public ObservableCollection<Project> ActiveProjects
        {
            get { return activeProjects; }
            set { activeProjects = value; }
        }

        public ObservableCollection<Entrepeneur> ActiveEntrepeneurs
        {
            get { return activeEntrepeneurs; }
            set { activeEntrepeneurs = value; }
        }

        public ObservableCollection<Builder> ActiveBuilders
        {
            get { return activeBuilders; }
            set { activeBuilders = value; }
        }
        #endregion
    }
}