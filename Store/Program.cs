using System.ComponentModel.DataAnnotations;
using System.Text;
using Store;
Console.OutputEncoding = Encoding.UTF8;
List<Kunde> Kunden = new List<Kunde> ();
List<Produkte> Ware = new List<Produkte>
{
    new Produkte ("Waschmaschine", 400),
    new Produkte("Kühlschrank", 500),
    new Produkte("Mikrowelle", 150),
    new Produkte("Staubsauger", 120),
    new Produkte("Fernseher", 800),
    new Produkte("Laptop", 900),
    new Produkte("Mixer", 70),
    new Produkte("Kaffeemaschine", 130),
    new Produkte("Toaster", 30),
    new Produkte("Haartrockner", 45)
};
List<Warenkrob> warenkorb = new List<Warenkrob>();

do
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Herzlich Willkommen bei E-Store");
    Console.WriteLine("Sind Sie bereits ein Kunde bei uns?\n\t1. Ja\n\t2. Nein");
    string kundeAntwort = Console.ReadLine().ToLower();

    switch (kundeAntwort)
    {
        case "ja":
            Console.WriteLine("Das ist sehr schön. Was ist Ihr Benutzername?");
            string benutzerName = Console.ReadLine().ToLower();

            bool kundeGefunden = false;

            foreach (Kunde kunde in Kunden)
            {
                if (kunde.BenutzerName.ToLower() == benutzerName)
                {
                    Console.WriteLine($"Hallo {kunde.BenutzerName}, Wir haben für Sie die folgenden Produkte:");

                    kundeGefunden = true;
                    foreach (Produkte produkt in Ware)
                    {
                        Console.WriteLine($"\t {produkt.ProduktName} - {produkt.Preis}€");
                    }
                    Console.WriteLine("\nWählen Sie von der gegebenen Liste, was Sie kaufen möchten (Oder 'exit', um zu beenden):");
                    string produktname = Console.ReadLine().ToLower();

                    while (produktname != "nein")
                    {
                        Produkte zukaufen = Ware.Find(p => p.ProduktName.ToLower() == produktname);
                        if (zukaufen != null)
                        {
                            warenkorb.Add(new Warenkrob(zukaufen.ProduktName, zukaufen.Preis));
                            Console.WriteLine($"{zukaufen.ProduktName} wurde zu Ihrem Warenkorb hinzugefügt.");
                        }
                        else
                        {
                            Console.WriteLine("Produkt nicht gefunden. Bitte erneut versuchen.");
                        }
                        Console.WriteLine("Möchten Sie ein weiteres Produkt hinzufügen?\n\tDann Schreiben Sie die Produktname\n\t[Nein] zu Warenkrob geheh");
                        produktname = Console.ReadLine().ToLower();
                    }

                    Console.WriteLine("\nIhre gekauften Produkte:");
                    foreach (Warenkrob  p in warenkorb)
                    {
                        Console.WriteLine($"{p.Produktname} - {p.Preiss}€");
                    }


                    Console.WriteLine("\nWie möchten Sie bezahlen?  \n\t1. Überweisung\n\t2. Rechnung");
                    string kundenantwort3 = Console.ReadLine().ToLower();

                    switch (kundenantwort3)
                    {
                        case "überweisung":
                            double totalPreis = 0;
                            foreach (var item in warenkorb)
                            {
                                totalPreis += item.Preiss;
                            }
                            Console.WriteLine($"Es wird von Ihrem Konto DE xyz {totalPreis}€ abgebucht.");
                           
                            
                            //Console.WriteLine($"Ihre Bestellung wird in ca. 3 Tage an die Adress:\n\t{kunde.BenutzerName}\n\t{kunde.Strasse}{kunde.Hausnummer}\n\t{kunde.Plz}\n\t{kunde.Stadt}");

                            


                            break;
                        case "rechnung":
                             string emailadresse = "";
                            foreach (var e in Kunden) { emailadresse += e.Email; }

                            Console.WriteLine($"Sie erhalten eine Rechnung an der {emailadresse}");

                             
                            break;

                      
                    }
                    Console.WriteLine($"Ihre Bestellung wird in ca. 3 Tage an die Adress:\n\t{kunde.BenutzerName}\n\t{kunde.Strasse}{kunde.Hausnummer}\n\t{kunde.Plz}\n\t{kunde.Stadt}");




                }

                if (kundeGefunden)
                {
                    break;
                }
            }

            if (!kundeGefunden)
            {
                Console.WriteLine("Leider haben wir Ihren Benutzernamen nicht gefunden. Bitte wählen Sie 2, um sich zu registrieren.");
            }
            break;

        case "nein":
            Console.WriteLine("Wie heißen Sie?");
            string neuerBenutzerName = Console.ReadLine();
            Console.WriteLine("Straße:");
            string straße = Console.ReadLine();
            Console.WriteLine("Hausnummer:");
            int hausnummer = int.Parse(Console.ReadLine());
            Console.WriteLine("PLZ:");
            int plz = int.Parse(Console.ReadLine());
            Console.WriteLine("Stadt:");
            string stadt = Console.ReadLine();
            Console.WriteLine("E-Mail:");
            string email = Console.ReadLine();

            Kunde neuerKunde = new Kunde(neuerBenutzerName, straße, hausnummer, plz, stadt, email);
            Kunden.Add(neuerKunde);
            Console.WriteLine("Sie wurden erfolgreich registriert. Ihre Daten:");
            Console.WriteLine(neuerKunde);
            break;

        default:
            Console.WriteLine("Ungültige Option.");
            break;
    }
    

    Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
    Console.ReadKey();
} while (true);






