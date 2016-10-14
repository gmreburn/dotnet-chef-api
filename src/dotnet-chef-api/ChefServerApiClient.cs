namespace EBSCO.ChefServer
{
    using System;
    using System.Net;
    using RestSharp;

    public class ChefServerApiClient
    {
        private readonly string user;
        private readonly string pem;
        private readonly RestClient client;
        private const string orgTemplate = "/organizations/{0}/{1}";

        public ChefServerApiClient(string client, string pem, Uri baseUri)
        {
            user = client;
            this.pem = pem;
            this.client = new RestClient(baseUri);
        }

        public ChefServerApiClient(string client, string pem, string baseUri) : this(client, pem, new Uri(baseUri))
        {

        }

        public void FetchUsers()
        {
            var request = new AuthenticatedChefRequest(user, new Uri("/users", UriKind.Relative));
            request.Sign(pem);
            var response = client.Execute(request);

            switch (response.StatusCode)
            {
                    case HttpStatusCode.OK:
                    break;
                    case HttpStatusCode.Unauthorized:
                    
                    break;
                    case HttpStatusCode.Forbidden:
                    break;
                    case HttpStatusCode.NotFound:
                    break;
            }
        }

        public IRestResponse FetchRoles(string organization)
        {
            var request = new AuthenticatedChefRequest(user, new Uri(string.Format(orgTemplate, organization, "roles"), UriKind.Relative));
            request.Sign(pem);
            return client.Execute(request);
        }
    }
}