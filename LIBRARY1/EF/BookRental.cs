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
    using LIBRARY1.ClassHelper;
    
    public partial class BookRental
    {
        public int IDReader { get; set; }
        public int IDBook { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<int> IDEmplovee { get; set; }
        public decimal DebtRatio { get; set; }
        public Nullable<bool> IsBack { get; set; }

        public double DebtProperty {
            get
              {               
                return DebtRationClass.Debt((double)(Book.Cost), StartDate);
              }
            }

        public virtual Book Book { get; set; }
        public virtual Emplovee Emplovee { get; set; }
        public virtual Reader Reader { get; set; }
    }
}
