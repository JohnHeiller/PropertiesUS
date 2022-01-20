using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.PropertiesUS.DAL.Dominio
{
    /// <summary>
    /// Entity with Owners data
    /// </summary>
    [Table("Owner", Schema = "dbo")]
    public class Owners
    {
        /// <summary>
        /// Owners record identifier
        /// </summary>
        [Key]
        [Comment("Owners record identifier")]
        public long IdOwner { get; set; }
        /// <summary>
        /// Owners full name
        /// </summary>
        [Required]
        [StringLength(50)]
        [Comment("Owners full name")]
        public string Name { get; set; }
        /// <summary>
        /// Owners residence address
        /// </summary>
        [StringLength(150)]
        [Comment("Owners residence address")]
        public string Address { get; set; }
        /// <summary>
        /// Owners phone number
        /// </summary>
        [StringLength(15)]
        [Comment("Owners phone number")]
        public string Phone { get; set; }
        /// <summary>
        /// Base64 value of owners photo
        /// </summary>
        [Comment("Base64 value of owners photo")]
        public string Photo { get; set; }
        /// <summary>
        /// Owners date of birth
        /// </summary>
        [Comment("Owners date of birth")]
        public DateTime? Birthday { get; set; }
    }
}
