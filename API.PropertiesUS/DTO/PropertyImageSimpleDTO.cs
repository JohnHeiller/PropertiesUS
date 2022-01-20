using System.Drawing;

namespace API.PropertiesUS.DTO
{
    /// <summary>
    /// DTO to Update Property Image record 
    /// </summary>
    public class PropertyImageSimpleDTO
    {
        /// <summary>
        /// Image type value for property image
        /// </summary>
        public Image FileImage { get; set; }
        /// <summary>
        /// Base64 value of property image
        /// </summary>
        public string ImageBase64 { get; set; }
        /// <summary>
        /// Property image record identifier
        /// </summary>
        public long IdPropertyImage { get; set; }
    }
}
