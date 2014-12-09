namespace Moviq.Api
{
    using Moviq.Helpers;
    using Moviq.Interfaces.Services;
    using Nancy;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Moviq.Domain.Auth;
    using Moviq.Interfaces;
    using Moviq.Interfaces.Models;
    using Nancy;
    using Nancy.Security;
    using Newtonsoft.Json;
    using System;
    using System.Collections;

    public class DownloadModule : NancyModule
    {
        public DownloadModule(IProductDomain bookDomain, IModuleHelpers helper, IUserRepository userRepo)  // removed IProductDomain downloadDomain, 
        {

            this.Get["/api/cart/deliver"] = args =>
            {
                var currentUser = (IUser)this.Context.CurrentUser;
                ArrayList fullcart = new ArrayList();
                if (currentUser != null)
                {
                    string username = currentUser.UserName;

                   
                    for (int i = 0; i < currentUser.Cart.Count; i++)
                    {
                        string productname = currentUser.Cart[i].ToString();
                        IProduct product = bookDomain.Repo.Get(productname);
                        fullcart.Add(product);
                    }
                    return helper.ToJson(fullcart);
                }
                return helper.ToJson(false);

            };
        }
    }
}