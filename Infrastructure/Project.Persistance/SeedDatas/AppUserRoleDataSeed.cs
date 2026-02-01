using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;

namespace Project.Persistance.SeedDatas
{
    public static class AppUserRoleDataSeed
    {
        public static void AppUserRoleSeed(this ModelBuilder modelBuilder)
        {
            var userroles = new List<AppUserRole>
            {
            
                new AppUserRole { UserId = 2,  RoleId = 2,  InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted },
                new AppUserRole { UserId = 3,  RoleId = 3,  InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted },
                new AppUserRole { UserId = 4,  RoleId = 4,  InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted },
                new AppUserRole { UserId = 5,  RoleId = 5,  InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted },
                new AppUserRole { UserId = 6,  RoleId = 6,  InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted },
                new AppUserRole { UserId = 7,  RoleId = 7,  InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted },
                new AppUserRole { UserId = 8,  RoleId = 8,  InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted },
                new AppUserRole { UserId = 9,  RoleId = 10,  InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted },
                new AppUserRole { UserId = 10, RoleId = 9, InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted },
                new AppUserRole { UserId = 11, RoleId = 11, InsertedDate = new DateTime(2026,01,14), Status = Domain.Enums.DataStatus.Inserted }
            };

            modelBuilder.Entity<AppUserRole>().HasData(userroles);
        }
    }
}