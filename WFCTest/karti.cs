//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WFCTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class karti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public karti()
        {
            this.prodazbas = new HashSet<prodazba>();
            this.klients = new HashSet<klient>();
        }
    
        public int id_karti { get; set; }
        public int n_id_nastan { get; set; }
        public string zona { get; set; }
        public string red { get; set; }
        public string mesto { get; set; }
        public Nullable<float> cena { get; set; }
        public string barkod { get; set; }
        public Nullable<decimal> lager { get; set; }
    
        public virtual nastan nastan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prodazba> prodazbas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<klient> klients { get; set; }
    }
}
