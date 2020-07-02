using BotAppData.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace BotAppData.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int[] AgeId { get; set; }
        public Age[] Age { get; set; }
        public int ProductTypeId { get; set; }//это вроде бы должно находитися в классе ProductType
        public ProductType ProductType { get; set; }
        public int FreeTimes { get; set; }

        public Product()
        {
            ProductId = new Guid();
        }
    }
}