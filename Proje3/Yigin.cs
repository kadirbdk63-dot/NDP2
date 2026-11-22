using System.Collections;

namespace Proje3
{
    /// <summary>Dizi tabanlı, generik olmayan yığın (LIFO).</summary>
    public class Yigin : IEnumerable
    {
        private object[] dizi = new object[4];
        private int sayi;

        public int ElemanSayisi => sayi;

        public void Doldur(object eleman)
        {
            if (sayi == dizi.Length)
            {
                object[] yeni = new object[dizi.Length * 2];
                for (int i = 0; i < sayi; i++) yeni[i] = dizi[i];
                dizi = yeni;
            }
            dizi[sayi++] = eleman;
        }

        public object Cek()
        {
            if (sayi == 0) throw new InvalidOperationException("Yığın boş.");
            object eleman = dizi[--sayi];
            dizi[sayi] = null;
            return eleman;
        }

        public object Bak()
        {
            if (sayi == 0) throw new InvalidOperationException("Yığın boş.");
            return dizi[sayi - 1];
        }

        public void Temizle()
        {
            dizi = new object[4];
            sayi = 0;
        }

        /// <summary>Üstten alta doğru (Stack.ToArray ile aynı sıra).</summary>
        public object[] Diziye()
        {
            object[] sonuc = new object[sayi];
            for (int i = 0; i < sayi; i++) sonuc[i] = dizi[sayi - 1 - i];
            return sonuc;
        }

        public IEnumerator GetEnumerator() => new Numerator(this);

        private class Numerator : IEnumerator
        {
            private readonly Yigin y;
            private int konum = -1;
            public Numerator(Yigin y) { this.y = y; }
            public object Current
            {
                get
                {
                    if (konum < 0 || konum >= y.sayi) throw new InvalidOperationException();
                    return y.dizi[y.sayi - 1 - konum];
                }
            }
            public bool MoveNext() => ++konum < y.sayi;
            public void Reset() => konum = -1;
        }
    }
}
