using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.SeedDatas
{
    public static class AppUserProfileDataSeed
    {
        public static void AppUserProfileSeed(this ModelBuilder modelBuilder)
        {
            List<AppUserProfile> userprofiles = new()
            {
              
                new AppUserProfile
                {
                    Id=2,
                    FirstName="Mehmet",
                    LastName="Demir",
                    TCKNo="10987654321",
                    Salary=150000,
                    HireDate=new DateTime(2021,3,10),
                    BirthDate=new DateTime(1985,8,15),
                    AppUserId=2,
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,

                },
                new AppUserProfile 
                {
                    Id=3,
                    FirstName="Ayşe",
                    LastName="Kara",
                    TCKNo="23456789012",
                    Salary=120000,
                    HireDate=new DateTime(2022,6,5),
                    BirthDate=new DateTime(1992,11,30),
                    AppUserId=3,
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                },
                new AppUserProfile
                {
                    Id=4,
                    FirstName="Fatma",
                    LastName="Çelik",
                    TCKNo="34567890123",
                    Salary=115000,
                    HireDate=new DateTime(2023,2,20),
                    BirthDate=new DateTime(1995,2,25),
                    AppUserId=4,
                     InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                },
                new AppUserProfile 
                {
                    Id=5,
                    FirstName="Ali",
                    LastName="Şahin",
                    TCKNo="45678901234",
                    Salary=110000,
                    HireDate=new DateTime(2023,5,15),
                    BirthDate=new DateTime(1993,7,10),
                    AppUserId=5,
                     InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,

                },
                new AppUserProfile 
                {
                    Id=6,
                    FirstName="Zeynep",
                    LastName="Yıldız",
                    TCKNo="56789012345",
                    Salary=90000,
                    HireDate=new DateTime(2023,8,1),
                    BirthDate=new DateTime(1996,3,5),
                    AppUserId=6,
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted,
                },
                 new AppUserProfile
                 {
                     Id=7,
                     FirstName="Can",
                     LastName="Polat",
                     TCKNo="67890123456",
                     Salary=85000,
                     HireDate=new DateTime(2023,9,10),
                     BirthDate=new DateTime(1994,12,15),
                     AppUserId=7,
                     InsertedDate =new DateTime(2026,01,14),
                     Status=Domain.Enums.DataStatus.Inserted,

                 },
                 new AppUserProfile
                 {
                     Id=8,
                     FirstName="Elif",
                     LastName="Aydın",
                     TCKNo="78901234567",
                     Salary=80000,
                     HireDate=new DateTime(2023,10,5),
                     BirthDate=new DateTime(1997,4,20),
                     AppUserId=8,
                      InsertedDate =new DateTime(2026,01,14),
                     Status=Domain.Enums.DataStatus.Inserted,
                 },
                 new AppUserProfile
                 {
                     Id=9,
                        FirstName="Mert",
                        LastName="Kılıç",
                        TCKNo="89012345678",
                        Salary=95000,
                        HireDate=new DateTime(2023,11,1),
                        BirthDate=new DateTime(1991,9,30),
                        AppUserId=9,
                        InsertedDate =new DateTime(2026,01,14),
                        Status=Domain.Enums.DataStatus.Inserted

                 },
                 new AppUserProfile
                 {
                     Id=10,
                        FirstName="Seda",
                        LastName="Güneş",
                        TCKNo="90123456789",
                        Salary=105000,
                        HireDate=new DateTime(2023,12,15),
                        BirthDate=new DateTime(1988,6,25),
                        AppUserId=10,
                         InsertedDate =new DateTime(2026,01,14),
                        Status=Domain.Enums.DataStatus.Inserted

                 },
                 new AppUserProfile
                 {
                     Id=11,
                        FirstName="Burak",
                        LastName="Arslan",
                        TCKNo="01234567890",
                        Salary=60000,
                        HireDate=new DateTime(2024,1,10),
                        BirthDate=new DateTime(2000,1,5),
                        AppUserId=11,
                         InsertedDate =new DateTime(2026,01,14),
                        Status=Domain.Enums.DataStatus.Inserted
                 }

            };
            modelBuilder.Entity<AppUserProfile>().HasData(userprofiles);
        }
        
    }
}
