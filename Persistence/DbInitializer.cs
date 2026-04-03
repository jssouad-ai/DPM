using Domain;
using Domain.Commun;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer
    {
        public async Task SeedData(AppDBContext context)
        {
            // Vérifie si des données existent déjà
            if (await context.Projects.AnyAsync() ||
                await context.Categories.AnyAsync() ||
                await context.Images.AnyAsync() ||
                await context.Users.AnyAsync() ||
                await context.Roles.AnyAsync() ||
                await context.Permissions.AnyAsync())
            { return; }

            var now = DateTime.UtcNow;
            const string seedUser = "seed";
            var updateDate = now.ToString("O");

            var category = new Category
            {
                CategoryName = "Residentiel"

            };

            Stamp(category, seedUser, now, updateDate);

            var ImgURL = "./API/Images/ResidenceLesOliviers.jpeg";
            var image = new Image(ImgURL,"caption")
            {
                ImgCaption = "Image_Résidence Les Oliviers",

            };
            Stamp(image, seedUser, now, updateDate);

            var project = new Project
            {
                ProjectName = "Résidence Les Oliviers",
                ProjectDescription = "Description_Résidence Les Oliviers",
                CategoryId = category.Id
            };
            Stamp(project, seedUser, now, updateDate);

            var adminRole = new Role
            {
                Id = Guid.NewGuid().ToString(),
                RoleName = "Admin"
            };
            Stamp(adminRole, seedUser, now, updateDate);

            var UpdatePermission = new Permission
            {
                Id = Guid.NewGuid().ToString(),
                PermissionCode = "Protfolio_Update"
            };
            Stamp(UpdatePermission, seedUser, now, updateDate);

            var user = new User
            {
                UserName = "User1",
                UserRole = "Admin",
                Email = "admin@dpm.com",
                IsActive = true
            };
            Stamp(user, seedUser, now, updateDate);



            await context.AddRangeAsync(category, image,project, adminRole, UpdatePermission, user);
            await context.SaveChangesAsync();
        }

        private static void Stamp(DomainBase entity, string user, DateTime now, string  updateDate)
        {
            entity.CreatedBy = user;
            entity.CreatedDate = now;
            entity.UpdatedBy = user;
            //entity.UpdatedDate = updateDate;

            // These are non-nullable and mapped as NOT NULL in the DB
            entity.DeletedBy = string.Empty;
            entity.DeletedDate = DateTime.UnixEpoch;
            entity.IsDeleted = false;
        }


    }


}
