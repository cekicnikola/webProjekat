using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backEnd.Models
{
    public class Sto
    {
        [Key]
        public int ID { get; set; }
        
        public int BrojStola { get; set; }
       
        
        public List<Narudzbina> Narudzbine { get; set; }
        
        [JsonIgnore]
        public Pivnica Pivnica { get; set; }

    }
}