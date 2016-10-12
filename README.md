# .NET Chef API [![Build status](https://ci.appveyor.com/api/projects/status/hfaxg4up1mytd3wq?svg=true)](https://ci.appveyor.com/project/gmreburn/dotnet-chef-api)
This is a simple C# class library used to interact with a Chef Server's REST API. You can use this with a open source and Enterprise versions of the Chef Server.

## Usage
As of yet, this is not a full wrapper for the Chef Server API. You will still need to refer to the API documentation at <http://docs.opscode.com/api_chef_server.html> to determine which methods to call.

Using the class library is relatively straightforward. First, you create an AuthenticatedChefRequest and sign it with your private key. Then, pass that off to an instance of the ChefServer class and send the request. 

Some example code is:

	System.Uri baseUri = new System.Uri("https://api.opscode.com");
	System.Uri requestUri = new System.Uri(baseUri, "/organizations/organization_name/roles");
	EBSCO.ChefApi.AuthenticatedChefRequest authenticatedRequest = new EBSCO.ChefApi.AuthenticatedChefRequest("client_name", requestUri);

	authenticatedRequest.Sign(System.IO.File.ReadAllText("privatekey.pem"));

	RestSharp.IRestClient client = new RestSharp.RestClient(baseUri);
	RestSharp.IRestResponse response = client.Execute(authenticatedRequest);
	System.Console.WriteLine(response.Content);

