namespace API.PropertiesUS.DTO
{
    /// <summary>
    /// Owner data DTO
    /// </summary>
    public class PropertyDTO
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string NameProperty { get; set; }
        /// <summary>
        /// Property Address
        /// </summary>
        public string AddressProperty { get; set; }
        /// <summary>
        /// Property price
        /// </summary>
        public string PriceProperty { get; set; }
        /// <summary>
        /// Internal code of the property. If not sent, it will be auto-generated
        /// </summary>
        public string CodeInternalProperty { get; set; }
        /// <summary>
        /// Year of construction of the property
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// Identifier of the owner of the property. Optional field for property owner query, the owner's ID or Name must be sent
        /// </summary>
        public long? IdOwner { get; set; }
        /// <summary>
        /// Name of the owner of the property. Optional field for property owner query, the owner's ID or Name must be sent
        /// </summary>
        public string NameOwner { get; set; }
        /// <summary>
        /// Identifier of the property. Optional field used for property query
        /// </summary>
        public long? IdProperty { get; set; }
    }
}
