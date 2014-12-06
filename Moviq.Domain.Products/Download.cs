using Moviq.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviq.Domain.Products
{
    public class Download : IDownload, IHelpCategorizeNoSqlData
    {
        public Download() 
        {
            this._type = "download";
        }

        public string Uid { get; set; }
        public string DLLink { get; set; }
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
