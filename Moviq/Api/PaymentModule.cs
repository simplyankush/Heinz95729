
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

    public class PaymentModule : NancyModule
    {
        public PaymentModule(IProductDomain bookDomain, IModuleHelpers modulehelper, StripeHelper helper, IUserRepository userRepo)
        {

            //this.Get["/api/books"] = args => {
            //    var take = Request.Query.take != null ? Request.Query.take : 20;
            //    var skip = Request.Query.skip != null ? Request.Query.skip : 0;

            //    return helper.ToJson(bookDomain.Repo.List(take, skip));
            //};

            this.Get["/api/pay", true] = async (args, cancellationToken) =>
            {
                string token = this.Request.Query.token; 
                string amt = this.Request.Query.amt;

                double amount = Convert.ToDouble(amt);
                int cents = (int)(amount * 100);

                bool success = helper.Charge(token, cents);
                
                
                //if (currentUser != null)
                //{
                //    string username = currentUser.UserName;

                //    //    var user = userRepo.GetByUsername(username);

                //    IProduct product = bookDomain.Repo.Get(productname);


                //    if (!currentUser.Cart.Contains(product.Uid))
                //    {
                //        currentUser.Cart.Add(product.Uid);
                //        userRepo.Set(currentUser);

                //        return helper.ToJson(true);
                //    }
                //}

                return modulehelper.ToJson(success);
                //helper.ToJson(bookDomain.Repo.Get(args.uid));
            };


        }
    }
}