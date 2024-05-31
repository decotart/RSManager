using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using RSManager.DataBase;

namespace RSManager.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstPass = pbFirstPass.Password,
                secondPass = pbSecondPass.Password,
                userName = tbUserName.Text;

                if (firstPass == secondPass)
                {
                    using (RepairShopContext _db = new())
                    {
                        try
                        {
                            var gen  = _db.Autorizations.Single(x => x.UserName == userName);
                            MessageBox.Show("Такой пользователь уже существует!");
                        }
                        catch
                        {
                            Autorization _user = new Autorization();

                            _user.UserName = userName;
                            _user.UserPassword = firstPass;

                            _db.Autorizations.Add(_user);
                            _db.SaveChanges();

                            MessageBox.Show("Успешно!");
                            Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (AllData.Data.isAddingUser == true)
            {
                lblName.Content = "Введите имя";
                btnReg.Content = "Добавить";

                Title = "Добавить пользователя";
            }
        }
    }

}
