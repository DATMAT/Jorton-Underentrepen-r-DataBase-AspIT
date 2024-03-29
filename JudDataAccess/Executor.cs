﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudDataAccess
{
    public class Executor : DbConn
    {
        #region Constructors
        /// <summary>
        /// Constructoren Class_YourProjectname initierer class DbConn med passende parametre
        /// der fortæller hvilken server, og hvilken database der skal kopbles op med.
        /// Connection med PW: Data Source=YYY.205.XX.31,\ASPITSQLSERVER;Initial Catalog = JensVen; User ID = sa; Password=QQQQQQQQQQQQQQQQ;
        /// Connection trusted: Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;
        /// Connection IP-address: Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog = myDataBase; User ID = myUsername; Password=myPassword;
        /// For yderligere info se: https://www.connectionstrings.com/sql-server/
        /// </summary>
        /// <param name="strCon">string</param>
        public Executor(string strCon)
        {
            StrConnectionString = strCon;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that adds a User to Db
        /// </summary>
        /// <param name="person">int</param>
        /// <param name="initials">string</param>
        /// <param name="passWord">string</param>
        /// <param name="jobDescriptionId">int</param>
        /// <param name="userLevelId">int</param>
        /// <returns>bool</returns>
        public bool AddUser(int person, string initials, int department, string passWord, int jobDescriptionId, int userLevelId)
        {
            string[] args = new string[6];
            args[0] = person.ToString();
            args[1] = initials;
            args[2] = department.ToString();
            args[3] = passWord;
            args[4] = jobDescriptionId.ToString();
            args[5] = userLevelId.ToString();

            return Convert.ToBoolean(DbReturnBool(@"usersAddUser", args));
        }

        /// <summary>
        /// Method, that sets new password in Db, if old password is correct
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="oldPassWord">string</param>
        /// <param name="newPassWord">string</param>
        /// <returns></returns>
        public bool ChangePassword(int id, string oldPassWord, string newPassWord)
        {
            bool result = false;
            string[] args = new string[3];
            args[0] = id.ToString();
            args[1] = oldPassWord;
            args[2] = newPassWord;

            result = DbReturnBool(@"usersUpdatePassword", args);

            return result;
        }


        /// <summary>
        /// Method, that checks wether password is correct
        /// </summary>
        /// <param name="initials">string</param>
        /// <param name="passWord">string</param>
        /// <returns></returns>
        public bool CheckLogin(string initials, string passWord)
        {
            bool result = false;
            string[] args = new string[2];
            args[0] = initials;
            args[1] = passWord;

            result = DbReturnBool(@"usersLogin", args);

            return result;
        }

        /// <summary>
        /// Method, that retrieves a List from Db
        /// </summary>
        /// <param name="table">string</param>
        /// <param name="id">int</param>
        /// <returns>List<string></returns>
        public List<string> ReadListFromDataBase(string table, int id = -1)
        {
            List<string> listRes = new List<string>();
            DataTable dm = GetListDataTable(table, id);

            foreach (DataRow row in dm.Rows)
            {
                string rowString = "";

                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    rowString += row[i] + ";";
                }
                rowString = rowString.Remove(rowString.Length - 1);
                listRes.Add(rowString);
            }
            return listRes;
        }

        /// <summary>
        /// Method, that retrieves a DataTable containing a List
        /// </summary>
        /// <param name="table">string</param>
        /// <returns>DataTable</returns>
        private DataTable GetListDataTable(string table, int id = -1)
        {
            DataTable result = new DataTable();

            switch (table)
            {
                case "Entrepeneurs":
                    if (id >= 0)
                    {
                        result = DbReturnDataTable(@"SELECT * FROM [Entrepeneurs] WHERE [Id] = " + id);
                    }
                    else
                    {
                        result = DbReturnDataTable(@"SELECT * FROM [Entrepeneurs] ORDER BY [Cooperative] DESC, [Id] ASC");
                    }
                    break;
                case "Users":
                    if (id >= 0)
                    {
                        result = DbReturnDataTable(@"SELECT Id, Person, Initials, Department, JobDescription, UserLevel FROM [Users] WHERE [Id] = " + id);
                    }
                    else
                    {
                        result = DbReturnDataTable(@"SELECT Id, Person, Initials, Department, JobDescription, UserLevel FROM [Users]");
                    }
                    break;
                default:
                    if (id >= 0)
                    {
                        result = DbReturnDataTable(@"SELECT * FROM [" + table + "] WHERE [Id] = " + id);
                    }
                    else
                    {
                        result = DbReturnDataTable("SELECT * FROM " + table);
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// Method, that retrieves a DataTable containing a List
        /// Accepts the following tables: Enterprises, Shippings & SubEntrepeneurs
        /// </summary>
        /// <param name="table">string</param>
        /// <param name="projectId">int</param>
        /// <returns>DataTable</returns>
        private DataTable GetProjectListDataTable(string table, int projectId)
        {
            return DbReturnDataTable(@"SELECT * FROM [" + table + @"] WHERE [Project] = " + projectId + "");
        }

        /// <summary>
        /// Method, that sends an SQL-Query string to Db
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>bool</returns>
        public bool WriteToDataBase(string strSql)
        {
            try
            {
                FunctionExecuteNonQuery(strSql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}
