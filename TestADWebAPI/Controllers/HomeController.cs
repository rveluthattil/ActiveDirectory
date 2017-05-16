using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using TestADWebAPI.Models;
using System.Web.Script.Serialization;
using System.IO;
using System.Xml;

namespace TestADWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ADDetails()
        {
            try
            {
                List<ADGroupModel> grpModel = new List<ADGroupModel>();
                /* string Url = @"http:/lvicisgappd01.ingramcontent.com:5556/api/Security/GetAllGroups/Administrator"; */
                string Url = "http://localhost:8091/api/ad";
                /* string response = null;
                using (var client = new WebClient())
                {
                    client.Headers.Add("content-type", "application/json");
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;

                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    response = client.DownloadString(Url);
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                grpModel = serializer.Deserialize<List<ADGroupModel>>(response);
                for (int gItem = 0; gItem < grpModel.Count; gItem++)
                    grpModel[gItem].Item = gItem + 1;
                return View(grpModel);*/

                WebRequest request = WebRequest.Create(Url);
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                WebResponse response = request.GetResponse();
                Stream datastream = response.GetResponseStream();
                StreamReader reader = new StreamReader(datastream);
                string responseFromServer = reader.ReadToEnd();
                return View(new ADGroupModel { Name = responseFromServer });

            }
            catch(Exception e)
            {
                return View("ErrorView",new ErrorInfo { StackTrace = e.Message + "     " + e.StackTrace + " " + e.InnerException});
            }
        }
    }
}