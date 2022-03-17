
using System;
using System.Linq;
using System.Threading.Tasks;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace backEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoController : ControllerBase
    {
        public PivnicaContext Context { get; set ;}
       public StoController( PivnicaContext context)
       {
           Context=context;
       }

       [Route("PreuzmiStolove")]
       [HttpGet]
       public async Task<ActionResult> Stolovi()
       {
           try
           {
               return Ok(await Context.Stolovi.Select(p=>
               new
               {
                   ID=p.ID,
                   BrojStola=p.BrojStola
               }).ToListAsync());
           }
           catch(Exception e)
           {
               return BadRequest(e.Message);
           }       } 
       [Route("PreuzmiSto/{id}")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiSto(int id)
       {
           if(id < 0)
           {
               return BadRequest("Pogresan ID.");
           }
           try
           {
               var sto=await Context.Stolovi.FindAsync(id);
               if(sto==null)
               {
                   return BadRequest($"Sto sa ID-jem: {id}, ne postoji u bazi. ");
               }
               else
               {
                   return Ok(sto);
               }
               
           }
           catch(Exception e)
           {
               return BadRequest(e.Message);
           }
       }
       [Route("PreuzmiStoUzPomocBroja/{BrojStola}")]
       [HttpGet]
       public async Task<ActionResult> VratiSto(int BrojStola)
       {
           try
           {
               var sto=await Context.Stolovi.Where(p=> p.BrojStola==BrojStola).FirstOrDefaultAsync();
               //mogu da iskoristim .Select() ako ne zelim Narudzbine
               if(sto==null)
               {
                   return BadRequest($"Sto sa brojem: {BrojStola}, ne postoji u bazi! ");
               }
               else
               {
                   return Ok(sto);
               }
               
           }
           catch(Exception e)
           {
               return BadRequest(e.Message);
           }
       }

       [Route("DodajSto/{pivnicaID}")]
       [HttpPost]
       public async Task<ActionResult> DodajSto([FromBody] Sto sto, int pivnicaID)
       {
           if(pivnicaID <=0)
           {
               return BadRequest("Nedozvoljena vrednost ID-ja!");
           }
           if( sto == null )
           {
                return BadRequest("Nepravilan unos stola!");
           }
                   
           if(sto.BrojStola > 500)        
           {
                return BadRequest("Broj stola je izvan opsega");
           }
           try
           {
               
                var pivnica=await Context.Pivnice.FindAsync(pivnicaID);
                if(pivnica !=null)
                {
                 sto.Pivnica=pivnica;   
                 Context.Stolovi.Add(sto);
                await Context.SaveChangesAsync();
                return Ok("Sto je uspesno dodat");
                }
                else
                {
                    return BadRequest("Pivnica nije pronadjena!");
                }
           }
          
          catch(Exception e)
          {
              return BadRequest(e.Message);
          }
       }
       [Route("DodajStoUzPomocBroja/{broj}")]
       [HttpPost]
       public async Task<ActionResult> DodajSto(int broj)
       {
           
           
                   
           if(broj > 500 || broj < 0)        
           {
                return BadRequest("Broj stola je izvan opsega");
           }
           try
           {
            
            var postojeciSto= await Context.Stolovi.Where(p=> p.BrojStola == broj).FirstOrDefaultAsync();
            if(postojeciSto != null)
            {
               return BadRequest("Vec postoji sto sa zeljenim brojem");
            }
               
                var sto= new Sto
                                {
                                    BrojStola=broj
                                };
                    
                 Context.Stolovi.Add(sto);
                await Context.SaveChangesAsync();
                return Ok("Sto je uspesno dodat");
           }
          
          catch(Exception e)
          {
              return BadRequest(e.Message);
          }
       }

      [Route("AzurirajSto/{id}/{brojStola}")]
      [HttpPut]
      public async Task<ActionResult> IzmeniSto(int id, int brojStola)
      {
          if(id <= 0)
          {
              return BadRequest("Pogresan ID.");
          }
          if(brojStola > 500 )
          {
              return BadRequest("Broj stola mora biti manji od 500." );
          }
          try
          {
              var sto=await Context.Stolovi.FindAsync(id);
              if(sto != null)
              {
                  sto.BrojStola=brojStola;
                  await Context.SaveChangesAsync();
                  return Ok($"Uspesno azuriran broj stola sa ID-jem:{sto.ID} ");
              }
              else
              {
                  return BadRequest("Sto nije pronadjen!");
              }

          }
          catch(Exception e)
          {
              return BadRequest(e.Message);
          }
      }
      [Route("IzbrisiSto/{id}")]
      [HttpDelete]
      public async Task<ActionResult> Izbrisi(int id)
      {
          if( id <= 0 )
          {
              return BadRequest("Pogresan ID!");
          }
          try
          {
              var sto=await Context.Stolovi
              .Include(p=>p.Narudzbine)
              .Where(q=>q.ID==id)
              .FirstOrDefaultAsync();

              
              if(sto == null)
              {
                  return BadRequest("Nije pronadjen sto sa zeljenim ID-jem.");
              }
              else 
              if( sto.Narudzbine.Count() != 0)
              {
                  return BadRequest("Sto se ne moze obrisati, jer sadrzi narudzbine!");
              }
              else
              {
                   Context.Stolovi.Remove(sto);
                   await Context.SaveChangesAsync();
                   return Ok("Sto je uspesno obrisan.");

              }
          }
          catch(Exception e)
          {
              return BadRequest(e.Message);
          }
      }
      

    }
}
