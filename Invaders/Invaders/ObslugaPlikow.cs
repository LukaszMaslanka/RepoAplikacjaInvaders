using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Invaders
{
    class ObslugaPlikow
    {
        public static string wyniki = "";
        /// <summary>
        /// Odczyt danych z pliku a następnie zapis danych w zmiennej
        /// typu string wyniki
        /// </summary>
        public static void OdczytajDane()
        {
            if (File.Exists("wyniki.txt"))
            {
                StreamReader odczytDanych = new StreamReader("wyniki.txt");
                //Odczyt danych z pliku do zmiennej w celu wpisania ich przy zapisie nowego pliku
                while (!odczytDanych.EndOfStream)
                {
                    wyniki = odczytDanych.ReadToEnd();
                }
                odczytDanych.Close();
            }
        }

        /// <summary>
        /// Zapis do pliku wyniki.txt.
        /// </summary>
        public static void ZapiszDane(StatekGracza statekGracza, int punkty)
        {
            //Zapis danych do pliku
            StreamWriter zapisDanych = new StreamWriter("wyniki.txt");
            int roznica = 20;
            int dlugoscLancucha = 0;
            /***Wyliczenie długości odstępu po Nazwie stattku***/
            if (statekGracza.NazwaStatku.Length < 20)
            {
                dlugoscLancucha = roznica - statekGracza.NazwaStatku.Length;
            }

            zapisDanych.Write("Gracz: " + statekGracza.NazwaStatku);
            for (int i = 0; i < dlugoscLancucha; i++)
            {
                zapisDanych.Write(" ");
            }
            /************************************************/
            zapisDanych.Write(" | punkty: " + punkty);
            zapisDanych.WriteLine("\n");
            /***42 to suma wszystkich znaków w linie gdy wszystkie wartości są maksymalne***/
            for (int i = 0; i < 42; i++)
            {
                zapisDanych.Write("-");
            }
            zapisDanych.WriteLine("\n");
            /***********************************************************************************/
            //zapis danych z zmiennej wyniki czyli z poprzedniej wersji pliku
            zapisDanych.Write(wyniki);
            zapisDanych.Close();
        }

        /// <summary>
        /// Zapis do pliku wyniki.txt. Plik zapisuje się w lokalizacji programu.
        /// </summary>
        public static void ZapiszDane(StatekGracza statekGracza1, StatekGracza statekGracza2, int punktyGracz1, int punktyGracz2)
        {
            //Zapis danych do pliku
            StreamWriter zapisDanych = new StreamWriter("wyniki.txt");
            int roznica = 20;
            int dlugoscLancucha = 0;
            /***Wyliczenie długości odstępu po Nazwie stattku***/
            if (statekGracza1.NazwaStatku.Length < 20)
            {
                dlugoscLancucha = roznica - statekGracza1.NazwaStatku.Length;
            }

            zapisDanych.Write("Gracz: " + statekGracza1.NazwaStatku);
            for (int i = 0; i < dlugoscLancucha; i++)
            {
                zapisDanych.Write(" ");
            }
            /************************************************/
            zapisDanych.Write(" | punkty: " + punktyGracz1);
            zapisDanych.WriteLine("\n");
            /***42 to suma wszystkich znaków w linie gdy wszystkie wartości są maksymalne***/
            for (int i = 0; i < 42; i++)
            {
                zapisDanych.Write("-");
            }
            zapisDanych.WriteLine("\n");
            /***********************************************************************************/

            /***Wyliczenie długości odstępu po Nazwie stattku***/
            if (statekGracza2.NazwaStatku.Length < 20)
            {
                dlugoscLancucha = roznica - statekGracza2.NazwaStatku.Length;
            }

            zapisDanych.Write("Gracz: " + statekGracza2.NazwaStatku);
            for (int i = 0; i < dlugoscLancucha; i++)
            {
                zapisDanych.Write(" ");
            }
            /************************************************/
            zapisDanych.Write(" | punkty: " + punktyGracz2);
            zapisDanych.WriteLine("\n");
            /***42 to suma wszystkich znaków w linie gdy wszystkie wartości są maksymalne***/
            for (int i = 0; i < 42; i++)
            {
                zapisDanych.Write("-");
            }
            zapisDanych.WriteLine("\n");

            //zapis danych z zmiennej wyniki czyli z poprzedniej wersji pliku
            zapisDanych.Write(wyniki);

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
