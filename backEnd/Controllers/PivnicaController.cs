using System;
using System.Linq;
using System.Threading.Tasks;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace backEnd.Models
{
    [ApiController]
    [Route("[controller]")]
    
    public class PivnicaController : ControllerBase
    {
        public PivnicaContext Context { get; set; }
        public PivnicaController(PivnicaContext context)
        {
            Context=context;
        }
        [Route("Preuzmi pivnice")]
        [HttpGet]
        public async Task<ActionResult> Vrati()
        {
            try
            {
            return Ok( await Context.Pivnice.Include(p=>p.Stolovi)
            .Include(p=>p.Meni).Select(p=>p).ToListAsync());
            }
            catch(Exception e )
            {
                return BadRequest(e.Message);
            }

        }
    }
}