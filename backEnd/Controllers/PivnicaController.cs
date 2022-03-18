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
        [Route("PreuzmiPivnice")]
        [HttpGet]
        public async Task<ActionResult> Vrati()
        {
            try
            {
            return Ok( await Context.Pivnice.Include(p=>p.Stolovi)
            .Include(p=>p.Meni).ThenInclude(p=>p.Stavke).Select(p=>p).ToListAsync());
            }
            catch(Exception e )
            {
                return BadRequest(e.Message);
            }

        }
        [Route("ObrisiPivnicu/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Obrisi(int id)
        {
            try
            {
                var pivnica=await Context.Pivnice.FindAsync(id);
                if(pivnica != null)
                {
                    Context.Pivnice.Remove(pivnica);
                    await Context.SaveChangesAsync();
                    return Ok("Pivnica uspesno izbrisana");
                }
                else
                {
                    return BadRequest("Pivnica nije pronadjena");
                }
            }
            catch(Exception e )
            {
                return BadRequest(e.Message);
            }

        }


    }
    
    
}