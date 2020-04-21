namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.owners")]
    public partial class owners
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public owners()
        {
            dacha_owners = new HashSet<dacha_owners>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string fio { get; set; }

        public int age { get; set; }

        public long number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dacha_owners> dacha_owners { get; set; }
    }
}
