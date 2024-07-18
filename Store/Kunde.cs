using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;


namespace Store
{
    public class Kunde
    {
        internal static List<Kunde> Kunden = new List<Kunde>();
        public static List<Warenkrob> warenkorb = new List<Warenkrob>();
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
        public int Plz { get; set; }
        public string Stadt { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"\n{BenutzerName},{Strasse} {Hausnummer}, {Plz} {Stadt}, {Email}";
        }
        public static void BreitsKunde()
       {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Das ist sehr schön. Was ist Ihr Benutzername?");
            Console.ForegroundColor = ConsoleColor.White;
            string benutzerName = Console.ReadLine().ToLower();
           
            Kunde kunde = Kunden.Find(k => k.BenutzerName.ToLower() == benutzerName);
            
            if(kunde != null)
            {
                ProdukteAnzeigen(kunde);
                AddtoWarenKrob();
                Bezahlmethode(kunde);
            }
            else
            {
                Console.WriteLine("Leider haben wir Ihren Benutzernamen nicht gefunden. Bitte wählen Sie 2, um sich zu registrieren.");
            }

        }
        public static void ProdukteAnzeigen(Kunde kunde)
        {
            Console.WriteLine($"Hallo {kunde.BenutzerName}, Wir haben für Sie die folgenden Produkte:");
            foreach (Produkte produkt in Produkte.ware)
            {
                Console.WriteLine($"\t {produkt.ProduktName} - {produkt.Preis}€");
            }
        }
        public static void AddtoWarenKrob()
        {
           
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nWählen Sie von der gegebenen Liste, was Sie kaufen möchten (Oder 'exit', um zu beenden):");
            Console.ForegroundColor = ConsoleColor.White;
            string produktname = Console.ReadLine().ToLower();

            while (produktname != "nein")
            {
                Produkte zukaufen = Produkte.ware.Find(p => p.ProduktName.ToLower() == produktname);
                if (zukaufen != null)
                {
                    warenkorb.Add(new Warenkrob(zukaufen.ProduktName, zukaufen.Preis));
                    Console.WriteLine($"\n{zukaufen.ProduktName} wurde zu Ihrem Warenkorb hinzugefügt.");
                }
                else
                {
                    Console.WriteLine("\nProdukt nicht gefunden. Bitte erneut versuchen.");
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nMöchten Sie ein weiteres Produkt hinzufügen?\n\tDann Schreiben Sie die Produktname\n\t[Nein] zu Warenkrob geheh");
                Console.ForegroundColor = ConsoleColor.White;
                produktname = Console.ReadLine().ToLower();
            }

            Console.WriteLine("\nIhre gekauften Produkte:");
            foreach (Warenkrob p in warenkorb)
            {
                Console.WriteLine($"{p.Produktname} - {p.Preiss}€");
            }

        }
        public static void Bezahlmethode(Kunde kunde)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nWie möchten Sie bezahlen?  \n\t1. Überweisung\n\t2. Rechnung");
            Console.ForegroundColor = ConsoleColor.White;
            var kundenantwort3 = Console.ReadLine().ToLower();

            double totalPreis = 0;
            foreach (var item in warenkorb)
            {
                totalPreis += item.Preiss;
            }

            switch (kundenantwort3)
            {
                case "überweisung":
                    Console.WriteLine($"\nEs wird von Ihrem Konto DE xyz {totalPreis}€ abgebucht.");
                    break;
                case "rechnung":
                    string emailadresse = "";
                    foreach (var e in Kunde.Kunden) { emailadresse += e.Email; }
                    Console.WriteLine($"\nSie erhalten eine Rechnung in Höhe von:{totalPreis}€ an der {kunde.Email}");
                    break;
            }
            Console.WriteLine($"\nIhre Bestellung wird versendet in ca. 3 Tage an der Adress:\n\t{kunde.BenutzerName}\n\t{kunde.Strasse}{kunde.Hausnummer}\n\t{kunde.Plz}\n\t{kunde.Stadt}");
        }
        public static void NeueKunde()
        {
            static string PromptForInput(string prompt)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(prompt);
                Console.ForegroundColor = ConsoleColor.White;
                return Console.ReadLine();
            }
            static int PromptForInt(string prompt)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(prompt);
                Console.ForegroundColor = ConsoleColor.White;
                return int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Bitte geben Sie Ihre Daten für die Registrierung ein:");
            string neuerBenutzerName = PromptForInput("Wie heißen Sie?");
            string straße = PromptForInput("Straße:");
            int hausnummer = PromptForInt("Hausnummer:");
            int plz = PromptForInt("PLZ:");
            string stadt = PromptForInput("Stadt:");
            string email = PromptForInput("E-Mail:");

            Kunde neuerKunde = new Kunde(neuerBenutzerName, straße, hausnummer, plz, stadt, email);
            Kunde.Kunden.Add(neuerKunde);

            Console.WriteLine("\nSie wurden erfolgreich registriert. Ihre Daten:");
            Console.WriteLine(neuerKunde);
            Kunde.SerializeAll();
          
        }
        public static void SerializeAll()
        {
            string jsonString = JsonSerializer.Serialize(Kunde.Kunden);
            File.WriteAllText("Kunden.json", jsonString);

        }
        public static void DeserializeAll()
        {
            var jsonString = File.ReadAllText("Kunden.json");
            List<Kunde> neuerKunde = JsonSerializer.Deserialize<List<Kunde>>(jsonString);
            Kunde.Kunden = neuerKunde;
        }

    }

    public class Produkte
    {

        public int Preis { get; set; }
        public string ProduktName { get; set; }


        public static List<Produkte> ware = new();


        public Produkte(string produktname, int preis)
        {
            Preis = preis;
            ProduktName = produktname;

        }

        public override string ToString()
        {
            return $"{ProduktName} {Preis} Euro";
        }

       
        public static void SerializeAll()
        {

            string json = JsonSerializer.Serialize(ware);
            File.WriteAllText("Produkte.json", json);

        }

        public static void DeserializeAll()
        {
            var jsonString = File.ReadAllText("Produkte.json");
            List<Produkte> WarenListe = JsonSerializer.Deserialize<List<Produkte>>(jsonString);

            ware = WarenListe;

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
