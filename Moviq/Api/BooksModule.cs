namespace Moviq.Api
{
    using Moviq.Helpers;
    using Moviq.Interfaces.Services;
    using Nancy;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class BooksModule : NancyModule
    {
        public BooksModule(IProductDomain bookDomain, IModuleHelpers helper) {
            
           this.Get["/api/books/{uid}"] = args => {
                return helper.ToJson(bookDomain.Repo.Get(args.uid));
            };

            this.Get["/api/books/search", true] = async (args, cancellationToken) => {
                var searchTerm = this.Request.Query.q; // +" AND _type: book";
                var result = await bookDomain.Repo.Find(searchTerm);
                return helper.ToJson(result);
            };
        }
    }
}