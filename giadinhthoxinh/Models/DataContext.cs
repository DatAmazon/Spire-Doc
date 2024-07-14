using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }
    }
}
