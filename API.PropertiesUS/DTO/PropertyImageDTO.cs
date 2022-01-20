using System.Drawing;

namespace API.PropertiesUS.DTO
{
    /// <summary>
    /// Property image data DTO
    /// </summary>
    public class PropertyImageDTO
    {
        /// <summary>
        /// Image file of the property. The image can be uploaded in Image or Base64 type
        /// </summary>
        public Image FileImage { get; set; }
        /// <summary>
        /// Image file of the property. The image can be uploaded in Image or Base64 type
        /// </summary>
        public string ImageBase64 { get; set; }
        /// <summary>
        /// Internal code of the property. Optional for Property query
        /// </summary>
        public string CodeInternalProperty { get; set; }
        /// <summary>
        /// Property name. Optional for Property query
        /// </summary>
        public string NameProperty { get; set; }
        /// <summary>
        /// Property identifier. Optional for Property query
        /// </summary>
        public long? IdProperty { get; set; }
    }
}
