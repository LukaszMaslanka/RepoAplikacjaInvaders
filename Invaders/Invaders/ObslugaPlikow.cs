using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Invaders
{
    class ObslugaPlikow
    {
        private struct GraczWyniki
        {
            public string NazwaStatku;
            public int Punkty;
            public string DataRyzgrywki;
        }

        public static string wyniki = "";
        static List<string> lines = new List<string>();
        static List<GraczWyniki> wynikiGraczy = new List<GraczWyniki>();
        /// <summary>
        /// Odczyt danych z pliku a następnie zapis danych w zmiennej
        /// typu string wyniki
        /// </summary>
        public static void OdczytajDane()
        {
            GraczWyniki wynikGracza;

            if (File.Exists("wyniki.txt"))
            {
                //odczytanie danych z pliku o strukturze csv
                var odczytDanych = new StreamReader(File.OpenRead("wyniki.txt"));
                while (!odczytDanych.EndOfStream)
                {
                    //doczytanie pliku ze splitem
                    var line = odczytDanych.ReadLine();
                    var values = line.Split(';');

                    //wpisywanie do struktury danych z pliku
                    wynikGracza.NazwaStatku = values[0];
                    wynikGracza.Punkty = int.Parse(values[1]);
                    wynikGracza.DataRyzgrywki = values[2];

                    //dodanie struktury do listy struktur
                    wynikiGraczy.Add(wynikGracza);
                }

                //Linq Sortowanie
                wynikiGraczy = wynikiGraczy.OrderByDescending(o => o.Punkty).ToList();

                int roznica;
                int dlugoscLancucha;

                foreach (GraczWyniki graczWyniki in wynikiGraczy)
                {
                    roznica = 10;
                    dlugoscLancucha = 0;
                    //wyliczenie dlugosci odstepu po nazwie, 10 to max ilosc znakow w txtbox
                    if (graczWyniki.NazwaStatku.Length < 10)
                    {
                        dlugoscLancucha = roznica - graczWyniki.NazwaStatku.Length;
                    }
                    wyniki += "Gracz: " + graczWyniki.NazwaStatku;
                    for (int i = 0; i < dlugoscLancucha; i++)
                    {
                        wyniki += " ";
                    }
                    
                    roznica = 4;
                    dlugoscLancucha = 0;
                    //wyliczenie dlugosci odstepu po punktach, 4 to max dlugosc liczby
                    if (graczWyniki.Punkty.ToString().Length < 4)
                    {
                        dlugoscLancucha = roznica - graczWyniki.Punkty.ToString().Length;
                    }
                    wyniki += " punkty: " + graczWyniki.Punkty;
                    for (int i = 0; i < dlugoscLancucha; i++)
                    {
                        wyniki += " ";
                    }

                    wyniki += " data rozgrywki: " + graczWyniki.DataRyzgrywki + Environment.NewLine;
                }
                odczytDanych.Close();
                
                wynikiGraczy.Clear();
            }
        }

        /// <summary>
        /// Zapis do pliku wyniki.txt.
        /// </summary>
        public static void ZapiszDane(StatekGracza statekGracza)
        {
            string nowyWpis;

            nowyWpis = statekGracza.NazwaStatku + ";" + statekGracza.Punkty + ";" + DateTime.Now.ToString("dd/MM/yyyy");

            //Linq wypełnienie listy lines wpisami z pliku z pominieciem pustych wierszy
            if (File.Exists("wyniki.txt"))
            {
                lines = File.ReadAllLines("wyniki.txt").Where(arg => !string.IsNullOrWhiteSpace(arg)).ToList();
            }
            
            //dodaniego nowego wpisu do listy
            lines.Add(nowyWpis);
                       
            //Zapis listy do pliku
            StreamWriter zapisDanych = new StreamWriter("wyniki.txt");
            foreach (string line in lines)
            {
                zapisDanych.Write(line);
                zapisDanych.WriteLine();
            }
            zapisDanych.Close();
        }

        
        /// <summary>
        /// Zapis do pliku wyniki.txt. Plik zapisuje się w lokalizacji programu.
        /// </summary>
        public static void ZapiszDane(StatekGracza statekGracza1, StatekGracza statekGracza2)
        {
            string nowyWpisGracz1;
            string nowyWpisGracz2;

            nowyWpisGracz1 = statekGracza1.NazwaStatku + ";" + statekGracza1.Punkty + ";" + DateTime.Now.ToString("dd/MM/yyyy");
            nowyWpisGracz2 = statekGracza2.NazwaStatku + ";" + statekGracza2.Punkty + ";" + DateTime.Now.ToString("dd/MM/yyyy");

            //Linq wypełnienie listy lines wpisami z pliku z pominieciem pustych wierszy
            if (File.Exists("wyniki.txt"))
            {
                lines = File.ReadAllLines("wyniki.txt").Where(arg => !string.IsNullOrWhiteSpace(arg)).ToList();
            }

            //dodanie nowych wpisow do listy
            lines.Add(nowyWpisGracz1);
            lines.Add(nowyWpisGracz2);

            //Zapis listy do pliku
            StreamWriter zapisDanych = new StreamWriter("wyniki.txt");
            foreach (string line in lines)
            {
                zapisDanych.Write(line);
                zapisDanych.WriteLine();
            }
            zapisDanych.Close();
        }

        public static bool wavSkopiowane = true;
        /// <summary>
        /// Kopiowanie plików *.wav do folderu Temp
        /// </summary>
        public static void KopiujWav()
        {
            if (File.Exists(Path.GetTempPath() + "SoundLaserShot.wav"))
            {
                //Plik istnieje;
                wavSkopiowane = true;
            }
            else
            {
                try
                {
                    File.Copy(@"Resources\SoundLaserShot.wav", Path.GetTempPath() + "SoundLaserShot.wav");
                    wavSkopiowane = true;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Błąd: " + e.Message);
                    wavSkopiowane = false;
                }

            }

            if (File.Exists(Path.GetTempPath() + "SoundBoom.wav"))
            {
                //Plik istnieje
                wavSkopiowane = true;
            }
            else
            {
                try
                {
                    File.Copy(@"Resources\SoundBoom.wav", Path.GetTempPath() + "SoundBoom.wav");
                    wavSkopiowane = true;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Błąd: " + e.Message);
                    wavSkopiowane = false;
                }
            }
        }
    }
}
