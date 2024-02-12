using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using TravelApp.Extensions;
using TravelApp.Models;
using System;

namespace TravelApp.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        private readonly string _apiKey = "esWWkptYkM7yNHgXtlsGBwN0AoBcACns";
        private readonly string _apiKeySecret = "ORpqcLYjHvTGetb9";

        public RestClient _restClient;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ApiTokenModel token = context.HttpContext.Session.GetObjectFromJson<ApiTokenModel>("token") ?? new();

            if (token.AccessToken is null || token.ExpirationTime >= DateTime.Now)
            {
                token = GenerateNewToken();
            }

            context.HttpContext.Session.SetObjectAsJson("token", token);

            _restClient = new RestClient();
            _restClient.AddDefaultHeader("Bearer", token.AccessToken);


            base.OnActionExecuting(context);
        }

        private ApiTokenModel GenerateNewToken()
        {
            var url = "https://test.api.amadeus.com/v1/security/oauth2/token";

            var client = new RestClient(url);

            var request = new RestRequest(url ,Method.Post);
            request.AddParameter("client_id", _apiKey);
            request.AddParameter("client_secret", _apiKeySecret);
            request.AddParameter("grant_type", "client_credentials");
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ApiTokenModel>(response.Content);
            }

            return new ApiTokenModel();
        }



    }
}
