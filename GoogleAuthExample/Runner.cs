using NUnit.Framework;

namespace GoogleAuthExample
{
    [TestFixture]
    public class Runner
    {
        private const string ClientId = "919350792124-vcfuvs4lgai007us30glukl9bti0d37b.apps.googleusercontent.com";
        private const string PrivateKey = "i2a6v6x_-GDen9_OYQkMqZqu";
        
        [Test]
        public void Run()
        {
            var googleShit = new GoogleAuthenticator(ClientId, PrivateKey);

            var token = googleShit.GetToken("MY_AUTH_CODE");
            var tokenDetails = googleShit.GetTokenDetails(token);
            var googlePlusDetails = googleShit.GetMyGooglePlusDetails(token, tokenDetails.UserId);


            Assert.NotNull(googlePlusDetails.DisplayName);
        }
    }
}