using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
            EnableGrid(GridTables);
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            EnableGrid(GridQuery);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            EnableGrid(gridSettings);
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
            EnableDataGrid(dataGridAuto, 0);
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            EnableDataGrid(dataGridClientts, 1);
        }

        private void btnWorkers_Click(object sender, RoutedEventArgs e)
        {
            EnableDataGrid(dataGridWorkers, 2);
        }

        private void btnParts_Click(object sender, RoutedEventArgs e)
        {
            EnableDataGrid(dataGridParts, 3);
        }

        private void btnWorks_Click(object sender, RoutedEventArgs e)
        {
            EnableDataGrid(dataGridWorks, 4);
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
                        AllData.Data.worksInformation = null;

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
                            AllData.Data.worksInformation = (WorksInformationEdited)dataGridWorks.SelectedItem;

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
                                    _db.Cars.Remove(new Car
                                    {
                                        Id = _db.Cars.Where(x => x.Id == ((Car)dataGridAuto.SelectedItem).Id)
                                        .Select(x => x.Id)
                                        .FirstOrDefault()
                                    });
                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;

                            case 1:

                                if (dataGridClientts.SelectedItem != null)
                                {
                                    _db.Clients.Remove(new Client
                                    {
                                        Id = _db.Clients.Where(x => x.Id == ((Client)dataGridClientts.SelectedItem).Id)
                                        .Select(x => x.Id)
                                        .FirstOrDefault()
                                    });

                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;

                            case 2:

                                if (dataGridWorkers.SelectedItem != null)
                                {
                                    _db.Workers.Remove(new Worker
                                    {
                                        Id = _db.Workers.Where(x => x.Id == ((Worker)dataGridWorkers.SelectedItem).Id)
                                        .Select(x => x.Id)
                                        .FirstOrDefault()
                                    });

                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;

                            case 3:

                                if (dataGridParts.SelectedItem != null)
                                {
                                    _db.Parts.Remove(new Part
                                    {
                                        Id = _db.Parts.Where(x => x.Id == ((PartsBrandsView)dataGridParts.SelectedItem).Id)
                                        .Select(x => x.Id)
                                        .FirstOrDefault()
                                    });

                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;

                            case 4:

                                if (dataGridWorks.SelectedItem != null)
                                {
                                    _db.Works.Remove(new Work
                                    {
                                        Id = _db.Works.Where(x => x.Id == ((WorksInformationEdited)dataGridWorks.SelectedItem).Id)
                                        .Select(x => x.Id)
                                        .FirstOrDefault()
                                    });

                                    _db.SaveChanges();

                                    MessageBox.Show("Запись успешно удалена!");

                                    LoadDbInDataGdrid();
                                }

                                break;


                        }
                    }
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Запись не может быть удалена т.к. используется в другой записи.");
                }
                catch (Exception ex)
                {
                    ErrorEcho(ex);
                }
            }
        }

        private void btnLostParts_Click(object sender, RoutedEventArgs e)
        {
            TextBoxWindow win = new();

            win.Title = "Введите ID работы";

            win.ShowDialog();

            using (RepairShopContext _db = new())
            {
                var query = from wp in _db.WorksParts
                            join p in _db.Parts on wp.PartId equals p.Id
                            select new
                            {
                                WorkId = wp.WorksId,
                                PartsName = p.PartName,
                                PartCount = wp.CountOfParts
                            };

                var gen = query.Where(x => x.WorkId == Convert.ToInt32(AllData.Data.TbWindowResult)).ToList();

                if (gen == null)
                {
                    MessageBox.Show("Записи запчастей в этой работе нет");
                }
                else
                {
                    dataGridQuery.ItemsSource = gen;
                }
            }
        }

        private void btnZeroParts_Click(object sender, RoutedEventArgs e)
        {
            using (RepairShopContext _db = new())
            {
                var gen = _db.Parts.Where(x => x.PartCount == 0).ToList();

                dataGridQuery.ItemsSource = gen;
            }
        }

        private void btnMostPricefullPart_Click(object sender, RoutedEventArgs e)
        {
            using (RepairShopContext _db = new())
            {
                var gen = _db.Parts.Where(x => x.PartSum == _db.Parts.Max(x => x.PartSum)).ToList();

                dataGridQuery.ItemsSource = gen;
            }
        }

        private void btnMostPricefulWork_Click(object sender, RoutedEventArgs e)
        {
            using (RepairShopContext _db = new())
            {
                var gen = _db.Works.Where(x => x.SumOfWork == _db.Works.Max(x => x.SumOfWork)).ToList();

                dataGridQuery.ItemsSource = gen;
            }
        }

        private void btnLessPricefulWork_Click(object sender, RoutedEventArgs e)
        {
            using (RepairShopContext _db = new())
            {
                var gen = _db.Works.Where(x => x.SumOfWork == _db.Works.Min(x => x.SumOfWork)).ToList();

                dataGridQuery.ItemsSource = gen;
            }
        }

        private void btnLessPricefulPart_Click(object sender, RoutedEventArgs e)
        {
            using (RepairShopContext _db = new())
            {
                var gen = _db.Parts.Where(x => x.PartSum == _db.Parts.Min(x => x.PartSum)).ToList();

                dataGridQuery.ItemsSource = gen;
            }
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

        public void EnableGrid(Grid element)
        {
            foreach (var i in _gridlist)
            {
                i.Visibility = Visibility.Hidden;
            }
            element.Visibility = Visibility.Visible;
        }

        public void EnableDataGrid(DataGrid element, int id)
        {
            foreach (var i in _datagridlist)
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

                _db.WorksInformationEdited.Load();
                dataGridWorks.ItemsSource = _db.WorksInformationEdited.ToList();

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
