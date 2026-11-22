using System.Collections;

namespace Proje4
{
    /// <summary>Zincirleme (separate chaining) yöntemli, generik olmayan hash tablosu.</summary>
    public class HashTablo : IEnumerable
    {
        private class Dugum
        {
            public object Anahtar;
            public object Deger;
            public Dugum Sonraki;
        }

        private Dugum[] kovalar = new Dugum[8];
        private int elemanSayisi;

        public int ElemanSayisi => elemanSayisi;

        public object[] Anahtarlar
        {
            get
            {
                object[] a = new object[elemanSayisi];
                int n = 0;
                foreach (Dugum kova in kovalar)
                    for (Dugum d = kova; d != null; d = d.Sonraki) a[n++] = d.Anahtar;
                return a;
            }
        }

        public object[] Degerler
        {
            get
            {
                object[] a = new object[elemanSayisi];
                int n = 0;
                foreach (Dugum kova in kovalar)
                    for (Dugum d = kova; d != null; d = d.Sonraki) a[n++] = d.Deger;
                return a;
            }
        }

        /// <summary>Anahtardan kova numarası üretir.</summary>
        public int HashUret(object anahtar) => HashUret(anahtar, kovalar.Length);

        private static int HashUret(object anahtar, int kovaSayisi)
        {
            if (anahtar == null) throw new ArgumentNullException(nameof(anahtar));
            return (anahtar.GetHashCode() & 0x7FFFFFFF) % kovaSayisi;
        }

        private Dugum BulDugum(object anahtar)
        {
            for (Dugum d = kovalar[HashUret(anahtar)]; d != null; d = d.Sonraki)
                if (Equals(d.Anahtar, anahtar)) return d;
            return null;
        }

        public void Ekle(object anahtar, object deger)
        {
            if (BulDugum(anahtar) != null)
                throw new ArgumentException("Bu anahtar zaten mevcut.");
            if (elemanSayisi + 1 > kovalar.Length * 3 / 4) Buyut();
            int k = HashUret(anahtar);
            kovalar[k] = new Dugum { Anahtar = anahtar, Deger = deger, Sonraki = kovalar[k] };
            elemanSayisi++;
        }

        public bool Sil(object anahtar)
        {
            int k = HashUret(anahtar);
            Dugum onceki = null;
            for (Dugum d = kovalar[k]; d != null; onceki = d, d = d.Sonraki)
            {
                if (Equals(d.Anahtar, anahtar))
                {
                    if (onceki == null) kovalar[k] = d.Sonraki; else onceki.Sonraki = d.Sonraki;
                    elemanSayisi--;
                    return true;
                }
            }
            return false;
        }

        public bool AnahtarVarMi(object anahtar) => BulDugum(anahtar) != null;

        public void Temizle()
        {
            kovalar = new Dugum[8];
            elemanSayisi = 0;
        }

        public object this[object anahtar]
        {
            get => BulDugum(anahtar)?.Deger;
            set
            {
                Dugum d = BulDugum(anahtar);
                if (d != null) d.Deger = value; else Ekle(anahtar, value);
            }
        }

        private void Buyut()
        {
            Dugum[] yeni = new Dugum[kovalar.Length * 2];
            foreach (Dugum kova in kovalar)
            {
                for (Dugum d = kova; d != null;)
                {
                    Dugum sonraki = d.Sonraki;
                    int k = HashUret(d.Anahtar, yeni.Length);
                    d.Sonraki = yeni[k];
                    yeni[k] = d;
                    d = sonraki;
                }
            }
            kovalar = yeni;
        }

        private DictionaryEntry[] Kopya()
        {
            DictionaryEntry[] a = new DictionaryEntry[elemanSayisi];
            int n = 0;
            foreach (Dugum kova in kovalar)
                for (Dugum d = kova; d != null; d = d.Sonraki) a[n++] = new DictionaryEntry(d.Anahtar, d.Deger);
            return a;
        }

        public IEnumerator GetEnumerator() => new Numerator(Kopya());

        private class Numerator : IEnumerator
        {
            private readonly DictionaryEntry[] girdiler;
            private int konum = -1;
            public Numerator(DictionaryEntry[] girdiler) { this.girdiler = girdiler; }
            public object Current
            {
                get
                {
                    if (konum < 0 || konum >= girdiler.Length) throw new InvalidOperationException();
                    return girdiler[konum];
                }
            }
            public bool MoveNext() => ++konum < girdiler.Length;
            public void Reset() => konum = -1;
        }
    }
}
