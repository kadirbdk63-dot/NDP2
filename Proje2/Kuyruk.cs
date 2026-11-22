using System.Collections;

namespace Proje2
{
    /// <summary>Dairesel dizi tabanlı, generik olmayan kuyruk (FIFO).</summary>
    public class Kuyruk : IEnumerable
    {
        private object[] dizi = new object[4];
        private int bas;      // ilk eleman
        private int sayi;

        public int ElemanSayisi => sayi;

        public void KuyrugaGir(object eleman)
        {
            if (sayi == dizi.Length)
            {
                object[] yeni = new object[dizi.Length * 2];
                for (int i = 0; i < sayi; i++) yeni[i] = dizi[(bas + i) % dizi.Length];
                dizi = yeni;
                bas = 0;
            }
            dizi[(bas + sayi) % dizi.Length] = eleman;
            sayi++;
        }

        public object KuyruktanCik()
        {
            if (sayi == 0) throw new InvalidOperationException("Kuyruk boş.");
            object eleman = dizi[bas];
            dizi[bas] = null;
            bas = (bas + 1) % dizi.Length;
            sayi--;
            return eleman;
        }

        public object Bak()
        {
            if (sayi == 0) throw new InvalidOperationException("Kuyruk boş.");
            return dizi[bas];
        }

        public void Temizle()
        {
            dizi = new object[4];
            bas = 0;
            sayi = 0;
        }

        public object[] Diziye()
        {
            object[] sonuc = new object[sayi];
            for (int i = 0; i < sayi; i++) sonuc[i] = dizi[(bas + i) % dizi.Length];
            return sonuc;
        }

        public IEnumerator GetEnumerator() => new Numerator(this);

        private class Numerator : IEnumerator
        {
            private readonly Kuyruk k;
            private int konum = -1;
            public Numerator(Kuyruk k) { this.k = k; }
            public object Current
            {
                get
                {
                    if (konum < 0 || konum >= k.sayi) throw new InvalidOperationException();
                    return k.dizi[(k.bas + konum) % k.dizi.Length];
                }
            }
            public bool MoveNext() => ++konum < k.sayi;
            public void Reset() => konum = -1;
        }
    }
}
