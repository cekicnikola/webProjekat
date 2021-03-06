using System;
using System.Linq;
using System.Threading.Tasks;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace backEnd.Models
{
    [ApiController]
    [Route("[controller]")]
    
    public class NarudzbinaController : ControllerBase
    {
        public PivnicaContext Context { get; set; }
        public NarudzbinaController(PivnicaContext context)
        {
            Context=context;
        }
        [Route("PregledNarudzbina/{stoID}")]
        [HttpGet]
        public async Task<List<Narudzbina>>Preuzmi(int stoID)
        {
        
               // var sto=await Context.Stolovi.FindAsync(stoID);
                
                
                    var narudzbine=await Context.Narudzbine.Where(p=>p.Sto.ID==stoID)
                    .Include(p=>p.PivoHrana)
                    .ToListAsync();
                    
                    return  (narudzbine);
                
                
           
        }

        [Route("Naruci/{stoID}/{pivoHranaID}")]
        [HttpPost]
        public async Task<ActionResult> NaruciPivoHranu(int stoID, int pivoHranaID, [FromBody] Narudzbina narudzbina)
        {
            if(stoID <= 0 || pivoHranaID <= 0 )
            {
                return BadRequest("Nedozvoljena vrednost ID-ja!");
            }
            if(narudzbina.KolicinaHrane ==0 && narudzbina.KolicinaPiva == 0)
            {
                return BadRequest("kolicina pica ili hrane mora imati vrednost !");
            }
            if(narudzbina.KolicinaHrane < 0 && narudzbina.KolicinaPiva < 0)
            {
                return BadRequest("Nepravilan unos kolicine!");
            }
            if(narudzbina.KolicinaHrane > 50 || narudzbina.KolicinaPiva > 150)
            {
                return BadRequest("Nepravilan unos kolicine!");
            }
            if(narudzbina.Doneta == true)
            {
                return BadRequest("Narudzbina ne moze biti doneta i narucena u istom trenutku");
            }
            try
            {
                var sto= await Context.Stolovi.Where(p=>p.ID==stoID).FirstOrDefaultAsync();
                var pivoHrana= await Context.PivaHrana.Where(p=>p.ID==pivoHranaID).FirstOrDefaultAsync();
                if(sto !=null && pivoHrana !=null)
                {
                    narudzbina.ZaNaplatu=pivoHrana.Cena * narudzbina.KolicinaHrane 
                    + pivoHrana.Cena * narudzbina.KolicinaPiva;

                    narudzbina.Sto=sto;
                    narudzbina.PivoHrana=pivoHrana;
                    
                    Context.Narudzbine.Add(narudzbina);
                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno ste narucil ");
                    //vrati ID samo ukoliko je potrebno
                }
                else
                {
                    return BadRequest("Sto ili pivo nisu pronadjeni!");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("DodajJosPiva/{stoID}/{pivoHranaID}/{kolicina}")]
        [HttpPut]
        public async Task<ActionResult> DodajJos(int stoID, int pivoHranaID, int kolicina)
        {
            try
            {
               var narudzbina=await Context.Narudzbine.Where(p=>p.Sto.ID==stoID 
               && p.PivoHrana.ID==pivoHranaID ).Include(p=>p.PivoHrana)
               .FirstOrDefaultAsync();
               if(narudzbina != null)
               {
                   if(narudzbina.PivoHrana.PiceIliHrana ==true)
                   {
                       narudzbina.KolicinaPiva+=kolicina;
                   }
                   else
                   {
                       narudzbina.KolicinaHrane+=kolicina;
                   }
                   narudzbina.ZaNaplatu+=narudzbina.PivoHrana.Cena * kolicina;
                   await Context.SaveChangesAsync();
                   return Ok("Uspesno ste dodali jos piva ili hrane!");
               }
               else
               {
                   return BadRequest("Narudzbina sa zadatim vrednostima nije pronadjena !");
               }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        [Route("Naplati/{stoID}")]
        [HttpDelete]
        public async Task<ActionResult> NaplatiRacun(int stoID)
        {
            try
            {
                //spoji ovu metodu i metodu iznad!
               var  narudzbine=await Context.Narudzbine.Where(p=>p.Sto.ID==stoID).ToListAsync();
               if(narudzbine.Count() ==0 )
               {
                   return BadRequest("Sto nema narudzbine.");
               }

               
                var spoj=await Context.Stolovi.Where(p=>p.ID==stoID).Include(p=>p.Pivnica)
                .ThenInclude(p=>p.Meni).FirstOrDefaultAsync();
                

                var meni=spoj.Pivnica.Meni;
                if(meni == null)
                {
                    return BadRequest("Meni nije pronadjen");
                }
                

                /*var postojiPopust=narudzbine.Where(p=>p.KolicinaHrane >= meni.MinKolicinaHrane
                 || p.KolicinaPiva >= meni.MinKolicinaPica).FirstOrDefault();*/
                 var kolicinaPica=narudzbine.Sum(p=>p.KolicinaPiva);
                 var kolicinaHrane=narudzbine.Sum(p=>p.KolicinaHrane);
                 var racun=0.0f;
                 

                 foreach(Narudzbina stavka in narudzbine)
                 {
                     racun+=stavka.ZaNaplatu;
                     Context.Narudzbine.Remove(stavka);
                 }
                  await Context.SaveChangesAsync();


                 if(kolicinaPica >= meni.MinKolicinaPica || kolicinaHrane >= meni.MinKolicinaHrane)
                 {
                      racun -= racun * (meni.Popust / 100.0f);

                     return Ok($"Racun je sa popustom od {meni.Popust}% i iznosi: {racun}");
                 }
                 else
                 {
                     return Ok($"Racun je bez popusta i iznosi: {racun}");
                 }
            
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    
    

}