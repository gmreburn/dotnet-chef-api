namespace EBSCO.ChefServer.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using gmreburn;
    using NUnit.Framework;

    [TestFixture]
    public class AuthenticatedChefRequestUnitTests
    {
        [Test]
        public void ConstructorSetsChefVersionHeader()
        {
            // Arrange
            var client = "gmreburn";
            var resource = new Uri("/", UriKind.Relative);

            // Act
            var request = new AuthenticatedChefRequest(client, resource);

            // Assert
            Assert.AreEqual("11.4.0", request.Parameters.Single(p => p.Name.Equals("X-Chef-Version")).Value);
        }

        [Test]
        public void ConstructorSetsOpsUserIdHeader()
        {
            // Arrange
            var client = "gmreburn";
            var resource = new Uri("/", UriKind.Relative);

            // Act
            var request = new AuthenticatedChefRequest(client, resource);

            // Assert
            Assert.AreEqual(client, request.Parameters.Single(p => p.Name.Equals("X-Ops-UserId")).Value);
        }

        [Test]
        public void SignSetsOpsSignHeader()
        {
            // Arrange
            var request = new AuthenticatedChefRequest("test", new Uri("/", UriKind.Relative));

            // Act
            request.Sign(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "test.pem")));

            // Assert
            Assert.AreEqual("algorithm=sha1;version=1.0",
                request.Parameters.Single(p => p.Name.Equals("X-Ops-Sign")).Value);
        }

        [Test]
        public void SignSetsOpsTimestampHeader()
        {
            // Arrange
            var request = new AuthenticatedChefRequest("test", new Uri("/", UriKind.Relative));

            // Act
            request.Sign(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "test.pem")));

            // Assert
            Assert.IsNotNull(request.Parameters.Single(p => p.Name.Equals("X-Ops-Timestamp")).Value);
        }

        [Test]
        public void SignSetsOpsContentHashHeader()
        {
            // Arrange
            var request = new AuthenticatedChefRequest("test", new Uri("/", UriKind.Relative));

            // Act
            request.Sign(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "test.pem")));

            // Assert
            Assert.IsNotNull(request.Parameters.Single(p => p.Name.Equals("X-Ops-Content-Hash")).Value);
        }

        [Test]
        public void SignProducesValidSignature()
        {
            // Arrange
            SystemTime.UtcNow = default(DateTime);
            var request = new AuthenticatedChefRequest("test", new Uri("/", UriKind.Relative));

            // Act
            request.Sign(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "test.pem")));

            // Assert
            Assert.AreEqual("o5/7MhzNGzhenMDwVrKVHFYHTyOVnJy1ww+NsUzxxElbeNKxA7Gz277VIGre", request.Parameters.Single(p => p.Name.Equals("X-Ops-Authorization-1")).Value);
            Assert.AreEqual("eQLc+3sBPAMJrfIbdV5qCgL47Hm5qzUwn7vL9Z+I0NG5f4X4vmgync3jFxCB", request.Parameters.Single(p => p.Name.Equals("X-Ops-Authorization-2")).Value);
            Assert.AreEqual("821RyF4duDUhSgstpshhjZa0heNn+7pYhRjg0LBfpPxkjoV555o250GpwD/r", request.Parameters.Single(p => p.Name.Equals("X-Ops-Authorization-3")).Value);
            Assert.AreEqual("1ZAMh3uI/ihMNVO0pyM6IwVtmEgjLKHdLvph8R9PB+LBkOH9HZCv+wVZOz9h", request.Parameters.Single(p => p.Name.Equals("X-Ops-Authorization-4")).Value);
            Assert.AreEqual("y8DlrRAXdMQ44iXEsrqsAwDGbZDk4u9BRyS+PDqgGTInTwi/Dn4CxJpJ79oG", request.Parameters.Single(p => p.Name.Equals("X-Ops-Authorization-5")).Value);
            Assert.AreEqual("M1tGWo7KhieFpDRrbZVX9ft0ziiNsBEFY9PFNYmV+Q==", request.Parameters.Single(p => p.Name.Equals("X-Ops-Authorization-6")).Value);
        }
    }
}