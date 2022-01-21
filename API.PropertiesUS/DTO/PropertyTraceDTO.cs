namespace API.PropertiesUS.DTO
{
    /// <summary>
    /// DTO to add property trace record
    /// </summary>
    public class PropertyTraceDTO
    {
        /// <summary>
        /// Related property record identifier
        /// </summary>
        public long? IdProperty { get; set; }
        /// <summary>
        /// Property name
        /// </summary>
        public string NameProperty { get; set; }
        /// <summary>
        /// Internal code of the property
        /// </summary>
        public string CodeInternalProperty { get; set; }
        /// <summary>
        /// Property sale date
        /// </summary>
        public string DateSale { get; set; }
        /// <summary>
        /// Name of the buyer or trace of the property
        /// </summary>
        public string NameTrace { get; set; }
        /// <summary>
        /// Sale value of the property
        /// </summary>
        public string ValueTrace { get; set; }
        /// <summary>
        /// Tax applied to the sale of the property
        /// </summary>
        public string TaxTrace { get; set; }
    }
}
