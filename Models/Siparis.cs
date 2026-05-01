namespace KafeYonetim.Models
{
    public class Siparis
    {
        public Urun Urun { get; set; }
        public int Adet { get; set; }

        public Siparis(Urun urun, int adet)
        {
            Urun = urun;
            Adet = adet;
        }

        public double ToplamFiyat()
        {
            return Urun.Fiyat * Adet;
        }
    }
}