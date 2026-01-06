using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsUsersBuisnessLayer
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int PersonID {  get; set; }
        public int UserID {  get; set; }
         
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte IsActive { get; set; }




        public clsUsersBuisnessLayer()
        {
            this.PersonID = -1;
            this.UserID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.UserName = "";
            this.Password = "";
            this.IsActive = 0;

            Mode = enMode.AddNew;
        }

        public clsUsersBuisnessLayer(int UserID , int PersonID ,string UserName, string Password, byte IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

                
        public static DataTable GetInfoUsers()
        {
            return clsUsersDataAccess.GetInfoUsers();
        }

        public static clsUsersBuisnessLayer Find(int UserID)
        {
            int PersonID = 0;
            string UserName = "", Password = "";
            byte IsActive = 0;

            if (clsUsersDataAccess.FindUser(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUsersBuisnessLayer(UserID , PersonID , UserName , Password , IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUsersBuisnessLayer FindUserByUserNameAndPassword(string UserName , string Password )
        {
            int PersonID = 0 , UserID = 0;
            byte IsActive = 0;

            if (clsUsersDataAccess.FindUserByUserNameAndPassword(ref UserID, ref PersonID,  UserName,  Password, ref IsActive))
            {
                return new clsUsersBuisnessLayer(UserID , PersonID , UserName , Password , IsActive);
            }
            else
            {
                return null;
            }
        }

        public static bool isExist(int PersonID)
        {
            return clsUsersDataAccess.isExist(PersonID);
        }        



        private bool _AddNewUser()
        {
            this.UserID = clsUsersDataAccess.AddNewUser(this.PersonID , this.UserName , this.Password , this.IsActive);

            return (this.UserID != -1);
        }

                
        public bool ChangePassword()
        {
            return (clsUsersDataAccess.ChangePassword(this.UserID, this.Password));
        }


        private bool _UpdateUser()
        {
            return clsUsersDataAccess.UpdateUser(this.UserID,this.UserName, this.Password, this.IsActive);
        }


        public static bool DeleteUser(int UserID)
        {
            return clsUsersDataAccess.DeleteUser(UserID);
        }
                
        public bool Save()
        {

            switch (Mode)
            {

                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                    case enMode.Update:

                      return  _UpdateUser();



                default:

                    return false;
            }
        }



    }
}
