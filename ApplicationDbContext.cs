using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picheres1
{
    internal class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() : base ("DefaultConnection") 
        { 

        }
        public DbSet<Image> images { get; set; }
    }
}
