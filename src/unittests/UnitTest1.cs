namespace EBSCO.ChefServer.Tests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RestSharp;
    using Moq;

    [TestClass]
    public class ChefServerApiClientTests
    {
        [TestMethod, Ignore]
        public void TestMethod1()
        {
            // Arrange
            var user = "gmreburn";
            var baseUrl = new Uri("http://api.opscode.com");
            var client = new ChefServerApiClient(user, File.ReadAllText("gmreburn.pem"), baseUrl);

            // Act
            var resp = client.FetchRoles("chef_rendez");

            // Assert
            Assert.AreEqual("11.4.0", resp);
        }

        [TestMethod, Ignore]
        public void Test_Stubbed()
        {
            // Arrange
            var resp1 = "{\"test\":\"https://api.opscode.com/organizations/chef_rendez/roles/test\"}";
            var resp2 =
                "{\"name\":\"test\",\"description\":\"\",\"json_class\":\"Chef::Role\",\"default_attributes\":{},\"override_attributes\":{},\"chef_type\":\"role\",\"run_list\":[],\"env_run_lists\":{}}";
            var user = "gmreburn";
            var mockClient = new Mock<IRestClient>();
            var client = new ChefServerApiClient(user, File.ReadAllText("gmreburn.pem"), mockClient.Object);
            //mockClient.Setup(m => m.Execute(It.IsAny<IRestRequest>())).Returns(resp1);

            // Act
            var resp = client.FetchRoles("chef_rendez");

            // Assert
            Assert.AreEqual("11.4.0", resp);
        }
    }
}