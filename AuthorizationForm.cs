using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalRU
{
    public partial class AuthorizationForm : Form
    {
        #region INITIALIZATION
        public AuthorizationForm()
        {
            InitializeComponent();
        }
        #endregion
        #region authorization
        private void authorization()
        {
            //...
            //authorization code
            //...
            string login = this.login_TextBox.Text,
                   pswd = this.pswd_TextBox.Text,
                   true_login = "login",
                   true_pswd = "pswd";               
                
            if (login == true_login && pswd == true_pswd)
            {
                MainWindow MW = new MainWindow();
                MW.Show();
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Логин или пароль введены не верно, попробуйте еще раз");                
            }
        }
        #endregion
        #region EVENTS
        private void AF_ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void AF_cut_down_button_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void AF_cut_down_button_MouseHover(object sender, EventArgs e)
        {
            this.AF_cut_down_button.Image = global::PersonalRU.Properties.Resources.cut_down_cover;
        }
        private void AF_cut_down_button_MouseLeave(object sender, EventArgs e)
        {
            this.AF_cut_down_button.Image = global::PersonalRU.Properties.Resources.cut_down_def;
        }

        private void AF_ExitButton_MouseHover(object sender, EventArgs e)
        {
            this.AF_ExitButton.Image = global::PersonalRU.Properties.Resources.exit_btn_cover;
        }

        private void AF_ExitButton_MouseLeave(object sender, EventArgs e)
        {
            this.AF_ExitButton.Image = global::PersonalRU.Properties.Resources.exit_btn_def;
        }

        private void AF_cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        private void AF_Login_button_Click(object sender, EventArgs e)
        {
            this.authorization();
        }
        #endregion        
    }
}
