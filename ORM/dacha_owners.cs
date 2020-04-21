namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.dacha_owners")]
    public partial class dacha_owners
    {
        public int id_owners { get; set; }

        public int id_dacha { get; set; }

        public int id { get; set; }

        public virtual dacha dacha { get; set; }

        public virtual owners owners { get; set; }
    }
}
