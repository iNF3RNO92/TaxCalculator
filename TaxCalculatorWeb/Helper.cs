using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TaxCalculatorWeb.Web.Helper
{

    public class TaxTypeAPI
    {
        private string _apiBaseURI = "https://localhost:44393";
        public HttpClient InitializeClient()
        {
            var client = new HttpClient();
            //Passing service base url    
            client.BaseAddress = new Uri(_apiBaseURI);

            client.DefaultRequestHeaders.Clear();
            //Define request data format    
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }

    public class ReturnObjectDTO
    {
        public decimal PayableTax { get; set; }
        public string ErrorResult { get; set; }
    }

    public class Helper
    {        
        public async Task<ReturnObjectDTO> GetPayableTax(string postalCode, decimal AnualSalary)
        {
            TaxTypeAPI taxTypeAPI = new TaxTypeAPI();
            ReturnObjectDTO returnObject = new ReturnObjectDTO();
            HttpClient client = taxTypeAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync($"api/tax/{postalCode}/{AnualSalary}");



            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                returnObject = JsonConvert.DeserializeObject<ReturnObjectDTO>(result);
            }
            else
            {
                returnObject.ErrorResult = "An internal error occured, please contact support";
            }

            return returnObject;
        }
    }
}
