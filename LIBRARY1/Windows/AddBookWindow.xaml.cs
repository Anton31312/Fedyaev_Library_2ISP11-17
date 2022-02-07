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
using LIBRARY1.ClassHelper;

namespace LIBRARY1.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();

            cmbSelection.ItemsSource = AppDate.Context.Selection.ToList();
            cmbSelection.DisplayMemberPath = "NameSelection";
            cmbSelection.SelectedIndex = 0;

            cmbPublishHouse.ItemsSource = AppDate.Context.PublishHouse.ToList();
            cmbPublishHouse.DisplayMemberPath = "NamePublishHouse";
            cmbPublishHouse.SelectedIndex = 0;

            cmbLastNameAuthor.ItemsSource = AppDate.Context.Author.ToList();
            cmbLastNameAuthor.DisplayMemberPath = "LastName";
            cmbLastNameAuthor.SelectedIndex = 0;

            cmbFirstNameAuthor.ItemsSource = AppDate.Context.Author.ToList();
            cmbFirstNameAuthor.DisplayMemberPath = "FirstName";
            cmbFirstNameAuthor.SelectedIndex = 0;
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            //Валидация
            //Проверка на пустоту

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Поле «Название книги» не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


           

            //Проверка на количество символов

            if (txtTitle.Text.Length > 100)
            {
                MessageBox.Show("В поле «Название книги» недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //Проверка на ошибки в БД

            try
            {
                var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultClick == MessageBoxResult.Yes)
                {
                    //Добавление нового читателя
                    EF.Book book = new EF.Book();
                    book.Title = txtTitle.Text;
                    book.IDAuthor = cmbLastNameAuthor.SelectedIndex + 1;
                    book.IDAuthor = cmbFirstNameAuthor.SelectedIndex + 1;
                    book.IDSection = cmbSelection.SelectedIndex + 1;
                    book.IDPublishHouse = cmbPublishHouse.SelectedIndex + 1;


                    AppDate.Context.Book.Add(book);
                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Книга успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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

