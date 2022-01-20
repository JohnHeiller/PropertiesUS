using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.PropertiesUS.DAL.Dominio
{
    /// <summary>
    /// Entity with Properties image data
    /// </summary>
    [Table("PropertyImage", Schema = "dbo")]
    public class PropertyImages
    {
        /// <summary>
        /// Properties image record identifier
        /// </summary>
        [Key]
        [Comment("Properties image record identifier")]
        public long IdPropertyImage { get; set; }
        /// <summary>
        /// Properties record identifier FK
        /// </summary>
        [ForeignKey("Property")]
        [Comment("Properties record identifier FK")]
        public long IdProperty { get; set; }
        /// <summary>
        /// Base64 value of properties image file
        /// </summary>
        [Required]
        [Comment("Base64 value of properties image file")]
        public string File { get; set; }
        /// <summary>
        /// Properties image record enabled indicator
        /// </summary>
        [Comment("Properties image record enabled indicator")]
        public bool Enabled { get; set; }
        /// <summary>
        /// Property data domain
        /// </summary>
        public Properties Property { get; set; }
    }
}
