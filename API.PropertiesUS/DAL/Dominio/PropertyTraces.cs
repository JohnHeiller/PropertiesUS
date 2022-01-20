using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.PropertiesUS.DAL.Dominio
{
    /// <summary>
    /// Entity with Properties trace data
    /// </summary>
    [Table("PropertyTrace", Schema = "dbo")]
    public class PropertyTraces
    {
        /// <summary>
        /// Properties trace record identifier
        /// </summary>
        [Key]
        [Comment("Properties trace record identifier")]
        public long IdPropertyTrace { get; set; }
        /// <summary>
        /// Properties record identifier FK
        /// </summary>
        [ForeignKey("Property")]
        [Comment("Properties record identifier FK")]
        public long IdProperty { get; set; }
        /// <summary>
        /// Properties sale date
        /// </summary>
        [Comment("Properties sale date")]
        public DateTime DateSale { get; set; }
        /// <summary>
        /// Properties buyer name
        /// </summary>
        [StringLength(50)]
        [Comment("Properties buyer name")]
        public string Name { get; set; }
        /// <summary>
        /// Sale value of the property
        /// </summary>
        [Comment("Sale value of the property")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }
        /// <summary>
        /// Tax value for the sale of the property
        /// </summary>
        [Comment("Tax value for the sale of the property")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Tax { get; set; }
        /// <summary>
        /// Property data domain
        /// </summary>
        public Properties Property { get; set; }
    }
}
