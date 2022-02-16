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
        EF.Book editBook = new EF.Book();

        bool isEdit = true;

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
            isEdit = false;
        }

        public AddBookWindow(EF.Book book)
        {
            InitializeComponent();

            editBook = book;
            //add combobox
            cmbSelection.ItemsSource = AppDate.Context.Selection.ToList();
            cmbSelection.DisplayMemberPath = "NameSelection";

            cmbPublishHouse.ItemsSource = AppDate.Context.PublishHouse.ToList();
            cmbPublishHouse.DisplayMemberPath = "NamePublishHouse";

            cmbLastNameAuthor.ItemsSource = AppDate.Context.Author.ToList();
            cmbLastNameAuthor.DisplayMemberPath = "LastName";

            cmbFirstNameAuthor.ItemsSource = AppDate.Context.Author.ToList();
            cmbFirstNameAuthor.DisplayMemberPath = "FirstName";

            //edit Title and content into button
            tbTitle.Text = "Изменения данных о книге";
            btnAddBook.Content = "Изменить";

            //get value
            txtTitle.Text = editBook.Title;
            cmbLastNameAuthor.SelectedIndex = editBook.IDAuthor - 1;
            cmbFirstNameAuthor.SelectedIndex = editBook.IDAuthor - 1;
            cmbSelection.SelectedIndex = editBook.IDSection - 1;
            cmbPublishHouse.SelectedIndex = editBook.IDPublishHouse;

            isEdit = true;
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            //Валидация
            #region
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
            #endregion


            if (isEdit)
            {
                try
                {
                    editBook.Title = txtTitle.Text;
                    editBook.IDAuthor = cmbLastNameAuthor.SelectedIndex + 1;
                    editBook.IDAuthor = cmbFirstNameAuthor.SelectedIndex + 1;
                    editBook.IDSection = cmbSelection.SelectedIndex + 1;
                    editBook.IDPublishHouse = cmbPublishHouse.SelectedIndex + 1;

                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Информация о книге успешно изменена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
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
                }
            }
        }
    }
}