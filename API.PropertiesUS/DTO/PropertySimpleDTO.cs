namespace API.PropertiesUS.DTO
{
    /// <summary>
    /// DTO para consultar la cantidad de registros de visitas a una propiedad
    /// </summary>
    public class PropertySimpleDTO
    {
        /// <summary>
        /// Nombre de la propiedad
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Codigo interno de la propiedad
        /// </summary>
        public string CodeInternal { get; set; }
        /// <summary>
        /// Identificador del registro de la propiedad
        /// </summary>
        public long? IdProperty { get; set; }
    }
}
