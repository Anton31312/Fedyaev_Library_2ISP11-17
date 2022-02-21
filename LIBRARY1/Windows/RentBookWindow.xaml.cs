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
using LIBRARY1.EF;
using LIBRARY1.ClassHelper;

namespace LIBRARY1.Windows
{
    /// <summary>
    /// Логика взаимодействия для RentBookWindow.xaml
    /// </summary>
    public partial class RentBookWindow : Window
    {
        List<BookRental> rentBookList = new List<BookRental>();
        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии читателя", "По имени читателя", "По названию книги"};

        public RentBookWindow()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            rentBookList = AppDate.Context.BookRental.ToList();
            rentBookList = rentBookList.
                            Where(i => i.Reader.LastName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.Reader.FirstName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.Book.Title.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    rentBookList = rentBookList.OrderBy(i => i.IDReader).ToList();
                    break;
                case 1:
                    rentBookList = rentBookList.OrderBy(i => i.Reader.LastName).ToList();
                    break;
                case 2:
                    rentBookList = rentBookList.OrderBy(i => i.Reader.FirstName).ToList();
                    break;
                case 3:
                    rentBookList = rentBookList.OrderBy(i => i.Book.Title).ToList();
                    break;
                default:
                    rentBookList = rentBookList.OrderBy(i => i.IDReader).ToList();
                    break;
            }

            listRentBook.ItemsSource = rentBookList;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();

        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }



        private void listRentBook_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listRentBook.SelectedItem is EF.Book)
                {
                    try
                    {
                        var item = listRentBook.SelectedItem as EF.Book;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppDate.Context.Book.Remove(item);
                            AppDate.Context.SaveChanges();
                            MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            Filter();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void btnAddRentBook_Click(object sender, RoutedEventArgs e)
        {
            AddEditRentBookWindow addRentBookWindow = new AddEditRentBookWindow();
            this.Opacity = 0.2;
            addRentBookWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }
    }
}
