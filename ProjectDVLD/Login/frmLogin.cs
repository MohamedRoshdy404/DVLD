using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DVLD_Buisness;
using ProjectDVLD.Global_Classes;
using System.Media;

namespace ProjectDVLD.Login
{
    public partial class frmLogin: Form
    {


        clsUsersBuisnessLayer _User;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //private void _FillUserInfo()
        //{
        //    //clsUserInfo.CurrentUser.UserID = _User.UserID;
        //    //clsUserInfo.CurrentUser.UserName = _User.UserName;
        //    //clsUserInfo.CurrentUser.IsActive = _User.IsActive;
        //    //clsUserInfo.CurrentUser.PersonID = _User.PersonID;
        //}

        private void _SoundLogin()
        {
            SoundPlayer player = new SoundPlayer("Sounds/mixkit-fantasy-game-success-notification-270.wav");
            player.Play();

        }
        public void SetStringValueLoginFromRegistry(string Username , string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD"; // مسار المفتاح في التسجيل
            string Usernamevalue = "Username";
            string UsernamevalueData = Username;

            string Passwordvalue = "Password";
            string PasswordvalueData = Password;

            try
            {
                Registry.SetValue(keyPath, Usernamevalue, UsernamevalueData, RegistryValueKind.String);
                Registry.SetValue(keyPath, Passwordvalue, PasswordvalueData, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message , "ERROR" , MessageBoxButtons.OK , MessageBoxIcon.Error );
            }
        }

        // دالة مساعدة لقراءة قيمة السلسلة من التسجيل
        void GetStringValueLoginFromRegistry()
        {
            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD"; // مسار المفتاح في التسجيل
            string Usernamevalue = "Username";


            string Passwordvalue = "Password";


            try
            {
                string UsernameGetValue = Registry.GetValue(keyPath, Usernamevalue, null) as string;
                string PasswordGetVlaue = Registry.GetValue(keyPath, Passwordvalue, null) as string;

                if (Usernamevalue != null && Passwordvalue != null)
                {
                    txtUserName.Text = UsernameGetValue;
                    txtPassword.Text = PasswordGetVlaue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            // الحصول على النصوص من الـ TextBoxين
            string txtUserNameContent = txtUserName.Text;
            string txtPasswordContent = "" ;

            txtPasswordContent = Global_Classes.clsUtil.ComPuteHash(txtPassword.Text);

            _User = clsUsersBuisnessLayer.FindUserByUserNameAndPassword(txtUserNameContent.Trim(), txtPasswordContent.Trim());

            if (_User == null)
            {
                MessageBox.Show("This user does not exist. Please enter a user that exists in the system.", "This user does not exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            if (_User.IsActive > 0)
            {
                //_FillUserInfo();

                if (chkRememberMe.Checked)
                {
                    SetStringValueLoginFromRegistry(txtUserName.Text , txtPassword.Text);
                }
                else
                {
                    SetStringValueLoginFromRegistry("", "");
                }

                //_SoundLogin();
                clsUserInfo.CurrentUser = _User;
                this.Hide();
                Form frmMain = new Main();
                frmMain.ShowDialog();

            }
            else
            {
                MessageBox.Show("This user is not activated in the system.", "user is not activated.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
        private void _LoadDataFromFile()
        {
            GetStringValueLoginFromRegistry();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _LoadDataFromFile();
        }
    }
}
