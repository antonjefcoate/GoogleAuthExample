using System;
using System.Linq;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;

namespace GoogleAuthExample
{
    public class GoogleAuthenticator
    {
        private ClientSecrets Secrets { get; set; }

        public GoogleAuthenticator(string clientId, string clientSecret)
        {
            Secrets = new ClientSecrets()
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };
        }

        static public string[] Scopes = { PlusService.Scope.PlusLogin };

        public TokenResponse GetToken(string authCode)
        {
            var flowManager = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                        {
                            ClientSecrets = Secrets,
                            Scopes = Scopes
                        });

            var token = flowManager.ExchangeCodeForTokenAsync("", authCode, "postmessage", CancellationToken.None).Result;

            Console.WriteLine("Access Token: {0}", token.AccessToken);
            Console.WriteLine("Refresh Token: {0}", token.RefreshToken);

            return token;
        }

        public Tokeninfo GetTokenDetails(TokenResponse token)
        {
            var oauthService = new Oauth2Service(new Google.Apis.Services.BaseClientService.Initializer());

            var tokenInfoRequest = oauthService.Tokeninfo();
            tokenInfoRequest.AccessToken = token.AccessToken;

            var tokenInfo = tokenInfoRequest.Execute();

            Console.WriteLine("Email: {0}", tokenInfo.Email);
            Console.WriteLine("Refresh Token: {0}", tokenInfo.UserId);

            return tokenInfo;
        }

        public Person GetMyGooglePlusDetails(TokenResponse token, string userId)
        {
            var flowManager = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = Secrets,
                Scopes = Scopes
            });

            var credential = new UserCredential(flowManager, "me", token);
            var success = credential.RefreshTokenAsync(CancellationToken.None).Result;

            if(!success)
                throw new Exception("Not Authorized");

            token = credential.Token;
            var plusService  = new PlusService(
                new Google.Apis.Services.BaseClientService.Initializer()
                {
                    ApplicationName = ".NET Quickstart",
                    HttpClientInitializer = credential
                });

            var people = plusService.People.List("me", PeopleResource.ListRequest.CollectionEnum.Visible).Execute();

            Console.WriteLine("My Google Plus Name: {0}", people.Items.First().DisplayName);

            return people.Items.First();
        }

    }
}
