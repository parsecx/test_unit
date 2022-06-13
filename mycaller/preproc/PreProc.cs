﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using mycaller.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime;

namespace mycaller.preproc
{
    //Class with static method for each endpoint 
    
    static class PreProc
    {
        public static HttpClient? Client { get; set; }
        public static async Task<IPO> GetConnection_IPO(string date_from, string date_to)
        {
            date_from = Date_Cherker(date_from);
            date_to = Date_Cherker(date_to);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/ipos?parameters%5Bdate_from%5D={date_from}&parameters%5Bdate_to%5D={date_to}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            { 
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    IPO? model;
                    if (JsonConvert.DeserializeObject<IPO>(responseBody) != null)
                    {
                        model = JsonConvert.DeserializeObject<IPO>(responseBody);
                    }
                    else
                    {
                        model = new IPO();
                    }
                    return model; 
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<MA> GetConnection_MA(string date_from, string date_to)
        {
            date_from = Date_Cherker(date_from);
            date_to = Date_Cherker(date_to);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/ma?parameters%5Bdate_from%5D={date_from}&parameters%5Bdate_to%5D={date_to}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    MA? model;
                    if (JsonConvert.DeserializeObject<MA>(responseBody) != null)
                    {
                        model = JsonConvert.DeserializeObject<MA>(responseBody);
                    }
                    else
                    {
                        model = new MA();
                    }
                    return model;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }

        }
        public static async Task<Earnings> GetConncetion_Earnings(string date_from, string date_to)
        {
            date_from = Date_Cherker(date_from);
            date_to = Date_Cherker(date_to);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/earnings?parameters%5Bdate_from%5D={date_from}&parameters%5Bdate_to%5D={date_to}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                    if (response.IsSuccessStatusCode)
                    {
                    Earnings? model;
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (JsonConvert.DeserializeObject<Earnings>(responseBody) != null)
                    {
                        model = JsonConvert.DeserializeObject<Earnings>(responseBody);
                    }
                    else
                    {
                        model = new Earnings();
                    }
                    return model;
                }
                    else
                        throw new Exception(response.ReasonPhrase);
                
            }
        }
        public static async Task<Dividents> GetConnection_Div(string date_from, string date_to)
        {
            date_from = Date_Cherker(date_from);
            date_to = Date_Cherker(date_to);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/dividends?parameters%5Bdate_from%5D={date_from}&parameters%5Bdate_to%5D={date_to}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Dividents? model;
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (JsonConvert.DeserializeObject<Dividents>(responseBody) != null)
                    {
                        model = JsonConvert.DeserializeObject<Dividents>(responseBody);
                    }
                    else
                    {
                        model = new Dividents();
                    }
                    return model;
                }
                else 
                    throw new Exception(response.ReasonPhrase);
            }
        }
        //Cheks whether date is correct or not
        private static string Date_Cherker(string date)
        {
            string[] s = date.Split('-');
            DateTime ch = new DateTime(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), Convert.ToInt32(s[2]));
            if (DateTime.Now > ch&&!(ch.Year < DateTime.Now.Year - 5))
                return date;
            else 
                return DateTime.Now.ToString("yyyy-mm-dd");
        }
    }
}
