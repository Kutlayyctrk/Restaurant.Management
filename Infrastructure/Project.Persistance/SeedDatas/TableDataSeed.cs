using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.SeedDatas
{
    public static class TableDataSeed
    {
        public static void TableSeed(this ModelBuilder modelBuilder)
        {
            List<Table> tables = new()
            {
                new Table
                {
                    Id=1,
                    TableNumber="Masa 1",
                    TableStatus=Domain.Enums.TableStatus.Free,
                 InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Table
                                {
                    Id=2,
                    TableNumber="Masa 2",
                    TableStatus=Domain.Enums.TableStatus.Free,
                InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Table
                {
                    Id=3,
                    TableNumber="Masa 3",
                    TableStatus=Domain.Enums.TableStatus.Free,
                   InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted

                },
                new Table
                {
                    Id=4,
                    TableNumber="Masa 4",
                    TableStatus=Domain.Enums.TableStatus.Free,
                  InsertedDate =new DateTime(2026,01,14),
                  WaiterId=8,
                    Status=Domain.Enums.DataStatus.Inserted

                },
                new Table
                {
                    Id=5,
                    TableNumber="Masa 5",
                    TableStatus=Domain.Enums.TableStatus.Free,
                    InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Table
                {
                    Id=6,
                    TableNumber="Masa 6",
                    TableStatus=Domain.Enums.TableStatus.Free,
               InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                },
                new Table
                {
                    Id=7,
                    TableNumber="Masa 7",
                    TableStatus=Domain.Enums.TableStatus.Free,
               InsertedDate =new DateTime(2026,01,14),
                    Status=Domain.Enums.DataStatus.Inserted
                }
            };
            modelBuilder.Entity<Table>().HasData(tables);
        }
    }
}
