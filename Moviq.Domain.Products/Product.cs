using Moviq.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviq.Domain.Products
{
    public class Product : IProduct, IHelpCategorizeNoSqlData
    {
        public Product() 
        {
            this._type = "product";
        }

        public string Uid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public object Metadata { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailLink { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string _type { get; set; }


        //public override bool Equals(object obj2)
        //{
        //    Product obj = obj2 as Product;
        //    if (!Uid.Equals(obj.Uid))
        //        return false;
        //    if (!Title.Equals(obj.Title) )
        //        return false;
        //    if (!Description.Equals(obj.Description))
        //        return false;
        //    if (!ThumbnailLink.Equals(obj.ThumbnailLink))
        //        return false;
        //    if (!_type.Equals(obj._type))
        //        return false;
        //    if (!(Price == obj.Price))
        //        return false;

        //    return true;

        
        //}

    }
}
