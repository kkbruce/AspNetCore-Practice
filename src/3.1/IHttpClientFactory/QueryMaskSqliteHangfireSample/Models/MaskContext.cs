using Microsoft.EntityFrameworkCore;

namespace QueryMaskSample.Models
{
    public class MaskContext : DbContext
    {
        public DbSet<MedicalMask> MedicalMasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=MedicalMask.db");
    }
}
