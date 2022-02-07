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
    /// Логика взаимодействия для BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {

        List<Book> bookList = new List<Book>();
        List<string> listSort = new List<string>() { "По умолчанию", "По названию", "По фамилии автора", "По имени автора", "По издательству" };

        public BookWindow()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();
        }



        private void Filter()
        {
            bookList = AppDate.Context.Book.ToList();
            bookList = bookList.
                            Where(i => i.Title.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.PublishHouse.NamePublishHouse.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    bookList = bookList.OrderBy(i => i.Title).ToList();
                    break;
                case 2:
                    bookList = bookList.OrderBy(i => i.Author.LastName).ToList();
                    break;
                case 3:
                    bookList = bookList.OrderBy(i => i.Author.FirstName).ToList();
                    break;
                case 4:
                    bookList = bookList.OrderBy(i => i.PublishHouse.NamePublishHouse).ToList();
                    break;
                default:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
            }

            listBook.ItemsSource = bookList;
        }


        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            this.Opacity = 0.2;
            addBookWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void listBook_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listBook.SelectedItem is EF.Book)
                {
                    try
                    {
                        var item = listBook.SelectedItem as EF.Book;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppDate.Context.Book.Remove(item);
                            AppDate.Context.SaveChanges();
                            MessageBox.Show("Книга успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
