using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace WPFLernmaterial
{
    /// <summary>
    /// Interaktionslogik für LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window

        
    
    {
        private string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\coanh\OneDrive\Documents\c#\Abschlussarbeit Muster\WPFLernmaterial\WPFLernmaterial\Database1.mdf;Integrated Security = True";
        
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connect = new SqlConnection(sql);
            SqlCommand command = new SqlCommand("select count(1) from login where UserName='"+UserName.Text+"' and Password='"+Pass.Password.ToString()+"'", connect);

            try
            {
                connect.Open();
                int count =(int) command.ExecuteScalar();
                
                if (count >0)
                {
                    MainProgram main = new MainProgram();
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("User oder Passwort nicht korrekt!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        
    }
}
