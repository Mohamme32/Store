using System.Text;
using Store;
Console.OutputEncoding = Encoding.UTF8;
List<Warenkrob> warenkorb = new List<Warenkrob>();
Produkte.DeserializeAll();
Kunde.DeserializeAll();
do
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Herzlich Willkommen bei E-Store");
    Console.WriteLine("Sind Sie bereits ein Kunde bei uns?\n\t1. Ja\n\t2. Nein");
    Console.ForegroundColor = ConsoleColor.White;
    var kundeAntwort = Console.ReadLine().ToLower();
    switch (kundeAntwort)
    {
        case "ja":
            Kunde.BreitsKunde();          
            break;
        case "nein":
            Kunde.NeueKunde();
            break;      
        default:
            Console.WriteLine("Ungültige Option.");
            break;
    }
    Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
    Console.ReadKey();
} while (true);
