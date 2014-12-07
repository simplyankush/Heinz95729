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
            
            //this.Get["/api/books"] = args => {
            //    var take = Request.Query.take != null ? Request.Query.take : 20;
            //    var skip = Request.Query.skip != null ? Request.Query.skip : 0;

            //    return helper.ToJson(bookDomain.Repo.List(take, skip));
            //};

            this.Get["/api/cart/add", true] = async (args, cancellationToken) => {
                string productname = this.Request.Query.q; // +" AND _type: book";
                var currentUser = (IUser)this.Context.CurrentUser;
                
               if (currentUser != null)
                {
                    string username = currentUser.UserName;

                //    var user = userRepo.GetByUsername(username);
                     
                    IProduct product = bookDomain.Repo.Get(productname);


                    if (!currentUser.Cart.Contains(product.Uid))
                    {
                        currentUser.Cart.Add(product.Uid);
                        userRepo.Set(currentUser);
                        
                        return helper.ToJson(true);
                    }
                }

               return helper.ToJson(false);
                    //helper.ToJson(bookDomain.Repo.Get(args.uid));
            };

            //this.Get["/api/books/search", true] = async (args, cancellationToken) => {
            //    var searchTerm = this.Request.Query.q; // +" AND _type: book";
            //    var result = await bookDomain.Repo.Find(searchTerm);
            //    return helper.ToJson(result);
            //};

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

                    //IUser user = userRepo.GetByUsername(username);
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

                     //IUser user = userRepo.GetByUsername(username);
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