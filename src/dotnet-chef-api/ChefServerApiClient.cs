namespace EBSCO.ChefServer
{
    using System;
    using System.Collections.Generic;
    using System.Management.Instrumentation;
    using System.Net;
    using Models;
    using RestSharp;

    public class ChefServerApiClient
    {
        private readonly string user;
        private readonly string pem;
        private readonly IRestClient client;
        private const string orgTemplate = "/organizations/{0}/{1}";

        public ChefServerApiClient(string clientName, string pem, IRestClient restClient)
        {
            this.user = clientName;
            this.pem = pem;
            this.client = restClient;
        }

        public ChefServerApiClient(string clientName, string pem, Uri baseUri):this(clientName, pem, new RestClient(baseUri))
        {
            
        }

        public ChefServerApiClient(string clientName, string pem, string baseUri) : this(clientName, pem, new Uri(baseUri))
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

        public IList<Role> FetchRoles(string organization)
        {
            var request = new AuthenticatedChefRequest(user, new Uri(string.Format(orgTemplate, organization, "roles"), UriKind.Relative)).Sign(pem);
            var response = client.Execute<Dictionary<string, string>>(request);

            var roles = new List<Role>();
            foreach (var role in response.Data)
            {
                roles.Add(fetchRole(new Uri(role.Value)));
            }

            return roles;
        }

        private Role fetchRole(Uri uri)
        {
            var response = client.Execute<Role>(new AuthenticatedChefRequest(user, uri).Sign(pem));

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    return response.Data;
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException();
                case HttpStatusCode.NotFound:
                    throw new InstanceNotFoundException();
                default:
                    throw new NotImplementedException(response.Content);
            }
        }
    }
}