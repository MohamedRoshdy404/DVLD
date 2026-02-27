using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsCountriesBuisnessLayer
    {






        public int ID { set; get; }
        public string CountryName { set; get; }

        public clsCountriesBuisnessLayer()

        {
            this.ID = -1;
            this.CountryName = "";

        }

        private clsCountriesBuisnessLayer(int ID, string CountryName)

        {
            this.ID = ID;
            this.CountryName = CountryName;
        }

        public static clsCountriesBuisnessLayer Find(int ID)
        {
            string CountryName = "";

            if (clsCountriesDataAccess.GetCountryInfoByID(ID, ref CountryName))

                return new clsCountriesBuisnessLayer(ID, CountryName);
            else
                return null;

        }

        public static clsCountriesBuisnessLayer Find(string CountryName)
        {

            int ID = -1;

            if (clsCountriesDataAccess.GetCountryInfoByName(CountryName, ref ID))

                return new clsCountriesBuisnessLayer(ID, CountryName);
            else
                return null;

        }

        public static DataTable GetAllCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();

        }




    }
}
