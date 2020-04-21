namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.dacha")]
    public partial class dacha
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dacha()
        {
            dacha_owners = new HashSet<dacha_owners>();
        }

        public int id { get; set; }

        public int area { get; set; }

        public int awayfromtown { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public int district_id { get; set; }

        public string fullName { get { return name + " " + district.name + "  " + area + "  " + awayfromtown; }  }

        public virtual district district { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dacha_owners> dacha_owners { get; set; }
    }
}
