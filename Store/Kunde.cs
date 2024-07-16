using System.Reflection.Metadata.Ecma335;

namespace Store
{
    public  class Kunde
    {
    
    

        public Kunde(string benutzername, string strasse, int hausnummer, int plz, string stadt, string email)
        {
            BenutzerName = benutzername;
            Strasse = strasse;
            Hausnummer = hausnummer;
            Plz = plz;
            Stadt = stadt;
            Email = email;

        }

    public string BenutzerName { get; set; }    
    public string Strasse { get; set; } 
    public int Hausnummer { get; set; }  
    public int Plz{  get; set; } 
    public string Stadt { get; set; }
    public string Email { get; set; }
       
    

        
        public override string ToString()
        {
            return $"\n{BenutzerName},{Strasse} {Hausnummer}, {Plz} {Stadt}, {Email}";
        }


    }
    public class Produkte
    {

        public int Preis { get; set; }
        public string ProduktName { get; set; }


        public Produkte(string produktname, int preis)
        {
            Preis = preis;
            ProduktName = produktname;

        }

        public override string ToString()
        {
            return $"{ProduktName} {Preis} Euro";
        }


    }

    public class Warenkrob
    {

        public int Preiss { get; set; }
        public string Produktname { get; set; }


        public Warenkrob(string produktname, int preis)
        {
            Preiss = preis;
            Produktname = produktname;

        }

        public override string ToString()
        {
            return $"{Produktname} {Preiss} Euro";
        }
    }

    }
