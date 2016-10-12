namespace EBSCO.ChefServer.Tests
{
    using System;
    using System.IO;
    using System.Linq;
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
            var request = new AuthenticatedChefRequest("test", new Uri("/", UriKind.Relative));

            // Act
            request.Sign(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "test.pem")));

            // Assert
            var header = string.Join("",
                request.Parameters.Where(p => p.Name.Contains("X-Ops-Authorization-"))
                    .Select(p => p.Value)
                    .Aggregate((i, j) => string.Format("{0}{1}", i, j)));
            Assert.AreEqual("Q4K0MxTyb5+JjCWmlKUUWAZwDNZYb2wDg5RBkBbD9ORmM5xleQwIoIUYwqqa756oiHu8Cz/VT7X0ToFJnCYSfohlrGwIgYYmNA1d6LxvzmBAXvnhaJHaU7XJ3fROCDe/YVhPGSJIj9bKdJLGvg2dpKCZTxn9m0z2ZT6ObjBOyufGsbyauNdjLmhOboVtLOTw966tOSiCnqNRLyR7LsUtQsZJ1IVCMZu8Prqj9yWAA264ul13rFhyvIkj53b4N6AmKrV9BolkQM4OYsVdTQbovXwcD1XWboNFXtEYDZwPUHtqLrVu4BDGpsypdZ0cdXbMkhmSfvrCGdHT8+fZwx4uSg==", header);
        }
    }
}