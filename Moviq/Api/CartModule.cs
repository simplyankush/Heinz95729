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

    public class CartModule : NancyModule
    {
        public CartModule(IProductDomain bookDomain, IModuleHelpers helper,IUserRepository userRepo) {
            
           

            this.Get["/api/cart/add", true] = async (args, cancellationToken) => {
                string productname = this.Request.Query.q; // +" AND _type: book";
                var currentUser = (IUser)this.Context.CurrentUser;
                
               if (currentUser != null)
                {
                    string username = currentUser.UserName;

                
                     
                    IProduct product = bookDomain.Repo.Get(productname);


                    if (!currentUser.Cart.Contains(product.Uid))
                    {
                        currentUser.Cart.Add(product.Uid);
                        userRepo.Set(currentUser);

                        return helper.ToJson(1);
                    }

                    return helper.ToJson(2);
                }

               return helper.ToJson(3);
                    
            };

           

            this.Get["/api/cart/delete/", true] = async (args, cancellationToken) =>
            {
                string productname = this.Request.Query.q;
                var currentUser = this.Context.CurrentUser;

                if (currentUser != null)
                {
                    string username = currentUser.UserName;

                    var user = userRepo.GetByUsername(username);

                    user.Cart.Remove(productname);
                    userRepo.Set(user);
                    return helper.ToJson(true);
                }
                return helper.ToJson(false);
                
            };


            this.Get["/api/cart/full"] = args =>
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

            this.Get["/api/cart/paid"] = args =>
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

                    currentUser.Cart = new ArrayList();
                    userRepo.Set(currentUser);

                    return helper.ToJson(fullcart);
                }

                return helper.ToJson(false);

            };  
  
        
        }
    }
}