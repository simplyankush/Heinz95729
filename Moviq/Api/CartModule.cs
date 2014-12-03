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

    public class CartModule : NancyModule
    {
        public CartModule(IProductDomain bookDomain, IModuleHelpers helper,IUserRepository userRepo) {
            
            //this.Get["/api/books"] = args => {
            //    var take = Request.Query.take != null ? Request.Query.take : 20;
            //    var skip = Request.Query.skip != null ? Request.Query.skip : 0;

            //    return helper.ToJson(bookDomain.Repo.List(take, skip));
            //};

            this.Get["/api/cart"] = args => {
               var currentUser = this.Context.CurrentUser;



               if (currentUser != null)
                {
                    string username = currentUser.UserName;

                    var user = userRepo.GetByUsername(username);
                   
                

                }
                
                return helper.ToJson(bookDomain.Repo.Get(args.uid));
            };

            //this.Get["/api/books/search", true] = async (args, cancellationToken) => {
            //    var searchTerm = this.Request.Query.q; // +" AND _type: book";
            //    var result = await bookDomain.Repo.Find(searchTerm);
            //    return helper.ToJson(result);
            //};
        }
    }
}