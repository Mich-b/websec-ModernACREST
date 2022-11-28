using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Build.Security.AspNetCore.Middleware.Attributes;


namespace ProductAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/product")]
    [Authorize("productapi")]
    public class ProductController : Controller
    {
        // GET api/values
        [Authorize("read")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Success! This is a product." };
        }


    }
}