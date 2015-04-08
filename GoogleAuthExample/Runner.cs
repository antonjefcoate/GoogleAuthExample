using NUnit.Framework;

namespace GoogleAuthExample
{
    [TestFixture]
    public static class Runner
    {
        [Test]
        static void Run()
        {
            var googleShit = new GoogleAuthenticator("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");

            var token = googleShit.GetToken("MY_AUTH_CODE");
            var tokenDetails = googleShit.GetTokenDetails(token);
            var googlePlusDetails = googleShit.GetMyGooglePlusDetails(token, tokenDetails.UserId);


            Assert.NotNull(googlePlusDetails.DisplayName);
        }
    }
}