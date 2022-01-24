using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace API.PropertiesUS.DTO
{
    /// <summary>
    /// Owner data DTO
    /// </summary>
    public class OwnerDTO
    {
        /// <summary>
        /// Owner's name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Owner's Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// (Optional) Owner's phone number
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// (Optional) Photo for identification of the owner. Image can be sent in Base64 or Image type
        /// </summary>
        public Image Photo { get; set; }
        /// <summary>
        /// (Optional) Base64 of the photo to identify the owner. Image can be sent in Base64 or Image type
        /// </summary>
        public string PhotoBase64 { get; set; }
        /// <summary>
        /// (Optional) Owner's day of birth
        /// </summary>
        public string Birthday { get; set; }
    }
}
