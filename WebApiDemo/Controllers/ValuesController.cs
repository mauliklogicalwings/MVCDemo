using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;
using System.Runtime.Caching;

namespace WebApiDemo.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Get states 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/states")]
        public dynamic getstates()
        {
            states st = new states();
            List<string> a = new List<string>();
            a.Add("Gujarat");
            a.Add("Delhi");
            a.Add("Sikkim");

            st.name = a;

            return st;
        }

        /// <summary>
        /// Generate token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/login")]
        public dynamic login(string email, string pwd)
        {
            string emailID = email;
            string password = pwd;

            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                var user = db.Logins.Where(i => i.EmailID == emailID && i.Password == password).FirstOrDefault();

                if (user != null)
                {
                    string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    MemoryCache mc =  MemoryCache.Default;
                    mc.Add("token", token, DateTimeOffset.UtcNow.AddDays(1));
                    return token;
                }

                return "Please enter correct email and password";
            }
        }

        /// <summary>
        /// Get student data
        /// </summary>
        /// <param name="studentID">
        ///  studentID
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/get/student")]
        public dynamic getstudent(int studentID)
        {

            int stID = studentID;
            var re = Request;
            var header = re.Headers;
            if (header.Contains("Token"))
            {
                MemoryCache mc = MemoryCache.Default;
                var token = header.GetValues("Token").First();
                var cahcheToken = mc.Get("token");
                if (cahcheToken.Equals(token))
                {
                    using (SchoolManagementEntities db = new SchoolManagementEntities())
                    {
                        Student s = new Student();
                        s = db.Students.Where(i => i.StudentID == stID).FirstOrDefault();
                        return s;
                    }
                }
            } 
            return "Please pass header value";
          
        }
    }
}

public class states
{
    public List<string> name { get; set; }
}

public class studentParam
{
    public int studentID { get; set; }
}

