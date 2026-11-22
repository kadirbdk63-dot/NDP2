using System.Collections;

namespace Proje1
{
    /// <summary>ArrayList benzeri, generik olmayan dinamik dizi.</summary>
    public class DiziListe : IEnumerable
    {
        private object[] dizi;
        private int elemanSayisi;

        public int ElemanSayisi => elemanSayisi;

        public DiziListe() : this(4) { }

        public DiziListe(int kapasite)
        {
            if (kapasite < 1) kapasite = 4;
            dizi = new object[kapasite];
        }

        private void KapasiteKontrol()
        {
            if (elemanSayisi == dizi.Length)
            {
                object[] yeni = new object[dizi.Length * 2];
                for (int i = 0; i < elemanSayisi; i++) yeni[i] = dizi[i];
                dizi = yeni;
            }
        }

        public int SonaEkle(object eleman)
        {
            KapasiteKontrol();
            dizi[elemanSayisi] = eleman;
            return elemanSayisi++;
        }

        public void ArayaEkle(int endeks, object eleman)
        {
            if (endeks < 0 || endeks > elemanSayisi)
                throw new ArgumentOutOfRangeException(nameof(endeks));
            KapasiteKontrol();
            for (int i = elemanSayisi; i > endeks; i--) dizi[i] = dizi[i - 1];
            dizi[endeks] = eleman;
            elemanSayisi++;
        }

        /// <summary>RemoveAt karşılığı: endekse göre siler.</summary>
        public void Sil(int endeks)
        {
            if (endeks < 0 || endeks >= elemanSayisi)
                throw new ArgumentOutOfRangeException(nameof(endeks));
            for (int i = endeks; i < elemanSayisi - 1; i++) dizi[i] = dizi[i + 1];
            dizi[--elemanSayisi] = null;
        }

        /// <summary>Remove karşılığı: ilk eşleşen elemanı siler. (int değer silmek için Sil((object)5) yazılmalıdır.)</summary>
        public bool Sil(object eleman)
        {
            int e = Bul(eleman);
            if (e < 0) return false;
            Sil(e);
            return true;
        }

        public bool VarMi(object eleman) => Bul(eleman) >= 0;

        public int Bul(object eleman)
        {
            for (int i = 0; i < elemanSayisi; i++)
                if (Equals(dizi[i], eleman)) return i;
            return -1;
        }

        public void Temizle()
        {
            for (int i = 0; i < elemanSayisi; i++) dizi[i] = null;
            elemanSayisi = 0;
        }

        /// <summary>Eklemeli sıralama (insertion sort). Elemanlar IComparable olmalıdır.</summary>
        public void Sirala()
        {
            for (int i = 1; i < elemanSayisi; i++)
            {
                object anahtar = dizi[i];
                int j = i - 1;
                while (j >= 0 && Karsilastir(dizi[j], anahtar) > 0)
                {
                    dizi[j + 1] = dizi[j];
                    j--;
                }
                dizi[j + 1] = anahtar;
            }
        }

        private static int Karsilastir(object a, object b)
        {
            if (a is IComparable ca) return ca.CompareTo(b);
            throw new InvalidOperationException("Elemanlar IComparable olmalıdır.");
        }

        public void TerCevir()
        {
            for (int i = 0, j = elemanSayisi - 1; i < j; i++, j--)
            {
                object gecici = dizi[i];
                dizi[i] = dizi[j];
                dizi[j] = gecici;
            }
        }

        public object this[int endeks]
        {
            get
            {
                if (endeks < 0 || endeks >= elemanSayisi) throw new ArgumentOutOfRangeException(nameof(endeks));
                return dizi[endeks];
            }
            set
            {
                if (endeks < 0 || endeks >= elemanSayisi) throw new ArgumentOutOfRangeException(nameof(endeks));
                dizi[endeks] = value;
            }
        }

        public IEnumerator GetEnumerator() => new Numerator(this);

        private class Numerator : IEnumerator
        {
            private readonly DiziListe liste;
            private int konum = -1;
            public Numerator(DiziListe liste) { this.liste = liste; }
            public object Current
            {
                get
                {
                    if (konum < 0 || konum >= liste.elemanSayisi) throw new InvalidOperationException();
                    return liste.dizi[konum];
                }
            }
            public bool MoveNext() => ++konum < liste.elemanSayisi;
            public void Reset() => konum = -1;
        }
    }
}
