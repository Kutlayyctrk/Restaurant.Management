using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.SeedDatas
{
    public static class SupplierDataSeed
    {
        public static void SupplierSeed(this ModelBuilder modelBuilder)
        {
            List<Supplier> suppliers = new()
            {
                new Supplier
                {
                     Id = 1,
                    SupplierName = "Lezzet Tedarik A.Ş.",
                    ContactName = "Ali Yıldız",
                    PhoneNumber = "+90 (212) 555 0101",
                    Email = "info@lezzettedarik.com",
                    Address = "İkitelli OSB, İstanbul",
                 InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                }
                ,
                new Supplier
                {
                      Id = 2,
                    SupplierName = "Mutfak Malzemeleri Ltd.",
                    ContactName = "Ayşe Demir",
                    PhoneNumber = "+90 (232) 555 0202",
                    Email = "siparis@mutfakmalzemeleri.com",
                    Address = "Gaziemir, İzmir",
                  InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Supplier
                {
                     Id = 3,
                    SupplierName = "Doğal Ürünler Kooperatifi",
                    ContactName = "Mehmet Kaya",
                    PhoneNumber = "+90 (312) 555 0303",
                    Email = "iletisim@dogalurunler.org",
                    Address = "Çankaya, Ankara",
                 InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Supplier
                {
                     Id = 4,
                    SupplierName = "İçecek Merkezi",
                    ContactName = "Zeynep Çelik",
                    PhoneNumber = "+90 (216) 555 0404",
                    Email = "destek@icecekmerkezi.com",
                    Address = "Kartal, İstanbul",
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted

                },
                new Supplier
                {
                    Id = 5,
                    SupplierName = "Kafe & Bar Tedarik",
                    ContactName = "Cengiz Ak",
                    PhoneNumber = "+90 (212) 555 0707",
                    Email = "tedarik@kafebar.com",
                    Address = "Beşiktaş, İstanbul",
              InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                }
            };
            modelBuilder.Entity<Supplier>().HasData(suppliers);
        }

    }
}
