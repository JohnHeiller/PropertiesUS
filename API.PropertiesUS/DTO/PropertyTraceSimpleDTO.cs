namespace API.PropertiesUS.DTO
{
    /// <summary>
    /// DTO to Update property trace record
    /// </summary>
    public class PropertyTraceSimpleDTO
    {
        /// <summary>
        /// Related property trace record identifier
        /// </summary>
        public long IdPropertyTrace { get; set; }
        /// <summary>
        /// Name of the buyer or trace of the property
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Property sale date
        /// </summary>
        public string DateSale { get; set; }
        /// <summary>
        /// Sale value of the property
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Tax applied to the sale of the property
        /// </summary>
        public string Tax { get; set; }
    }
}
