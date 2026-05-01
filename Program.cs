using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using KafeYonetim.Models;
#nullable disable

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
class Program
{
    static List<Masa> masalar = new List<Masa>();
    static List<Siparis> tumSiparisler = new List<Siparis>();

    static int toplamMusteri = 0;
    static double toplamCiro = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- KAFE YONETIM SISTEMI ---");
            Console.WriteLine("1- Masa Olustur");
            Console.WriteLine("2- Musteri Oturt");
            Console.WriteLine("3- Siparis Ekle");
            Console.WriteLine("4- Siparisleri Goruntule");
            Console.WriteLine("5- Hesap Al");
            Console.WriteLine("6- Gun Sonu Raporu");
            Console.WriteLine("0- Cikis");

            int secim = OkuInt("Seciminiz: ");

            switch (secim)
            {
                case 1:
                    MasaOlustur();
                    break;
                case 2:
                    MusteriOturt();
                    break;
                case 3:
                    SiparisEkle();
                    break;
                case 4:
                    SiparisGoster();
                    break;
                case 5:
                    HesapKapat();
                    break;
                case 6:
                    GunSonu();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Gecersiz secim!");
                    break;
            }
        }
    }

    static int OkuInt(string mesaj)
    {
        int deger;
        Console.Write(mesaj);
        while (!int.TryParse(Console.ReadLine(), out deger))
        {
            Console.Write("Hatali giris! Tekrar: ");
        }
        return deger;
    }

    static double OkuDouble(string mesaj)
    {
        double deger;
        Console.Write(mesaj);
        while (!double.TryParse(Console.ReadLine(), out deger))
        {
            Console.Write("Hatali giris! Tekrar: ");
        }
        return deger;
    }

    static void MasaOlustur()
    {
        int no = OkuInt("Masa No: ");
        int kapasite = OkuInt("Kapasite: ");

        masalar.Add(new Masa(no, kapasite));
        Console.WriteLine("Masa eklendi.");
    }

    static void MusteriOturt()
    {
        int kisi = OkuInt("Kisi sayisi: ");

        foreach (var masa in masalar)
        {
            if (masa.Durum == "Bos" && masa.Kapasite >= kisi)
            {
                masa.Durum = "Dolu";
                toplamMusteri++;
                Console.WriteLine($"Musteri Masa {masa.MasaNo}'ya yerlestirildi.");
                return;
            }
        }

        Console.WriteLine("Uygun masa yok!");
    }

    static void SiparisEkle()
    {
        int no = OkuInt("Masa No: ");
        var masa = masalar.Find(m => m.MasaNo == no);

        if (masa == null || masa.Durum == "Bos")
        {
            Console.WriteLine("Gecersiz masa!");
            return;
        }

        Console.Write("Urun Adi: ");
        string ad = Console.ReadLine() ?? "";

        double fiyat = OkuDouble("Birim Fiyat: ");
        int adet = OkuInt("Adet: ");

        Urun urun = new Urun(ad, fiyat);
        Siparis siparis = new Siparis(urun, adet);

        masa.SiparisEkle(siparis);
        tumSiparisler.Add(siparis);

        Console.WriteLine("Siparis eklendi.");
    }

    static void SiparisGoster()
    {
        int no = OkuInt("Masa No: ");
        var masa = masalar.Find(m => m.MasaNo == no);

        if (masa == null)
        {
            Console.WriteLine("Masa bulunamadi!");
            return;
        }

        if (masa.Siparisler.Count == 0)
        {
            Console.WriteLine("Siparis yok.");
            return;
        }

        foreach (var s in masa.Siparisler)
        {
            Console.WriteLine($"{s.Urun.Ad} - {s.Adet} adet - {s.ToplamFiyat()} TL");
        }
    }

    static void HesapKapat()
    {
        int no = OkuInt("Masa No: ");
        var masa = masalar.Find(m => m.MasaNo == no);

        if (masa == null)
        {
            Console.WriteLine("Masa bulunamadi!");
            return;
        }

        double toplam = masa.Hesapla();
        toplamCiro += toplam;

        Console.WriteLine($"Toplam Hesap: {toplam} TL");

        masa.Temizle();
    }

    static void GunSonu()
    {
        Console.WriteLine("\n--- GUN SONU RAPORU ---");
        Console.WriteLine($"Toplam Musteri: {toplamMusteri}");
        Console.WriteLine($"Toplam Ciro: {toplamCiro} TL");

        var enCok = tumSiparisler
            .GroupBy(s => s.Urun.Ad)
            .OrderByDescending(g => g.Sum(x => x.Adet))
            .FirstOrDefault();

        if (enCok != null)
        {
            Console.WriteLine($"En cok siparis edilen: {enCok.Key}");
        }
        else
        {
            Console.WriteLine("Henuz siparis yok.");
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}