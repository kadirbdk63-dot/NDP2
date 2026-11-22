using System.Collections;
using System.Collections.Generic;

namespace Proje6
{
    /// <summary>List&lt;T&gt; benzeri generik dinamik dizi.</summary>
    public class Liste<T> : IEnumerable<T>
    {
        private T[] dizi;
        private int elemanSayisi;

        public int ElemanSayisi => elemanSayisi;

        public Liste() : this(4) { }

        public Liste(int kapasite)
        {
            if (kapasite < 1) kapasite = 4;
            dizi = new T[kapasite];
        }

        private void KapasiteKontrol()
        {
            if (elemanSayisi == dizi.Length) Array.Resize(ref dizi, dizi.Length * 2);
        }

        /// <summary>Add karşılığı.</summary>
        public void Ekle(T eleman)
        {
            KapasiteKontrol();
            dizi[elemanSayisi++] = eleman;
        }

        /// <summary>Insert karşılığı.</summary>
        public void Ekle(int endeks, T eleman)
        {
            if (endeks < 0 || endeks > elemanSayisi) throw new ArgumentOutOfRangeException(nameof(endeks));
            KapasiteKontrol();
            for (int i = elemanSayisi; i > endeks; i--) dizi[i] = dizi[i - 1];
            dizi[endeks] = eleman;
            elemanSayisi++;
        }

        /// <summary>RemoveAt karşılığı.</summary>
        public void Sil(int endeks)
        {
            if (endeks < 0 || endeks >= elemanSayisi) throw new ArgumentOutOfRangeException(nameof(endeks));
            for (int i = endeks; i < elemanSayisi - 1; i++) dizi[i] = dizi[i + 1];
            dizi[--elemanSayisi] = default;
        }

        /// <summary>Remove karşılığı. (T=int iken değer silmek için Sil(5) endeks sanılır; bu durumda değer için Bul + Sil(endeks) kullanın.)</summary>
        public bool Sil(T eleman)
        {
            int e = Bul(eleman);
            if (e < 0) return false;
            Sil(e);
            return true;
        }

        public bool VarMi(T eleman) => Bul(eleman) >= 0;

        public int Bul(T eleman)
        {
            EqualityComparer<T> karsilastirici = EqualityComparer<T>.Default;
            for (int i = 0; i < elemanSayisi; i++)
                if (karsilastirici.Equals(dizi[i], eleman)) return i;
            return -1;
        }

        public void Temizle()
        {
            Array.Clear(dizi, 0, elemanSayisi);
            elemanSayisi = 0;
        }

        /// <summary>Eklemeli sıralama. Verilmezse Comparer&lt;T&gt;.Default kullanılır.</summary>
        public void Sirala(Comparison<T> karsilastir = null)
        {
            karsilastir ??= Comparer<T>.Default.Compare;
            for (int i = 1; i < elemanSayisi; i++)
            {
                T anahtar = dizi[i];
                int j = i - 1;
                while (j >= 0 && karsilastir(dizi[j], anahtar) > 0)
                {
                    dizi[j + 1] = dizi[j];
                    j--;
                }
                dizi[j + 1] = anahtar;
            }
        }

        public void TerCevir()
        {
            for (int i = 0, j = elemanSayisi - 1; i < j; i++, j--)
            {
                T gecici = dizi[i];
                dizi[i] = dizi[j];
                dizi[j] = gecici;
            }
        }

        public T this[int endeks]
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

        public IEnumerator<T> GetEnumerator() => new Numerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class Numerator : IEnumerator<T>
        {
            private readonly Liste<T> liste;
            private int konum = -1;
            public Numerator(Liste<T> liste) { this.liste = liste; }
            public T Current
            {
                get
                {
                    if (konum < 0 || konum >= liste.elemanSayisi) throw new InvalidOperationException();
                    return liste.dizi[konum];
                }
            }
            object IEnumerator.Current => Current;
            public bool MoveNext() => ++konum < liste.elemanSayisi;
            public void Reset() => konum = -1;
            public void Dispose() { }
        }
    }
}
