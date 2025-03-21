﻿using System.Collections.Specialized;
using System.Text.Json;

internal class ServerCommunication
{        

    private HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("http://ec2-44-211-225-127.compute-1.amazonaws.com"),
    };

    public void SendRequestASync(UriParam[] uriParams, Action<ServerResponse> onComplete, Action onError)
    {
        NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

        for (int i = 0; i < uriParams.Length; i++)
        {
            UriParam param = uriParams[i];
            queryString.Add(param.key, param.value);
        }

        Task.Run(() => GetAsync(sharedClient, queryString.ToString(), onComplete, onError));
    }

    public async Task SendRequestSync(UriParam[] uriParams, Action<ServerResponse> onComplete, Action onError)
    {
        NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

        for (int i = 0; i < uriParams.Length; i++)
        {
            UriParam param = uriParams[i];
            queryString.Add(param.key, param.value);
        }

        await GetAsync(sharedClient, queryString.ToString(), onComplete, onError);
    }

    async Task GetAsync(HttpClient httpClient, string uriParams, Action<ServerResponse> onComplete, Action onError)
    {
        HttpResponseMessage response = await httpClient.GetAsync(string.Format("api.php?{0}", uriParams));
        var jsonResponse = await response.Content.ReadAsStringAsync();
        //Console.WriteLine($"{jsonResponse}\n");

        try
        {
            ServerResponse? serverResponse = JsonSerializer.Deserialize<ServerResponse>(jsonResponse);
            onComplete(serverResponse);
        }
        catch
        {
            onError();
        }            
    }
}
