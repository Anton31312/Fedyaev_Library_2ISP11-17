using System;
using System.Collections.Generic;
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
using LIBRARY1.ClassHelper;
using LIBRARY1.Windows;
using Microsoft.Win32;

namespace LIBRARY1.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddReaderWindow.xaml
    /// </summary>
    public partial class AddReaderWindow : Window
    {
        EF.Reader editReader = new EF.Reader();
        bool isEdit = true; // изменяем или добавляем пользователя
        string pathPhoto = null; // Для сохранения пути к изображению

        public AddReaderWindow()
        {
            InitializeComponent();

            cmbGender.ItemsSource = AppDate.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "NameGender";
            cmbGender.SelectedIndex = 0;

            isEdit = false;
        }

        public AddReaderWindow(EF.Reader reader)
        {
            InitializeComponent();

            // add image
            if (reader.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(reader.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    imgUser.Source = bitmapImage;

                }
            }

            //edit combobox
            cmbGender.ItemsSource = AppDate.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "NameGender";

            //edit TItle and content button
            tbTitle.Text = "Изменить данные читателя";
            btnAdd.Content = "Изменить";

            //Get value
            editReader = reader;

            txtLastName.Text = editReader.LastName;
            txtFirstName.Text = editReader.FirstName;
            txtPhone.Text = editReader.Phone;
            txtEmail.Text = editReader.Email;
            txtAddress.Text = editReader.Address;
            cmbGender.SelectedIndex = editReader.IDGender - 1;

            isEdit = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Валидация
            #region
            //Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поле Телефон не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Поле Email не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Поле Адрес не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка на количество символов

            if (txtLastName.Text.Length > 100)
            {
                MessageBox.Show("В поле Фамилия недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtFirstName.Text.Length > 100)
            {
                MessageBox.Show("В поле Имя недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhone.Text.Length > 20)
            {
                MessageBox.Show("В поле Телефон недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtEmail.Text.Length > 100)
            {
                MessageBox.Show("В поле Email недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtAddress.Text.Length > 100)
            {
                MessageBox.Show("В поле Адрес недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            //Проверка на ошибки в БД
            if (isEdit)
            {
                try
                {
                    //изменение читателя
                    editReader.LastName = txtLastName.Text;
                    editReader.FirstName = txtFirstName.Text;
                    editReader.Phone = txtPhone.Text;
                    editReader.Email = txtEmail.Text;
                    editReader.Address = txtAddress.Text;
                    editReader.IDGender = cmbGender.SelectedIndex + 1;

                    if (pathPhoto != null)
                    {
                        editReader.Photo = File.ReadAllBytes(pathPhoto);
                    }

                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Данные пользователя успешно изменены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        //Добавление нового читателя
                        EF.Reader reader = new EF.Reader();
                        reader.LastName = txtLastName.Text;
                        reader.FirstName = txtFirstName.Text;
                        reader.Phone = txtPhone.Text;
                        reader.Email = txtEmail.Text;
                        reader.Address = txtAddress.Text;
                        reader.IDGender = cmbGender.SelectedIndex + 1;

                        if (pathPhoto != null)
                        {
                            reader.Photo = File.ReadAllBytes(pathPhoto);
                        }


                        AppDate.Context.Reader.Add(reader);
                        AppDate.Context.SaveChanges();
                        MessageBox.Show("Пользователь успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        //Валидация
        //Запрет на ввод некоректных данных
        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox txtPhone)
            {
                txtPhone.Text = new string(txtPhone.Text.Where(ch => ch == '+' || ch == '-' || (ch >= '0' && ch <= '9') || ch == '(' || ch == ')') .ToArray());
                txtPhone.SelectionStart = e.Changes.First().Offset + 1;
                txtPhone.SelectionLength = 0;
            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                pathPhoto = openFileDialog.FileName;
            }
        }
    }
}
