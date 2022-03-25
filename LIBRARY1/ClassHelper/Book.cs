using LIBRARY1.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY1.EF
{
    public partial class Book
    {
        public string GetAuthor { get => $"ФИО автора: {Author.LastName} {Author.FirstName} {Author.Patronymic}"; }
        public string GetTitle { get => $"Название: {Title}"; }
        public string GetPublishHouse { get => $"Редакция: {PublishHouse.NamePublishHouse}"; }
        public string GetSection{ get => $"Секция книги: {Selection.NameSelection}"; }
        public string GetCost{ get => $"Цена: {Cost}"; }
    }
}
