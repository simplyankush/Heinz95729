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
    using System.Net;
    using System.Threading.Tasks;
    using System.Text;
    using System.IO;
    using System.Configuration;

    public class StripeHelper
    {

        public bool Charge(string token, int cents)
        {
            string secretkey = ConfigurationManager.AppSettings["stripesecret"];
            StripePayment payment = new StripePayment(secretkey);
            //string token = "tok_156a6sB1dqkk1NBjfqEKHAWy";
            bool charge = payment.Charge(cents, "usd", token, "Test charge");
            Console.WriteLine(charge);
            //string charge_id = charge.ID;
            return charge;
        }
    }
}