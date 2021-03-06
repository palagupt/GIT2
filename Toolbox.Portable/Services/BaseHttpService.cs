﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;

namespace Toolbox.Portable.Services
{
    /// <summary>
    /// Base service. Provides singlton pattern approach, and inherited functionality for making http requests.
    /// </summary>
    public abstract class BaseHttpService
    {
        protected HttpClient client;
        string baseApiUri;

        protected BaseHttpService(string baseApiUri)
        {
            client = new HttpClient();
            this.baseApiUri = baseApiUri;
        }

        protected Task<T> DeleteAsync<T>(string requestUri, Action<HttpRequestMessage> modifyRequestAction = null)
        {
            return SendWithRetryAsync<T>(HttpMethod.Delete, requestUri, modifyRequestAction);
        }

        protected Task<T> GetAsync<T>(string requestUri, Action<HttpRequestMessage> modifyRequestAction = null)
        {
            return SendWithRetryAsync<T>(HttpMethod.Get, requestUri, modifyRequestAction);
        }

        protected Task<T> PutAsync<T>(string requestUri, Action<HttpRequestMessage> modifyRequestAction = null)
        {
            return SendWithRetryAsync<T>(HttpMethod.Put, requestUri, modifyRequestAction);
        }

        protected Task<T> PutAsync<T, K>(string requestUri, K obj, Action<HttpRequestMessage> modifyRequestAction = null) // where object
        {
            var jsonRequest = !obj.Equals(default(K)) ? JsonConvert.SerializeObject(obj) : null;
            return SendWithRetryAsync<T>(HttpMethod.Put, requestUri, modifyRequestAction, jsonRequest);
        }

        protected Task<T> PostAsync<T>(string requestUri, Action<HttpRequestMessage> modifyRequestAction = null)
        {
            return SendWithRetryAsync<T>(HttpMethod.Post, requestUri, modifyRequestAction);
        }

        protected Task<T> PostAsync<T, K>(string requestUri, K obj, Action<HttpRequestMessage> modifyRequestAction = null) // where object
        {
            var jsonRequest = !obj.Equals(default(K)) ? JsonConvert.SerializeObject(obj) : null;
            return SendWithRetryAsync<T>(HttpMethod.Post, requestUri, modifyRequestAction, jsonRequest);
        }

        async Task<T> SendWithRetryAsync<T>(HttpMethod requestType, string requestUri, Action<HttpRequestMessage> modifyRequestAction, string jsonRequest = null)
        {
            T result = default(T);

            result = await Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(
                    5,
                    retryAttempt => TimeSpan.FromMilliseconds((200 * retryAttempt)),
                    (exception, timeSpan, context) =>
                    {
                        Debug.WriteLine(exception.Message);
                    }
                )
                .ExecuteAsync(async () => await SendAsync<T>(requestType, requestUri, modifyRequestAction, jsonRequest));

            return result;
        }

        async Task<T> SendAsync<T>(HttpMethod requestType, string requestUri, Action<HttpRequestMessage> modifyRequestAction, string jsonRequest = null)
        {
            T result = default(T);

            var request = new HttpRequestMessage(requestType, new Uri($"{baseApiUri}{requestUri}"));

            request.Headers.Add("ui-culture", System.Globalization.CultureInfo.CurrentCulture.Name);

            modifyRequestAction?.Invoke(request);

            if (jsonRequest != null)
                request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // TODO: Include the overloaded CancellationToken, if needed
            var response = await client.SendAsync(request).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string json = string.Empty;

            if (response != null)
                json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);



            if (result is string)
            {
                result = (T)Convert.ChangeType(json, typeof(T));
            }
            else if (!string.IsNullOrEmpty(json))
            {
                result = JsonConvert.DeserializeObject<T>(json);
            }

            return result;
        }
    }
}