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

     public class PivoHranaController : ControllerBase
     {
       public PivnicaContext Context { get; set ;}
       public PivoHranaController( PivnicaContext context)
       {
           Context=context;
       }
       
       [Route("PreuzmiSveStavke")]
       [HttpGet]
       public async Task<ActionResult> Stavke()
       {
           try
           {
               return Ok(await Context.PivaHrana.Select(p=>
               new
               {
                   ID=p.ID,
                   Naziv=p.Naziv,
                   Cena=p.Cena,
                   PiceIliHrana=p.PiceIliHrana,
                   Narudzbine=p.Narudzbine
                   //,Meni=p.Meni

               }).ToListAsync());
           }
           catch(Exception e)
           {
               return BadRequest(e.Message);
           }
       } 

       [Route("VratiPivo/{id}")]
       [HttpGet]
       public async Task<ActionResult> Pivo(int id)
       {
           if(id <= 0)
           {
              return BadRequest("Pogresan ID.");
           }
           try
           {
               var pivo=await Context.PivaHrana
               .Where(p=>p.ID==id && p.PiceIliHrana==true)
               .FirstOrDefaultAsync();
               if(pivo !=null)
               {
                   return Ok (pivo);
               }
               else
               {
                   return BadRequest("Pivo sa zadatim ID-jem ne postoji u bazi.");
               }

           }
           catch(Exception e)
           {
               return BadRequest(e.Message);
           }
       }
       [Route("VratiHranu/{id}")]
       [HttpGet]
       public async Task<ActionResult> Hrana(int id)
       {
           if(id <= 0)
           {
              return BadRequest("Pogresan ID.");
           }
           try
           {
               var hrana=await Context.PivaHrana
               .Where(p=>p.ID==id && p.PiceIliHrana==false)
               .FirstOrDefaultAsync();
               if(hrana !=null)
               {
                   return Ok (hrana);
               }
               else
               {
                   return BadRequest("Hrana sa zadatim ID-jem ne postoji u bazi.");
               }

           }
           catch(Exception e)
           {
               return BadRequest(e.Message);
           }
       }
     [Route("DodajPivoHranu/{meniID}")]
     [HttpPost]
     public async  Task<ActionResult> UpisiPivo( [FromRoute] int meniID, [FromBody] PivoHrana pivo )
     {
         if(meniID <= 0)
           {
               return BadRequest("Pogresan ID.");
           }
         
         if(string.IsNullOrWhiteSpace(pivo.Naziv) || pivo.Naziv.Length > 50 )
         {
             return BadRequest("Nepravilan unos naziva piva ili hrane .");
         }
         
         if(pivo.Cena < 10 || pivo.Cena > 10000)
         {
             return BadRequest("Nedozvoljena vrednost cene ");
         }
         try
         {
             var meni=await Context.Meni.FindAsync(meniID);
             var postojecePivo=await Context.PivaHrana
             .Where(p=>p.Naziv==pivo.Naziv)
             .FirstOrDefaultAsync();
             if(meni != null)
             {
                 if(postojecePivo != null)
                 {
                     return BadRequest("Pivo ili hrana sa datim nazivom vec postoji");
                 }
                 else
                 {
                     pivo.Meni=meni;
                     Context.PivaHrana.Add(pivo);
                     await Context.SaveChangesAsync();
                     return Ok(pivo.ID);
                 }
             }
             else
             {
                 return BadRequest("Meni sa zadatim ID-jem ne postoji!");
             }
             
         }
         catch(Exception e)
         {
             return BadRequest(e.Message);
         }
     } 
     
     [Route("IzmeniPivoHranuUzPomocRute/{id}/{naziv}/{cena}")]
     [HttpPut]
     public async Task<ActionResult> Izmeni(int id, string naziv, float cena)
     {
         if(id <= 0)
           {
               return BadRequest("Pogresan ID.");
           }
        

         if(string.IsNullOrWhiteSpace(naziv) || naziv.Length > 50 )
         {
             return BadRequest("Nepravilan unos naziva piva.");
         }
         
         if(cena < 10.0f || cena > 10000.0f)
         {
             return BadRequest("Nedozvoljena vrednost cene piva");
         }
         
         try
         {
              var pivoHrana= await Context.PivaHrana
             .Where(p=>p.ID==id)
             .FirstOrDefaultAsync();
             var postojecaStavka=await Context.PivaHrana
             .Where(p=>p.Naziv==naziv)
             .FirstOrDefaultAsync();
             if(pivoHrana != null)
             {
                 if(postojecaStavka == null)
                 {
                 pivoHrana.Naziv=naziv;
                 pivoHrana.Cena=cena;

                 await Context.SaveChangesAsync();
                 return Ok($"Pivo sa ID-jem: {pivoHrana.ID} je uspesno izmenjeno.");
                 }
                 else
                 {
                     return BadRequest("Zeljeni Naziv vec postoji!");
                 }
             }
             else
             {
                 return BadRequest("Pivo ili hrana nisu pronadjeni!");
             }
             
         }
         catch(Exception e )
         {
             return BadRequest(e.Message);
         }
     }
     [Route("IzmeniPivoHranu/{id}")]
     [HttpPut]
     public async Task<ActionResult> IzmeniPivo(int id, [FromBody] PivoHrana pivo)
     {
         if(id <= 0)
           {
               return BadRequest("Pogresan ID.");
           }
        

         if(string.IsNullOrWhiteSpace(pivo.Naziv) || pivo.Naziv.Length > 50 )
         {
             return BadRequest("Nepravilan unos naziva piva.");
         }
         
         if(pivo.Cena < 10.0f || pivo.Cena > 10000.0f)
         {
             return BadRequest("Nedozvoljena vrednost cene piva");
         }
         
         try
         {
              var pivoHrana= await Context.PivaHrana.FindAsync(id);
             var postojecaStavka=await Context.PivaHrana
             .Where(p=>p.Naziv==pivo.Naziv)
             .FirstOrDefaultAsync();
             if(pivoHrana != null)
             {
                 if(postojecaStavka == null)
                 {
                     pivoHrana.Naziv=pivo.Naziv;
                     pivoHrana.Cena=pivo.Cena;
                 Context.PivaHrana.Update(pivoHrana);

                 await Context.SaveChangesAsync();
                 return Ok($"Pivo ili hrana  su uspesno izmenjeni.");
                 }
                 else
                 {
                     return BadRequest("Zeljeni Naziv vec postoji!");
                 }
             }
             else
             {
                 return BadRequest("Pivo  nije pronadjeno!");
             }
             
         }
         catch(Exception e )
         {
             return BadRequest(e.Message);
         }
     }

     [Route("IzbrisiPivoHranu/{id}")]
      [HttpDelete]
      public async Task<ActionResult> Izbrisi(int id)
      {
          if( id <= 0 )
          {
              return BadRequest("Pogresan ID!");
          }
          try
          {
              var pivoHrana=await Context.PivaHrana
              .Include(p=>p.Narudzbine)
              .Where(q=>q.ID==id)
              .FirstOrDefaultAsync();

              
              if(pivoHrana == null)
              {
                  return BadRequest("Nije pronadjena hrana ili pivo sa zeljenim ID-jem.");
              }
              else 
              if( pivoHrana.Narudzbine.Count() != 0)
              {
                  return BadRequest("Pivo ili hrana se ne moze obrisati, jer sadrzi narudzbine!");
              }
              else
              {
                   Context.PivaHrana.Remove(pivoHrana);
                   await Context.SaveChangesAsync();
                   return Ok("Pivo ili hrana su uspesno obrisani.");

              }
          }
          catch(Exception e)
          {
              return BadRequest(e.Message);
          }
      }
     /* {za MeniID-proba spajanja tabela}[Route("PreuzmiSveizMenija/{meniID}")]
       [HttpGet]
       public async Task<ActionResult> Sve( int meniID)
       {
           try
           {
               return Ok(await Context.Meni
               .Where(p=>p.ID==meniID) //treba isto q, i onda u select
               //izaberem samo stavke bez podataka o meniju
               .Include(q=>q.Stavke).Select(q=>q).ToListAsync());
             
           }
           catch(Exception e)
           {
               return BadRequest(e.Message);
           }
       } */

     }
     
}