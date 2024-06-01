using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using Microsoft.EntityFrameworkCore;
using RSManager.DataBase;
using RSManager.Windows;

namespace RSManager
{
    /// <summary>
    /// Логика взаимодействия для GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        public GeneralWindow()
        {
            InitializeComponent();
        }

        List<Grid> _gridlist = new List<Grid>();
        List<DataGrid> _datagridlist = new List<DataGrid>();

        private void btnTables_Click(object sender, RoutedEventArgs e)
        {
            EnableGrid(_gridlist, GridTables);
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            EnableGrid(_gridlist, GridQuery);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            EnableGrid(_gridlist, gridSettings);
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная программа создана для демонстрации базы данных из курсового проекта.\n" +
                "Подготовил студент группы ИСП-31 Копенкин Егор.\n" +
                "Тема курсового проекта: Автомастерская.\n" +
                "Более подробные сведения о проделанной работе содержатся в приложенном отчете.");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
            EnableDataGrid(_datagridlist, dataGridAuto, 0);
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            EnableDataGrid(_datagridlist, dataGridClientts, 1);
        }

        private void btnWorkers_Click(object sender, RoutedEventArgs e)
        {
            EnableDataGrid(_datagridlist, dataGridWorkers, 2);
        }

        private void btnParts_Click(object sender, RoutedEventArgs e)
        {
            EnableDataGrid(_datagridlist, dataGridParts, 3);
        }

        private void btnWorks_Click(object sender, RoutedEventArgs e)
        {
            EnableDataGrid(_datagridlist, dataGridWorks, 4);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllData.Data.isEditing = false;

                Windows.AddEditWindow win = new();
                win.Owner = this;

                switch (AllData.Data.selectedTableIndex)
                {

                    case 0:
                        AllData.Data.carTable = null;

                        win.ShowDialog();

                        LoadDbInDataGdrid();

                        break;

                    case 1:
                        AllData.Data.clientTable = null;

                        win.ShowDialog();

                        LoadDbInDataGdrid();

                        break;

                    case 2:
                        AllData.Data.workerTable = null;

                        win.ShowDialog();

                        LoadDbInDataGdrid();

                        break;

                    case 3:
                        AllData.Data.partTable = null;

                        win.ShowDialog();

                        LoadDbInDataGdrid();

                        break;

                    case 4:
                        AllData.Data.workTable = null;

                        win.ShowDialog();

                        LoadDbInDataGdrid();

                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorEcho(ex);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllData.Data.isEditing = true;

                Windows.AddEditWindow win = new();
                win.Owner = this;

                switch (AllData.Data.selectedTableIndex)
                {
                    case 0:

                        if (dataGridAuto.SelectedItem != null)
                        {
                            AllData.Data.carTable = (Car)dataGridAuto.SelectedItem;

                            win.ShowDialog();

                            LoadDbInDataGdrid();
                        }

                        break;


                    case 1:

                        if (dataGridClientts.SelectedItem != null)
                        {
                            AllData.Data.clientTable = (Client)dataGridClientts.SelectedItem;

                            win.ShowDialog();

                            LoadDbInDataGdrid();
                        }

                        break;

                    case 2:

                        if (dataGridWorkers.SelectedItem != null)
                        {
                            AllData.Data.workerTable = (Worker)dataGridWorkers.SelectedItem;

                            win.ShowDialog();

                            LoadDbInDataGdrid();
                        }

                        break;

                    case 3:

                        if (dataGridParts.SelectedItem != null)
                        {
                            var gen = dataGridParts.SelectedItem;

                            AllData.Data.partsBrandsView = (PartsBrandsView)dataGridParts.SelectedItem;                          

                            win.ShowDialog();

                            LoadDbInDataGdrid();
                        }

                        break;

                    case 4:

                        if (dataGridWorks.SelectedItem != null)
                        {
                            AllData.Data.workTable = (Work)dataGridWorks.SelectedItem;

                            win.ShowDialog();

                            LoadDbInDataGdrid();
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorEcho(ex);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (RepairShopContext _db = new())
                    {
                        switch (AllData.Data.selectedTableIndex)
                        {
                            case 0:

                                if (dataGridAuto.SelectedItem != null)
                                {
                                    _db.Cars.Remove((Car)dataGridAuto.SelectedItem);
                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;

                            case 1:

                                if (dataGridClientts.SelectedItem != null)
                                {
                                    _db.Clients.Remove((Client)dataGridClientts.SelectedItem);
                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;

                            case 2:

                                if (dataGridWorkers.SelectedItem != null)
                                {
                                    _db.Workers.Remove((Worker)dataGridWorkers.SelectedItem);
                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;

                            case 3:

                                if (dataGridParts.SelectedItem != null)
                                {
                                    _db.Parts.Remove((Part)dataGridParts.SelectedItem);
                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;

                            case 4:

                                if (dataGridWorks.SelectedItem != null)
                                {
                                    _db.Works.Remove((Work)dataGridWorks.SelectedItem);
                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;


                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorEcho(ex);
                }
            }
        }

        private void btnLostParts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnZeroParts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMostPricefullPart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMostPricefulWork_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLessPricefulWork_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLessPricefulPart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AllData.Data.isAddingUser = true;

            RegWindow win = new();

            win.ShowDialog();
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new();

            win.Show();

            Close();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            TextBoxWindow win = new();

            win.Title = "Введите имя пользователя";
            win.ShowDialog();

            using (RepairShopContext _db = new())
            {
                try
                {
                    var gen = _db.Autorizations.Single(x => x.UserName == AllData.Data.TbWindowResult);

                    _db.Autorizations.Remove(gen);
                    _db.SaveChanges();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Такого пользователя не существует!");
                }
                catch (Exception ex)
                {
                    ErrorEcho(ex);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _gridlist.Add(gridSettings);
                _gridlist.Add(GridTables);
                _gridlist.Add(GridQuery);

                _datagridlist.Add(dataGridWorks);
                _datagridlist.Add(dataGridParts);
                _datagridlist.Add(dataGridWorkers);
                _datagridlist.Add(dataGridClientts);
                _datagridlist.Add(dataGridAuto);

                LoadDbInDataGdrid();
            }
            catch (Exception ex)
            {
                ErrorEcho(ex);
            }
        }

        public void EnableGrid(List<Grid> list, Grid element)
        {
            foreach (var i in list)
            {
                i.Visibility = Visibility.Hidden;
            }
            element.Visibility = Visibility.Visible;
        }

        public void EnableDataGrid(List<DataGrid> list, DataGrid element, int id)
        {
            foreach (var i in list)
            {
                i.Visibility = Visibility.Hidden;
            }
            element.Visibility = Visibility.Visible;

            AllData.Data.selectedTableIndex = id;
        }

        public void LoadDbInDataGdrid()
        {
            using (RepairShopContext _db = new())
            {
                _db.Cars.Load();
                dataGridAuto.ItemsSource = _db.Cars.ToList();

                _db.Workers.Load();
                dataGridWorkers.ItemsSource = _db.Workers.ToList();

                _db.Works.Load();
                dataGridWorks.ItemsSource = _db.Works.ToList();

                _db.PartsBrandsView.Load();
                dataGridParts.ItemsSource = _db.PartsBrandsView.ToList();

                _db.Clients.Load();
                dataGridClientts.ItemsSource = _db.Clients.ToList();
            }
        }

        public void ErrorEcho(Exception ex)
        {
            MessageBox.Show($"Что-то пошло не так. \nСообщение об ошибке: {ex}");
        }
    }
}
