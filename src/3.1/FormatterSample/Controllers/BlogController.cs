using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormatterSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormatterSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/xml")]
    [FormatFilter]
    public class BlogController : ControllerBase
    {
        [HttpGet("{format?}")]
        public IActionResult Get()
        {
            var Posts = new List<Post>
            {
                new Post
                {
                    Title = "精準解析 ASP.NET Core Web API",
                    MetaDescription = "精準解析 ASP.NET Core Web API",
                }, new Post
                {
                    Title = "深入淺出 LINQ",
                    MetaDescription = "深入淺出 LINQ"
                }, new Post
                {
                    Title = "輕鬆學會物件導向",
                    MetaDescription = "輕鬆學會物件導向"
                }
            };


            var blogs = new List<Blog>
            {
                new Blog()
                {
                    Name = "KingKong Bruce記事",
                    Description = "坐，請坐，請上座。 茶，上茶，請上茶。",
                    Posts = Posts
                }
            };

            return Ok(blogs);
        }
    }
}