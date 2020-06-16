using System;
using System.Collections.Generic;

namespace XML_Izvještaj
{
    public class Pumpe
    {
        public int ID { get; set; }
        
        public int BrojTankova { get; set; }
       
        public int MatičniBroj { get; set; }

        public string TelBrojKontrolera { get; set; }
        
        public string NazivPumpe { get; set; }
        
        public string Adresa { get; set; }

        //public virtual ICollection<Tankovi> Tankovi { get; set; }
    }
}