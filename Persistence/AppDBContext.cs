using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDBContext(DbContextOptions options) : DbContext(options)
    {
        
        public required DbSet<Project> Projects { get; set; }
        public required DbSet<Category> Categories { get; set; }
        public required DbSet<Image> Images { get; set; }
        public required DbSet<Role> Roles { get; set; }
        public required DbSet<User> Users { get; set; }
        public required DbSet<Permission> Permissions { get; set; }


    }
}
