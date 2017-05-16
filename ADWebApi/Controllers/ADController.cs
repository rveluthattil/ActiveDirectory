using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.DirectoryServices.AccountManagement;
using ADWebApi.Models;
using System.Web.Script.Serialization;

namespace ADWebApi.Controllers
{
    public class ADController : ApiController
    {
        public string Get()
        {
            PrincipalContext test = new PrincipalContext(ContextType.Domain,"ICG");
            GroupPrincipal qGroup = new GroupPrincipal(test);
            PrincipalSearcher pSearcher = new PrincipalSearcher(qGroup);
            List<ADInfo> adData = new List<ADInfo>();
            foreach (var found in pSearcher.FindAll())
                adData.Add(new ADInfo { Sid = found.Sid.ToString(), SamName = found.SamAccountName });
            //return adData;
            return new JavaScriptSerializer().Serialize(adData);
        }
    }
}
