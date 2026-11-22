using System.Collections;

namespace Proje5
{
    /// <summary>Anahtara göre sıralı tutulan, generik olmayan anahtar-değer listesi.
    /// Anahtarlar IComparable olmalıdır. Arama için ikili arama (binary search) kullanılır.</summary>
    public class SiraliListe : IEnumerable
    {
        private object[] anahtarlar = new object[4];
        private object[] degerler = new object[4];
        private int elemanSayisi;

        public int ElemanSayisi => elemanSayisi;

        public object[] Anahtarlar
        {
            get { object[] a = new object[elemanSayisi]; Array.Copy(anahtarlar, a, elemanSayisi); return a; }
        }

        public object[] Degerler
        {
            get { object[] a = new object[elemanSayisi]; Array.Copy(degerler, a, elemanSayisi); return a; }
        }

        public int HashUret(object anahtar)
        {
            if (anahtar == null) throw new ArgumentNullException(nameof(anahtar));
            return anahtar.GetHashCode() & 0x7FFFFFFF;
        }

        private static int Karsilastir(object a, object b)
        {
            if (a is IComparable ca) return ca.CompareTo(b);
            throw new InvalidOperationException("Anahtarlar IComparable olmalıdır.");
        }

        /// <summary>Bulunursa endeksi, bulunamazsa eklenmesi gereken konumun tümleyenini (~konum) döndürür.</summary>
        private int IkiliArama(object anahtar)
        {
            int alt = 0, ust = elemanSayisi - 1;
            while (alt <= ust)
            {
                int orta = (alt + ust) / 2;
                int c = Karsilastir(anahtarlar[orta], anahtar);
                if (c == 0) return orta;
                if (c < 0) alt = orta + 1; else ust = orta - 1;
            }
            return ~alt;
        }

        public void Ekle(object anahtar, object deger)
        {
            if (anahtar == null) throw new ArgumentNullException(nameof(anahtar));
            int k = IkiliArama(anahtar);
            if (k >= 0) throw new ArgumentException("Bu anahtar zaten mevcut.");
            k = ~k;
            if (elemanSayisi == anahtarlar.Length)
            {
                Array.Resize(ref anahtarlar, elemanSayisi * 2);
                Array.Resize(ref degerler, elemanSayisi * 2);
            }
            for (int i = elemanSayisi; i > k; i--)
            {
                anahtarlar[i] = anahtarlar[i - 1];
                degerler[i] = degerler[i - 1];
            }
            anahtarlar[k] = anahtar;
            degerler[k] = deger;
            elemanSayisi++;
        }

        /// <summary>Remove karşılığı: anahtara göre siler.</summary>
        public bool Sil(object anahtar)
        {
            int k = IkiliArama(anahtar);
            if (k < 0) return false;
            Sil(k);
            return true;
        }

        /// <summary>RemoveAt karşılığı: endekse göre siler. (int anahtar silmek için Sil((object)5) yazılmalıdır.)</summary>
        public void Sil(int endeks)
        {
            if (endeks < 0 || endeks >= elemanSayisi) throw new ArgumentOutOfRangeException(nameof(endeks));
            for (int i = endeks; i < elemanSayisi - 1; i++)
            {
                anahtarlar[i] = anahtarlar[i + 1];
                degerler[i] = degerler[i + 1];
            }
            elemanSayisi--;
            anahtarlar[elemanSayisi] = null;
            degerler[elemanSayisi] = null;
        }

        public void Temizle()
        {
            anahtarlar = new object[4];
            degerler = new object[4];
            elemanSayisi = 0;
        }

        public object this[object anahtar]
        {
            get { int k = IkiliArama(anahtar); return k >= 0 ? degerler[k] : null; }
            set
            {
                int k = IkiliArama(anahtar);
                if (k >= 0) degerler[k] = value; else Ekle(anahtar, value);
            }
        }

        public IEnumerator GetEnumerator() => new Numerator(this);

        private class Numerator : IEnumerator
        {
            private readonly SiraliListe l;
            private int konum = -1;
            public Numerator(SiraliListe l) { this.l = l; }
            public object Current
            {
                get
                {
                    if (konum < 0 || konum >= l.elemanSayisi) throw new InvalidOperationException();
                    return new DictionaryEntry(l.anahtarlar[konum], l.degerler[konum]);
                }
            }
            public bool MoveNext() => ++konum < l.elemanSayisi;
            public void Reset() => konum = -1;
        }
    }
}
