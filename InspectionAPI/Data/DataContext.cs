using InspectionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InspectionAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> option) : base(option) { }

        public DbSet<Inspection>     Inspections    { get; set; }
        public DbSet<InspectionType> InspectionType { get; set; }
        public DbSet<Status>         Status         { get; set; }

    }
}
