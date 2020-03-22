using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VehicleApp.DAL;

namespace VehicleApp.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public void Get(int id)
        {
            using (var ctx = new VehicleContext())
            {
                //var x = new VehicleMake()
                //{
                //    Name = "Opel",
                //    Abrv = "Corsa D"
                //};

                //ctx.VehicleMake.Add(x);
                //ctx.SaveChanges();

                Debug.Print(ctx.VehicleMake.First().Name);

            }
        }
        //public string Get(int id)
        //{
        //    return "value";
        //}


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
    }
}
