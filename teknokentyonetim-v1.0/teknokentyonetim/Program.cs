using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB;


namespace teknokentyonetim
{
    class Yonetim
    {
        public string firma_adi;
        public int calisansayisi;
        public int kurulustarihi;
        public string sektor;
        public int firmaid;
 
        Hizmet_giderleri hg = new Hizmet_giderleri();
        public class Hizmet_giderleri
        {
            public int personel_ucret;
            public string personel_pozisyon;
            public string blok;
            public string izin;
        }
        public class Otopark
        {
            public string plakaismi;
            public string plaka;
            public int odeme;
                        
        }

    }
    class Program
    {
        private static int kullanici_parola;
        private static string kullanici_adi;
        public static string kullaniciadi = "burak";
        public static int parola = 1656;
        public static int girissay = 3;
        private static int secim;
        static void Main(string[] args)
        {

            //              Dosya yazım alanı
            string dosya_yolu = @"C:\Users\Masaüstü\kayitlar.txt";// sizde programın bulunduğu yer
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            //

            //////////////////FONKSİYON YAZIM ALANI/////////

            Logger logger = LogManager.GetCurrentClassLogger();//Loglama servisi

            void Database()
            {
                // data base kontrol islemi
                string baglantiSatiri = "Server=localhost;User ID=******;password=******;Database=teknokent";

                NpgsqlConnection baglanti = new NpgsqlConnection(baglantiSatiri);
                NpgsqlCommandBuilder cm = new NpgsqlCommandBuilder();

                try
                {
                    baglanti.Open();
                    Console.Write("Baglantı kuruldu\n");
                    Console.ReadKey();
                }
                catch (Exception)
                {

                    Console.Write("Baglantı yok\n");
                    Console.ReadKey();
                }
            }//database son
            void Firma_bilgisi()
            {
                NpgsqlCommand commend = new NpgsqlCommand();

                Yonetim yonetim1 = new Yonetim();


                Console.WriteLine("Çalısan sayısı giriniz:");
                yonetim1.calisansayisi = Convert.ToInt32(Console.ReadLine());
                sw.WriteLine("Çalışan sayisi: " + yonetim1.calisansayisi);

                Console.WriteLine("Firma adi giriniz:");
                yonetim1.firma_adi = Console.ReadLine();
                sw.Write("Firma adı: " + yonetim1.firma_adi);

                Console.WriteLine("Kuruluş tarihi giriniz(Sadece sayı):");
                yonetim1.kurulustarihi = Convert.ToInt32(Console.ReadLine());
                sw.WriteLine("\nKuruluş tarihi: " + yonetim1.kurulustarihi);

                Console.WriteLine("Firmanın çalışacağı sektörü giriniz:");
                yonetim1.sektor = Console.ReadLine();
                sw.WriteLine("\nSektör: " + yonetim1.sektor);

                Random rnd = new Random();
                if (yonetim1.sektor == "yazilim")
                {

                    yonetim1.firmaid = rnd.Next(1, 10000);
                    Console.WriteLine("Firma için hazırlanan ID:" + yonetim1.firmaid);
                    Console.WriteLine("Konum:C Blok");
                    sw.WriteLine("ID:" + yonetim1.firmaid + " C Blok");
                    Console.Read();
                }
                else if (yonetim1.sektor == "biomedikal")
                {
                    yonetim1.firmaid = rnd.Next(10001, 20000);
                    Console.WriteLine("Firma için hazırlanan ID:" + yonetim1.firmaid);
                    Console.WriteLine("Konum:E Blok");
                    sw.WriteLine("ID:" + yonetim1.firmaid + "E Blok");
                    Console.Read();
                }
                else if (yonetim1.sektor == "enerji")
                {
                    yonetim1.firmaid = rnd.Next(20001, 30000);
                    Console.WriteLine("Firma için hazırlanan ID:" + yonetim1.firmaid);
                    Console.WriteLine("Konum:A Blok");
                    sw.WriteLine("ID:" + yonetim1.firmaid + " A Blok");
                    Console.Read();
                }
                else if (yonetim1.sektor == "savunma")
                {
                    yonetim1.firmaid = rnd.Next(30001, 40000);
                    Console.WriteLine("Firma için hazırlanan ID:" + yonetim1.firmaid);
                    Console.WriteLine("Konum:F Blok");
                    sw.WriteLine("ID:" + yonetim1.firmaid + "  F Blok");
                    Console.Read();
                }
                else
                {
                    Console.WriteLine("HATALI İŞLEM");
                    Console.Read();
                    logger.Error(kullanici_adi + " Hatali islem yaptı");//log
                }
                sw.Flush();
                sw.Close();
                fs.Close();

                

                NpgsqlConnection baglanti = new NpgsqlConnection(baglantiSatiri);
                DataSet dataset = new DataSet();
                string sql = "INSERT INTO hizmetler(isim,calisansayisi,kurulustarihi,sektor,firmaid) VALUES(@p1,@p2,@p3,@p4,@p5)";

                NpgsqlDataAdapter add = new NpgsqlDataAdapter(sql, baglantiSatiri);
                add.Fill(dataset);

                baglanti.Close();

            }//firmabilgisi son
            void Hizmetler()
            {

                Yonetim.Hizmet_giderleri hg = new Yonetim.Hizmet_giderleri();

                Console.WriteLine("Personel ucreti giriniz");
                hg.personel_ucret = Convert.ToInt32(Console.ReadLine());
                sw.WriteLine(hg.personel_ucret);

                Console.WriteLine("Çalışan pozisyonunu giriniz:");
                hg.personel_pozisyon = Console.ReadLine();
                sw.WriteLine(hg.personel_pozisyon);

                Console.WriteLine("Personelin hangi blokta çalışacagini giriniz");
                hg.blok = Console.ReadLine();
                sw.WriteLine(hg.blok);

                Console.WriteLine("Personel izin günleri:");
                hg.izin = Console.ReadLine();
                sw.WriteLine(hg.izin);
                sw.Flush();
                sw.Close();
                fs.Close();
            }//hizmetler son           
            int Giris()
            {
                Console.WriteLine("Kullanıcı adı:");
                kullanici_adi = Console.ReadLine();

                Console.WriteLine("Parola:");
                kullanici_parola = Convert.ToInt32(Console.ReadLine());

                if (kullanici_adi == kullaniciadi && kullanici_parola == parola)
                {

                    Console.WriteLine("Giriş başarılı");
                    logger.Info("Giris yapan kullanici: " + kullanici_adi);//log

                    return 1;
                }
                else//kullanıcı giris ekranı else 
                {
                    Console.WriteLine("Kullanıcı adı veya parola yanlış");
                    logger.Info("Kullanıcı adı:" + kullanici_adi + "parola" + kullanici_parola + " ile kullanıcı yanlıs login oldu");//log

                    girissay -= 1;

                    if (girissay == 0)
                    {
                        Console.WriteLine("SİSTEME GİRİŞ HAKKINIZ DOLDU..");
                        logger.Info(kullanici_adi + " - " + kullanici_parola + " 3 kez yanlıs giris yaptı");
                        return 0;

                    }
                    return 2;

                }



            }
            void otopark()
            {
                Yonetim.Otopark otopark1 = new Yonetim.Otopark();
                Console.WriteLine("Araç Sahibinin İsmi:");
                otopark1.plakaismi = Console.ReadLine();
                Console.WriteLine("Araç Plakası:");
                otopark1.plaka = Console.ReadLine();
                Console.WriteLine("Sticker Durumu\n[1] VAR [2] YOK");
                otopark1.odeme = Convert.ToInt16(Console.ReadLine());
                if (otopark1.odeme == 1)
                {
                    Console.WriteLine("Geçiş izni verildi...\n");
                    Console.Read();
                }
                else
                {
                    Console.WriteLine("Geçiş rededildi...\n");
                    Console.Read();
                }


            }//otopark son
             ////////////////////////////////////////////////
            

           
            ///////////////////////////////////////////////
            Database();
            while (true)
            {
                int donusDegeri = Giris();

                if (donusDegeri == 0)
                {
                    Console.Read();
                    break;
                }
                if (donusDegeri == 2)
                {
                    continue;
                }
                if (donusDegeri == 1) //giris calisti
                {
                    Console.Write("\n\nİslem seçiniz\n[1]Yönetim Portalı [2]Hizmetler [3]Otopark [4]Çıkış");
                    secim = Convert.ToInt32(Console.ReadLine());
                    if (secim == 1)
                    {
                        Firma_bilgisi();
                        continue;

                    }
                    else if (secim == 2)
                    {
                        Hizmetler();
                        continue;
                    }
                    else if (secim == 4)
                    {
                        Console.Write("\nSistemden çıkış yapılacak...");
                        logger.Trace("Cikis yapan kullanici: " + kullanici_adi);//log
                        Console.Read();
                        break;
                    }
                    else if (secim == 3)
                    {
                        otopark();
                    }
                    else
                    {
                        Console.Write("\nHATALI İŞLEM YAPTINIZ..\n");
                        logger.Error(kullanici_adi + " Hatalı islem yaptı");//log
                        continue;
                    }
                }

            }//while son
            
            











        }//main son
    }//PROGRAM CLASS SON
}//NAMESPACE SON

