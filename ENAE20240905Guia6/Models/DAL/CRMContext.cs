using Microsoft.EntityFrameworkCore;
using ENAE20240905Guia6.Models.EN;

namespace ENAE20240905Guia6.Models.DAL
{
    public class CRMContext : DbContext
    {
        public CRMContext(DbContextOptions<CRMContext>options) : base(options) { 
        }
        public DbSet<ProductENAE> ProductENAE { get; set; }
    }
}
