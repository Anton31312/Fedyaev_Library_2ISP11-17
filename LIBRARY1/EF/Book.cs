//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LIBRARY1.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.BookRental = new HashSet<BookRental>();
            this.Genre = new HashSet<Genre>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public int IDAuthor { get; set; }
        public int IDSection { get; set; }
        public int IDPublishHouse { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual PublishHouse PublishHouse { get; set; }
        public virtual Selection Selection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookRental> BookRental { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genre { get; set; }
    }
}
