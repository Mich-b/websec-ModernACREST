using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Build.Security.AspNetCore.Middleware.Attributes;


namespace ShopAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/shop")]
    [Authorize("shopapi")]
    public class ShopController : Controller
    {
        // GET api/shop
        [Authorize("read")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Success! This is a shop." };
        }

    }
}