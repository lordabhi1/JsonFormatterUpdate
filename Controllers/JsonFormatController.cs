using JsonFormatterUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Security;

namespace JsonFormatterUI.Controllers
{
    public class JsonFormatController : Controller
    {
        HttpClientHandler clientHandler=new HttpClientHandler();

        JsonData json =new JsonData();
        public JsonFormatController() 
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors)
                =>
            { return true; };
        }


        public ActionResult DisplayForm()
        {
            return View("Display");
        }

        [HttpGet]
        public async Task<string?> FormatJson(string jsondata)
        {
            string data = jsondata;
            string result="";
           
            using (var client=new HttpClient(clientHandler))
            {
                using (var response = await client.GetAsync("https://localhost:7120/JsonFormatter/Formatjson?data=" + data))
                {
                    string apiResponse= await response.Content.ReadAsStringAsync();
                    result =(apiResponse);
                }

            }
            return result;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
