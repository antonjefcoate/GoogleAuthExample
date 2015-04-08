using NUnit.Framework;

namespace GoogleAuthExample
{
    [TestFixture]
    public static class Runner
    {
        private const string ClientId = "7b4b801bf181b9ff85f70f4eee051decde032eb0";
        private const string PrivateKey = "";
        
        [Test]
        static void Run()
        {
            var googleShit = new GoogleAuthenticator(ClientId, PrivateKey);

            var token = googleShit.GetToken("MY_AUTH_CODE");
            var tokenDetails = googleShit.GetTokenDetails(token);
            var googlePlusDetails = googleShit.GetMyGooglePlusDetails(token, tokenDetails.UserId);


            Assert.NotNull(googlePlusDetails.DisplayName);
        }
    }
}