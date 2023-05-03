using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KryptoCalc.Server.Data;
using KryptoCalc.Shared;

namespace KryptoCalc.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly KryptoCalcServerContext _context;

        public CoinsController(KryptoCalcServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Coin> Get()
        {
            return _context.Coin;
        }
    }
}
