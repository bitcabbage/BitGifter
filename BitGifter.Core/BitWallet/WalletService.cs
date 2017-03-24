﻿using BitGifter.Core.BitWallet.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitGifter.Core.BitWallet
{
    public class WalletService
    {
        public WalletResponse CreateWallet(WalletRequest request)
        {
            var response = new HttpClientWrapper()
             .SetAuthHeader("IiHbKeXKLE2vfmQvTHO2Q5Yw63Lj2Qn65eQeYXXc")
             .SetBaseAddress("https://quxze3zsll.execute-api.us-west-2.amazonaws.com/latest")
             .SetResource("/wallets")
             .AddParameter(request)
             .PostAsJsonAsync().Result;

            //if (!response.IsSuccessStatusCode)
            //{
            //    return response.StatusCode.ToString();
            //}
            var responseJson = response.Content.ReadAsStringAsync().Result.ToString();


            var res = JsonConvert.DeserializeObject<WalletResponse>(responseJson);

            return res;
        }

        public PaymentResponse MakePayment(PaymentRequest request)
        {
            var response = new HttpClientWrapper()
          .SetAuthHeader("IiHbKeXKLE2vfmQvTHO2Q5Yw63Lj2Qn65eQeYXXc")
          .SetBaseAddress("https://quxze3zsll.execute-api.us-west-2.amazonaws.com/latest")
          .SetResource("/payments")
          .AddParameter(request)
          .PostAsJsonAsync().Result;

            //if (!response.IsSuccessStatusCode)
            //{
            //    return response.StatusCode.ToString();
            //}
            var responseJson = response.Content.ReadAsStringAsync().Result.ToString();


            var res = JsonConvert.DeserializeObject<PaymentResponse>(responseJson);

            return res;
        }
    }


    //x-api-key – IiHbKeXKLE2vfmQvTHO2Q5Yw63Lj2Qn65eQeYXXc
    public class HttpClientWrapper
    {
        public HttpClient _client { get; set; } = new HttpClient();
        public string _resource { get; set; } = string.Empty;
        public object _param { get; set; }

        public HttpClientWrapper SetBaseAddress(string baseAddress)
        {
            _client.BaseAddress = new Uri(baseAddress);
            return this;
        }

        public HttpClientWrapper SetResource(string resource)
        {
            _resource = resource;
            return this;
        }
        public HttpClientWrapper SetAuthHeader(string token)
        {
            _client.DefaultRequestHeaders.Add("x-api-key", token);
            return this;
        }

        public HttpClientWrapper AddParameter<T>(T parameter) where T : class
        {
            _param = parameter;
            return this;
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync()
        {
            var jsonString = JsonConvert.SerializeObject(_param);
            return await _client.PostAsync(_client.BaseAddress + _resource, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
    }
}
