using System;
using System.Drawing;
using System.IO;

namespace API.PropertiesUS.BL
{
    /// <summary>
    /// Class for common validations in the other BLs, such as Utilities
    /// </summary>
    public class UtilBL
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public UtilBL()
        { 
        
        }

        /// <summary>
        /// Method to convert a value of type Image to base64
        /// </summary>
        /// <param name="image">Image file type</param>
        /// <returns>image base64</returns>
        public string GetBase64(Image image)
        {
            try
            {
                using MemoryStream m = new MemoryStream();
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }
}
