using Google.Apis.Auth.OAuth2.Responses;
using NUnit.Framework;

namespace GoogleAuthExample
{
    [TestFixture]
    public class Runner
    {
        // Original 'installed'
        //private const string ClientId = "919350792124-vcfuvs4lgai007us30glukl9bti0d37b.apps.googleusercontent.com";
        //private const string PrivateKey = "i2a6v6x_-GDen9_OYQkMqZqu";
        
        // Web
        private const string ClientId = "919350792124-8a9n7l35fhel47liln8mh27o9t74cf68.apps.googleusercontent.com";
        private const string PrivateKey = "tGo2pExc1wuwZ8ugSBH26cm9";
        
        [Test]
        public void RunAgainst_AuthCode()
        {
            var googleShit = new GoogleAuthenticator(ClientId, PrivateKey);

            var token = googleShit.GetToken("4/xno5e_eVTBWk1ztjVL7wwPF1tPLC9uWFM1TAFx6hhcc.wmzllUr1WWIRoiIBeO6P2m9GpH0zmQI");
            var tokenDetails = googleShit.GetTokenDetails(token);
            var googlePlusDetails = googleShit.GetMyGooglePlusDetails(token, tokenDetails.UserId);


            Assert.NotNull(googlePlusDetails.DisplayName);
        }

        [Test]
        public void RunAgainst_Tokens()
        {
            var googleShit = new GoogleAuthenticator(ClientId, PrivateKey);

            var tokenFromNative = new TokenResponse
            {
                AccessToken = "ya29.TwHmJ0I90jEU8P47MxXWA0UiUtjQ5UBr-YsQyNheOZg_bzDk82rCQ_jKK_dxwvckxnM298eJt4jSxw",
                RefreshToken = "1/KWHJUHsJ4rPKq9IjLc63Tb492hPtJKhPMcJ9wO1jWxU"
            };
            
            var tokenDetails = googleShit.GetTokenDetails(tokenFromNative);
            var googlePlusDetails = googleShit.GetMyGooglePlusDetails(tokenFromNative, tokenDetails.UserId);


            Assert.NotNull(googlePlusDetails.DisplayName);
        }
    }
}