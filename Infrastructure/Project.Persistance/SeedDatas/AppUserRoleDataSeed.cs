using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.SeedDatas
{
    public static class AppUserRoleDataSeed
    {
        public static void AppUserRoleSeed(this ModelBuilder modelBuilder)
        {
            List<AppUserRole> userroles = new()
            {
                new AppUserRole
                {
                    Id = 1,
                    UserId = 1, 
                    RoleId = 1,
                     InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 2,
                    UserId = 2, 
                    RoleId = 2,
                    InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 3,
                    UserId = 3, 
                    RoleId = 3,
                    InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 4,
                    UserId = 4, 
                    RoleId = 4,
                    InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 5,
                    UserId = 5, 
                    RoleId = 5,
                    InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 6,
                    UserId = 6, 
                    RoleId = 6,
                     InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 7,
                    UserId = 7, 
                    RoleId = 7,
                    InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 8,
                    UserId = 8, 
                    RoleId = 8,
                     InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 9,
                    UserId = 9, 
                    RoleId = 9,
                     InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 10,
                    UserId = 10, 
                    RoleId = 10,
                     InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },
                new AppUserRole
                {
                    Id = 11,
                    UserId = 11, 
                    RoleId = 11,
                     InsertedDate =new DateTime(2026,01,14),
                    Status = Domain.Enums.DataStatus.Inserted
                },

            };
            modelBuilder.Entity<AppUserRole>().HasData(userroles);
        }
    }
}
