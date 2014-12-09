
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

           

            this.Get["/api/pay", true] = async (args, cancellationToken) =>
            {
                string token = this.Request.Query.token; 
                string amt = this.Request.Query.amt;

                double amount = Convert.ToDouble(amt);
                int cents = (int)(amount * 100);

                bool success = helper.Charge(token, cents);
                
                
                

                return modulehelper.ToJson(success);
                
            };


        }
    }
}