using System.Collections.Generic;

namespace KafeYonetim.Models
{
    public class Masa
    {
        public int MasaNo { get; set; }
        public int Kapasite { get; set; }
        public string Durum { get; set; } // Bos, Dolu, Rezerve
        public List<Siparis> Siparisler { get; set; }

        public Masa(int masaNo, int kapasite)
        {
            MasaNo = masaNo;
            Kapasite = kapasite;
            Durum = "Bos";
            Siparisler = new List<Siparis>();
        }

        public void SiparisEkle(Siparis siparis)
        {
            Siparisler.Add(siparis);
        }

        public double Hesapla()
        {
            double toplam = 0;
            foreach (var siparis in Siparisler)
            {
                toplam += siparis.ToplamFiyat();
            }
            return toplam;
        }

        public void Temizle()
        {
            Siparisler.Clear();
            Durum = "Bos";
        }
    }
}