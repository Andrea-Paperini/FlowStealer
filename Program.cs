using System;
using System.IO;
using System.Linq;

namespace FlowStealer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Benvenuto!");
            var options = new EnumerationOptions()
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = true
            };
            int porta = 8080;
            //stabisco variabili vuote
            string percorsoVLC = "";
            //cerco dentro il disco principale i programmi vlc.exe e obs64.exe cosi recupero i loro percorso dinamicamente
            foreach (string file in Directory.EnumerateFiles(@"c:\", "vlc.exe", options).Select(file => Path.GetDirectoryName(file)))
            {
                if (file.Contains("VLC") == true)
                {
                    //Console.WriteLine("Percorso trovato: "+file);
                    //inserisco il valore del percorso trovato dentro una variabile esterna
                    percorsoVLC += file;
                    break;
                }
            }
            Console.WriteLine("Percorso VLC trovato: " + percorsoVLC);
            if (string.IsNullOrEmpty(percorsoVLC))
            {
                Console.WriteLine("Errore ! Non è stato possibile trovare il percorso di vlc, assicurati di aver installato il programma correttamente");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Inserisci il link su cui effettuare lo streaming: ");
            var link = Console.ReadLine();
            //finchè il link non è valido continuo a chiederlo
            while (string.IsNullOrEmpty(link) || link.Length < 10 || !link.Contains("http"))
            {
                Console.WriteLine("Il link non è valido !");
                Console.WriteLine("Inseriscilo nuovamente !");
                link = Console.ReadLine();
            }
            //string percorsoOBS = @"C:\Program Files\obs-studio\bin\64bit";
            //            CMD / C     Esegui comando e quindi termina
            //            CMD / K     Esegui comando e poi torna al prompt CMD . Questo è utile per test , per esaminare le variabili
            //apro vlc e avvio la diretta sulla porta selezionata 
            //aggiungo il parametro per lo schermo intero e per il buffering da 10 secondi per mantenere lo streaming piu' fluido'
            string comando =  @"/C cd " + percorsoVLC + "  & vlc.exe " + link + " :network-caching=1000 :sout=#transcode{vcodec=theo,vb=1600,scale=1,acodec=none}:http{mux=ogg,dst=:" + porta + "/stream.ogg} :no-sout-rtp-sap :no-sout-standard-sap :sout-keep";
            //Console.WriteLine("azz "+comando);
            System.Diagnostics.Process.Start("CMD.exe", comando);
        }
    }
}
