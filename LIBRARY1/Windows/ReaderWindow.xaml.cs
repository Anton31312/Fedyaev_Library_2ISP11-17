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
using LIBRARY1.Windows;
using LIBRARY1.EF;


namespace LIBRARY1.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReaderWindow.xaml
    /// </summary>
    public partial class ReaderWindow : Window
    {

        List<Reader> readerList = new List<Reader>();
        List<string> listSort = new List<string>() {"По умолчанию", "По фамилии", "По имени", "По адресу" };

        public ReaderWindow()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();

        }

        private void Filter()
        {
            readerList = AppDate.Context.Reader.ToList();
            readerList = readerList.
                            Where(i => i.LastName.ToLower().Contains(txtSearch.Text.ToLower()) || 
                            i.FirstName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    readerList = readerList.OrderBy(i => i.LastName).ToList();
                    break;
                case 2:
                    readerList = readerList.OrderBy(i => i.FirstName).ToList();
                    break;
                case 3:
                    readerList = readerList.OrderBy(i => i.Address).ToList();
                    break;
                default:
                    readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;
            }

            listReader.ItemsSource = readerList;
        }

        private void btnAddReader_Click(object sender, RoutedEventArgs e)
        {
            AddReaderWindow addReaderWindow = new AddReaderWindow();
            this.Opacity = 0.2;
            addReaderWindow.ShowDialog();
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

        private void listReader_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete )
            {
                if (listReader.SelectedItem is EF.Reader)
                {
                    try
                    {
                        var item = listReader.SelectedItem as EF.Reader;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppDate.Context.Reader.Remove(item);
                            AppDate.Context.SaveChanges();
                            MessageBox.Show("Пользователь успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
