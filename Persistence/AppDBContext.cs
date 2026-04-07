using Domain;
using Domain.Commun;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Images)
                .WithOne() // pas de navigation inverse obligatoire
                .OnDelete(DeleteBehavior.Cascade); // supprime les images si le projet est supprimé

            modelBuilder.Entity<Project>()
                 .HasOne<Category>()          
                 .WithMany()                   
                 .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Image>()
                 .HasIndex(i => i.ImgURL)
                 .IsUnique();
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in ChangeTracker.Entries<DomainBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "system";
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "system";
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedDate = DateTime.UtcNow;
                        break;
                }



              /*  if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "system";
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedBy = "system";
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Deleted)
                {
                    //entry.State = EntityState.Modified; // soft delete
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedBy = "system";
                    entry.Entity.DeletedDate = DateTime.UtcNow;
                }*/
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
