using API.PropertiesUS.DAL.Dominio;
using Microsoft.EntityFrameworkCore;

namespace API.PropertiesUS.DAL
{
    /// <summary>
    /// Class to manage the database context
    /// </summary>
    public class DbContextPropertiesUS : DbContext
    {
        /// <summary>
        /// Private variable that gets the parameter for the connection string to the database
        /// </summary>
        private string dbParameter = string.Empty;

        /// <summary>
        /// Class constructor method for DBContext
        /// </summary>
        /// <param name="parameter">String with database connection string</param>
        public DbContextPropertiesUS(string parameter = null) : base()
        {
            if (parameter != null)
                dbParameter = parameter;
        }

        /// <summary>
        /// Setting method of the context towards the database
        /// </summary>
        /// <param name="optionsBuilder">EntityFramework object to configure the Database Context</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(dbParameter))
            {
                //Para Migrar la base de datos desde EntityFramework, habilite la asignacion de cadena de conexion directamente en esta clase
                dbParameter = "Server=localhost\\SQLEXPRESS;Database=PropertiesUS;Trusted_Connection=True;MultipleActiveResultSets=true;";
                //throw new System.Exception("Cadena de conexion a base de datos NO identificada");
            }
            optionsBuilder.UseSqlServer(dbParameter);
        }

        /// <summary>
        /// Method to model the database context with each entity
        /// </summary>
        /// <param name="modelBuilder">EntityFramework object to create the entity model in the database</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owners>()
                .HasKey(p => new { p.IdOwner });
            modelBuilder.Entity<Properties>()
                .HasKey(p => new { p.IdProperty });
            modelBuilder.Entity<PropertyImages>()
                .HasKey(p => new { p.IdPropertyImage });
            modelBuilder.Entity<PropertyTraces>()
                .HasKey(p => new { p.IdPropertyTrace });
        }

        /// <summary>
        /// Instance of the entity in the database: Owner
        /// </summary>
        public virtual DbSet<Owners> Owner { get; set; }
        /// <summary>
        /// Instance of the entity in the database: Property
        /// </summary>
        public virtual DbSet<Properties> Property { get; set; }
        /// <summary>
        /// Instance of the entity in the database: PropertyImage
        /// </summary>
        public virtual DbSet<PropertyImages> PropertyImage { get; set; }
        /// <summary>
        /// Instance of the entity in the database: PropertyTrace
        /// </summary>
        public virtual DbSet<PropertyTraces> PropertyTrace { get; set; }
    }
}
