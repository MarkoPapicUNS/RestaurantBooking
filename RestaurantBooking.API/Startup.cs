using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace RestaurantBooking.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = System.Configuration.ConfigurationManager.AppSettings["TokenIssuer"]; //"https://localhost:44300";
            var audience = new List<string> {"ab9da96ebf4c411eacdbc22b552724ed"}; //TODO: move this to database or something like that
            var secret = TextEncodings.Base64Url.Decode(System.Configuration.ConfigurationManager.AppSettings["AppSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = audience,
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    },
                    Provider = new OAuthBearerAuthenticationProvider()
                });
        }
    }
}