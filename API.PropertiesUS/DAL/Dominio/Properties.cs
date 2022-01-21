using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.PropertiesUS.DAL.Dominio
{
    /// <summary>
    /// Entity with Properties data
    /// </summary>
    [Table("Property", Schema = "dbo")]
    public class Properties
    {
        /// <summary>
        /// Properties record identifier
        /// </summary>
        [Key]
        [Comment("Properties record identifier")]
        public long IdProperty { get; set; }
        /// <summary>
        /// Properties name
        /// </summary>
        [StringLength(50)]
        [Comment("Properties name")]
        public string Name { get; set; }
        /// <summary>
        /// Properties address
        /// </summary>
        [Required]
        [StringLength(150)]
        [Comment("Properties address")]
        public string Address { get; set; }
        /// <summary>
        /// Properties sale price
        /// </summary>
        [Required]
        [Comment("Properties sale price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        /// <summary>
        /// Internal code for properties identification
        /// </summary>
        [StringLength(40)]
        [Comment("Internal code for properties identification")]
        public string CodeInternal { get; set; }
        /// <summary>
        /// Year of construction of the property
        /// </summary>
        [StringLength(4)]
        [Comment("Year of construction of the property")]
        public string Year { get; set; }
        /// <summary>
        /// Owners record identifier FK
        /// </summary>
        [ForeignKey("Owner")]
        [Comment("Owners record identifier FK")]
        public long IdOwner { get; set; }
        /// <summary>
        /// Owner data domain
        /// </summary>
        public Owners Owner { get; set; }
    }
}
