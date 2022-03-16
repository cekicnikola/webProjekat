using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backEnd.Models
{
    public class Meni
    {
        [Key]
        public int ID { get; set; }
       
        public string OpisPromocije { get; set; }
        public int MinKolicinaPica { get; set; }
        public int MinKolicinaHrane  { get; set; }
        public int Popust { get; set; }
        public List<PivoHrana> Stavke { get; set; }
    
        public int PivnicaID { get; set; }
        [ForeignKey("PivnicaID")]
        public Pivnica Pivnica { get; set; }   

        
    }
}