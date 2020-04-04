using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueryMaskSample.Models;
using QueryMaskSample.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QueryMaskSample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaskController : ControllerBase
    {
        private readonly MaskContext _maskContext;
        public MaskController(MaskContext maskContext)
        {
            _maskContext = maskContext;
        }

        public IActionResult Get()
        {
            try
            {
                return Ok(_maskContext.MedicalMasks.AsNoTracking());
            }
            catch (HttpRequestException ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}