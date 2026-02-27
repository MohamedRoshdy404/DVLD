using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ProjectDVLD.Global_Classes
{
    public class clsUtil
    {



        public enum enMainMenuePermissions
        {
            eAll = -1, ssToolStripMenuItem = 1, manageApplications = 2, detainLicenses = 4, manageApplicationTypesToolStripMenuItem = 8
                , manageTestTypesToolStripMenuItem = 16

        };

        public static bool CheckPermissions(enMainMenuePermissions permissions)
        {
            if ((enMainMenuePermissions)Global_Classes.clsUserInfo.CurrentUser.Permissions == enMainMenuePermissions.eAll)
                return true;
            if ((permissions & (enMainMenuePermissions)Global_Classes.clsUserInfo.CurrentUser.Permissions) == permissions)
                return true;
            else
                return false;
        }

        public static string GenerateGUID()
        {

            // Generate a new GUID
            Guid newGuid = Guid.NewGuid();

            // convert the GUID to a string
            return newGuid.ToString();

        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;

        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            // Full file name. Change your file name   
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string extn = fi.Extension;
            return GenerateGUID() + extn;

        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            // this funciton will copy the image to the
            // project images foldr after renaming it
            // with GUID with the same extention, then it will update the sourceFileName with the new name.

            string DestinationFolder = @"C:\DVLD-People-Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, destinationFile, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile = destinationFile;
            return true;
        }



        public static string ComPuteHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] HashByets = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(HashByets).Replace("-", "").ToLower();
            }
        }


        public static bool DarkModeIsActave()
        {

            return true;
        }

    }
}
