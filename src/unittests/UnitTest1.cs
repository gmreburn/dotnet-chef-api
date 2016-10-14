namespace EBSCO.ChefServer.Tests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ChefServerApiClientTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var user = "gmreburn";
            var baseUrl = new Uri("https://api.opscode.com");
            var client = new ChefServerApiClient(user, File.ReadAllText("test.pem"), baseUrl);

            // Act
            var resp = client.FetchRoles("chef_rendez");

            // Assert
            Assert.AreEqual("11.4.0", resp);
        }
    }
}