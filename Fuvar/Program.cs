using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuvar.Properties;

namespace Fuvar
{
    class Program
    {
        static List<Fuvarok> adatok = new List<Fuvarok>();
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Beolvasas(); //Beolvasás Linq-val=>
            Utazasok_Szama(); //3. feladat Linq-val
            Taxis_6185(); //4. feladat Linq-val
            Fizetesi_Modok(); //5. feladat Linq-val
            Osszes_Megtett_Km(); //6. feladat Linq-val
            Leghosszabb_Ideig_Tarto_Fuvar(); //7. feladat Linq-val
            Hibak();//8. feladat vegyes megoldás
            //Taxis_6185_masik_megoldas();
            //OsszesMegtettKmMasik();
            Console.ReadLine();
        }
        private static void Beolvasas() => Resource.fuvar.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Skip(1).ToList().ForEach(a => adatok.Add(new Fuvarok(a)));
        private static void Utazasok_Szama() => Console.WriteLine($"3. feladat: {adatok.Count} fuvar");
        private static void Taxis_6185()=>Console.WriteLine($"4. feladat:{(adatok.Count(a => a.Taxi_Id == 6185))} fuvar alatt: {adatok.Where(a => a.Taxi_Id == 6185).Sum(x=>x.VitelDij)}$");
        private static void Fizetesi_Modok()
        {
            Console.WriteLine("5. feladat:");
            adatok.GroupBy(a => a.FizetesModja).ToList().ForEach(x => Console.WriteLine($"\t{x.Key}: {x.Count()} fuvar"));
        }
        private static void Osszes_Megtett_Km() => Console.WriteLine($"6. feladat:{adatok.Sum(x => Math.Round(x.Megtett_Tav * 1.6,2))}km");
        private static void Leghosszabb_Ideig_Tarto_Fuvar()
        {
            Console.WriteLine("7. feladat: Leghosszabb fuvar:");
            Console.WriteLine($"\tFuvar hossza:{adatok.OrderBy(x=>x.Utazas_Ido).Last().Utazas_Ido} másodperc");
            Console.WriteLine($"\tTaxi azonosító:{adatok.OrderBy(x => x.Utazas_Ido).Last().Taxi_Id}");
            Console.WriteLine($"\tMegtett távolság:{adatok.OrderBy(x => x.Utazas_Ido).Last().Megtett_Tav} km");
            Console.WriteLine($"\tViteldíj:{adatok.OrderBy(x => x.Utazas_Ido).Last().Utazas_Ido}$");
        }
        static void Hibak()
        {
            List<Fuvarok> Hibak = adatok.OrderBy(x => x.Indulas_Ido).ToList();
            StreamWriter Iro = new StreamWriter("hibak.txt", false, Encoding.Default);
            Iro.WriteLine("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");
            for (int i = 0; i < Hibak.Count; i++)
            {
                if (Hibak[i].Megtett_Tav == 0 && (Hibak[i].Utazas_Ido > 0 || Hibak[i].VitelDij > 0))
                {
                    Iro.Write(Hibak[i].Taxi_Id + ";");
                    Iro.Write(Hibak[i].Indulas_Ido + ";");
                    Iro.Write(Hibak[i].Megtett_Tav + ";");
                    Iro.Write(Hibak[i].VitelDij + ";");
                    Iro.Write(Hibak[i].Borravalo + ";");
                    Iro.Write(Hibak[i].FizetesModja + ";");
                }
            }
            Iro.Close();
            Console.WriteLine("8. feladat: hibák.txt");
        }
        #region 4. feladat másik megoldása

        private static void Taxis_6185_masik_megoldas()
        {
            int darab = 0;
            double osszesen = 0;
            for (int i = 0; i < adatok.Count; i++)
            {
                if (adatok[i].Taxi_Id == 6185)
                {
                    darab++;
                    osszesen = osszesen + adatok[i].VitelDij;
                }
            }
            Console.WriteLine($"4. feladat: {darab} fuvar alatt {osszesen} $");
        }
        #endregion
        #region 6. feladat másik megoldás
        private static void OsszesMegtettKmMasik()
        {
            double osszesen = 0;
            for (int i = 0; i < adatok.Count; i++)
            {
                osszesen = osszesen + adatok[i].Megtett_Tav;
            }
            Console.WriteLine($"6. feladat: {Math.Round(osszesen * 1.6, 2)} km");
        }
        #endregion
    }
    class Fuvarok
    {
        public Fuvarok(string sor)
        {
            string[] sorelemek = sor.Split(';');
            this.Taxi_Id = Convert.ToInt32(sorelemek[0]);
            this.Indulas_Ido = Convert.ToDateTime(sorelemek[1]);
            this.Utazas_Ido = Convert.ToInt32(sorelemek[2]);
            this.Megtett_Tav = Convert.ToDouble(sorelemek[3]);
            this.VitelDij = Convert.ToDouble(sorelemek[4]);
            this.Borravalo = Convert.ToDouble(sorelemek[5]);
            this.FizetesModja = sorelemek[6];
        }
        public int Taxi_Id { get; set; }
        public DateTime Indulas_Ido { get; set; }
        public int Utazas_Ido { get; set; }
        public double Megtett_Tav { get; set; }
        public double VitelDij { get; set; }
        public double Borravalo { get; set; }
        public string FizetesModja { get; set; }
    }
}
