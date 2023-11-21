using Microsoft.EntityFrameworkCore;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;

namespace Project.DAL.Extensions
{
    public static class ProductDataSeedExtension
    {
        public static void SeedProducts(ModelBuilder modelBuilder)
        {

            List<Product> products = new();

            //KategoriyeGoreUrunEkle(1);
            //KategoriyeGoreUrunEkle(2);

            for (int i = 1; i < 10; i++)
            {
                Product p = new()
                {
                    ID = i,
                    ProductName = new Commerce("tr").ProductName(),
                    UnitPrice = Convert.ToDecimal(new Commerce("tr").Price()),
                    UnitsInStock = 100,
                    CategoryID = i,
                    ImagePath = new Images().Nightlife()

                };

                products.Add(p);
            }

            modelBuilder.Entity<Product>().HasData(products);

        }



        ///// <summary>
        ///// Verdiginiz argüman Id'sindeki Kategoriye 10 tane ürün ekle
        ///// </summary>
        ///// <param name="sayi">Dikkat edin sadece 1-10'a kadar sayı verin</param>
        //private static void KategoriyeGoreUrunEkle(int sayi)
        //{
           
        //}
    }
}
