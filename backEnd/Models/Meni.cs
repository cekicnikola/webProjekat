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
        public string Promocije { get; set; }
        
        
        public List<PivoHrana> Stavke { get; set; }
    
        public int PivnicaID { get; set; }
        [ForeignKey("PivnicaID")]
        public Pivnica Pivnica { get; set; }   

        
    }
}