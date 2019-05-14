using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace priprema_za_klkv
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] procesi = Process.GetProcesses();
            string funkcija;
            string pid;
            Console.WriteLine(@"C:\Kolokvij\zadatak\procesi\bin\debug\proces");
            funkcija = Console.ReadLine();


            // SORTIRARNO PRIORITET -----------------------------------------------------------------------
            if (funkcija == "sortiranoPrioritet")
            {
                Console.WriteLine("{0,-40}{1,-10}{2,-15}", "Naziv", "PID", "ID");
                Console.WriteLine("{0,-40}{1,-10}{2,-15}", "================================", "=======", "===========");

                for (int x = 0; x < procesi.Count(); x++)
                {
                    for (int y = 0; y < procesi.Count(); y++)
                    {
                        if (procesi[x].BasePriority > procesi[y].BasePriority)
                        {
                            var pamti = procesi[y];
                            procesi[y] = procesi[x];
                            procesi[x] = pamti;
                        }
                    }
                }
                //Ispis u konzolu 
                foreach (Process p in procesi)
                {
                    Console.WriteLine("{0,-40}{1,-15}{2,-15}", p.ProcessName, p.Id, p.BasePriority);
                }
                //Ispis po prioritetima u text file
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\Ema\Desktop\sortiranoPrioritet.txt"))
                {

                    file.WriteLine("{0,-40}{1,-10}{2,-15}", "Naziv", "PID", "ID");
                    file.WriteLine("{0,-40}{1,-10}{2,-15}", "================================", "=======", "===========");
                    foreach (Process p in procesi)
                    {
                        file.WriteLine("{0,-40}{1,-15}{2,-15}", p.ProcessName, p.Id, p.BasePriority);
                    }
                }
            }
            // DETALJNIJE -----------------------------------------------------------------------------------------------
            if (funkcija == "detaljnije")
            {
                pid = Console.ReadLine();


                foreach (Process p in procesi)
                {
                    int brojDretvi = p.Threads.Count;
                    if (pid == p.Id.ToString())
                    {
                        //Console.WriteLine("{0,-10}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}", p.ProcessName.ToString(), brojDretvi, p.StartTime, p.MachineName, p.TotalProcessorTime, p.PrivateMemorySize64);
                        Console.WriteLine("Naziv procesa: {0}", p.ProcessName.ToString());
                        Console.WriteLine("Broj dretvi: {0}", brojDretvi);
                        Console.WriteLine("Vrijeme pokretanja: {0}", p.StartTime);
                        Console.WriteLine("Naziv racunala na kojem je pokrenut: {0}", p.MachineName);
                        Console.WriteLine("Ukupno iskoristeno vrijeme procesora: {0}", p.TotalProcessorTime);
                        Console.WriteLine("Peak memorije: {0} MB", p.PeakWorkingSet64 / (1024 * 1024));

                        using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(@"C:\Users\Ema\Desktop\" + p.Id + ".txt"))
                        {
                            file.WriteLine("Naziv procesa: {0}", p.ProcessName.ToString());
                            file.WriteLine("Broj dretvi: {0}", brojDretvi);
                            file.WriteLine("Vrijeme pokretanja: {0}", p.StartTime);
                            file.WriteLine("Naziv racunala na kojem je pokrenut: {0}", p.MachineName);
                            file.WriteLine("Ukupno iskoristeno vrijeme procesora: {0}", p.TotalProcessorTime);
                            file.WriteLine("Peak memorije: {0} MB", p.PeakWorkingSet64 / (1024 * 1024));
                        }

                    }
                }
            }

            //ispis procesa koji koriste vise memorije od upisane (u mb)
            if (funkcija == "memorijaViseOd")
            {
                int memorija = Console.Read();
                foreach (Process p in procesi)
                {
                    var memo = p.WorkingSet64;
                    if (memo / (1024 * 1024) > memorija)
                    {

                        Console.WriteLine("{0,-40}{1,-15}{2,-15}", p.ProcessName, p.Id, memo / (1024 * 1024));

                    }
                }
            }


            //ispis procesa koji zapocinju na zadano slovo
            if (funkcija == "filtrirajPremaSlovu")
            {
                int br = 0;
                string sl = Console.ReadLine();
                Console.WriteLine("{0,-40}{1,-10}{2,-15}", "Naziv", "PID", " ID");
                Console.WriteLine("{0,-40}{1,-10}{2,-15}", "================================", "=======", "===========");
                foreach (Process p in procesi)
                {
                    var ime = p.ProcessName;

                    if (ime[0] == sl[0])
                    {
                        br++;
                        Console.WriteLine("{0,-40}{1,-15}{2,-15}", p.ProcessName, p.Id, p.BasePriority);

                    }


                }
                Console.WriteLine("Ukupno {0} procesa zapocinje na slovo {1}", br, sl);
            }
        }
    }
}
