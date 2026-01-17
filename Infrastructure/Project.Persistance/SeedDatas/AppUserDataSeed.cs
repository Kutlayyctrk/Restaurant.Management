using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;

namespace Project.Persistance.SeedDatas
{
    public static class AppUserDataSeed
    {
        public static void AppUserSeed(this ModelBuilder modelBuilder)
        {
            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();

            List<AppUser> appUsers = new()
            {
                new AppUser
                {
                    Id=1,
                    UserName="admin",
                    NormalizedUserName="ADMIN",
                    Email="admin@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"admin123")
                },
                new AppUser
                {
                    Id=2,
                    UserName="mudur",
                    NormalizedUserName="MUDUR",
                    Email="mudur@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"mudur123")
                },
                new AppUser
                {
                    Id=3,
                    UserName="insankaynaklari",
                    NormalizedUserName="INSANKAYNAKLARI",
                    Email="insankaynaklari@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"ik123")
                },
                new AppUser
                {
                    Id=4,
                    UserName="mutfaksef",
                    NormalizedUserName="MUTFAKSEF",
                    Email="mutfaksef@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"mutfak123")
                },
                new AppUser
                {
                    Id=5,
                    UserName="barsef",
                    NormalizedUserName="BARSEF",
                    Email="barsef@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"bar123")
                },
                new AppUser
                {
                    Id=6,
                    UserName="asci",
                    NormalizedUserName="ASCI",
                    Email="asci@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"asci123")
                },
                new AppUser
                {
                    Id=7,
                    UserName="barmen",
                    NormalizedUserName="BARMEN",
                    Email="barmen@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"barmen123")
                },
                new AppUser
                {
                    Id=8,
                    UserName="garson",
                    NormalizedUserName="GARSON",
                    Email="garson@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"garson123")
                },
                new AppUser
                {
                    Id=9,
                    UserName="idaripersonel",
                    NormalizedUserName="IDARIPERSONEL",
                    Email="idaripersonel@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"idari123")
                },
                new AppUser
                {
                    Id=10,
                    UserName="hizmetpersoneli",
                    NormalizedUserName="HIZMETPERSONELI",
                    Email="hizmetpersoneli@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"hizmet123")
                },
                new AppUser
                {
                    Id=11,
                    UserName="stajyer",
                    NormalizedUserName="STAJYER",
                    Email="stajyer@restaurantmanagement.com",
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString(),
                    ConcurrencyStamp= Guid.NewGuid().ToString(),
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                    PasswordHash=passwordHasher.HashPassword(null,"stajyer123")
                }
            };

            modelBuilder.Entity<AppUser>().HasData(appUsers);
        }
    }
}