namespace API.PropertiesUS.DTO
{
    /// <summary>
    /// DTO for Property Price field update
    /// </summary>
    public class PropertyPriceDTO
    {
        /// <summary>
        /// Price to update
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// Property name. Optional field for identification of this one
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Internal code of the property. Optional field for identification of this one
        /// </summary>
        public string CodeInternal { get; set; }
        /// <summary>
        /// Property identifier. Optional field for identification of this one
        /// </summary>
        public long? IdProperty { get; set; }
    }
}
