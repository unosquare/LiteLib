﻿namespace Unosquare.Labs.LiteLib.Tests.Database
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CustomWarehouse")]
    internal class Warehouse : LiteModel
    {
        [LiteUnique]
        public string UniqueId { get; set; }

        public string Name { get; set; }

        public byte[] ByteProperty { get; set; }

        [NotMapped]
        public string Description { get; set; }

    }
}