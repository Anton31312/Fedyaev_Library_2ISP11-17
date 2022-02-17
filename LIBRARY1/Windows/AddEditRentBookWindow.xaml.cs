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
    /// Логика взаимодействия для AddEditRentBookWindow.xaml
    /// </summary>
    public partial class AddEditRentBookWindow : Window
    {
        EF.BookRental editBookRental = new EF.BookRental();

        public AddEditRentBookWindow()
        {
            InitializeComponent();

            cmbBook.ItemsSource = AppDate.Context.Book.ToList();
            cmbBook.DisplayMemberPath = "Title";
            cmbBook.SelectedIndex = 0;

            cmbReader.ItemsSource = AppDate.Context.Reader.ToList();
            cmbReader.DisplayMemberPath = "LastName";
            cmbReader.SelectedIndex = 0;

            cmbEmployer.ItemsSource = AppDate.Context.Emplovee.ToList();
            cmbEmployer.DisplayMemberPath = "LastName";
            cmbEmployer.SelectedIndex = 0;

        }

        private void btnAddRentBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultClick == MessageBoxResult.Yes)
                {
                    //Добавление нового читателя
                    EF.BookRental bookRental = new EF.BookRental();
                    bookRental.IDBook = cmbBook.SelectedIndex + 1;
                    bookRental.IDReader = cmbReader.SelectedIndex + 1;
                    bookRental.IDEmplovee = cmbEmployer.SelectedIndex + 1;
                    bookRental.StartDate = dtDateStart.DisplayDate;
                    bookRental.EndDate = dtDateEnd.DisplayDate;

                    AppDate.Context.BookRental.Add(bookRental);
                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Запись успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
