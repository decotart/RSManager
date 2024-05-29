
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace RSManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = tbLogin.Text,
                password = pbPass.Password;
                bool access = false;

                using (RepairShopContext db = new())
                {
                    var gen = db.Autorizations.FromSql($"select * from Autorization where UserName = '{login}'").ToList();

                    foreach (var i in gen)
                    {
                        if (i.UserPassword == password)
                        {
                            access = true;
                        }
                    }

                    if (access)
                    {
                        MessageBox.Show("da");
                    }
                    else
                    {
                        MessageBox.Show("net");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}