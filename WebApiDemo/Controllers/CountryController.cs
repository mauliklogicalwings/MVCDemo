using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    public class CountryController : ApiController
    {
        [HttpGet]
        [Route("api/country")]
        public dynamic getCountries()
        {
            states st = new states();
            List<string> a = new List<string>();
            a.Add("India");
            a.Add("America");
            a.Add("China");

            st.name = a;

            return st;
        }
    }
}