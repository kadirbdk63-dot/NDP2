using Proje1;

DiziListe l = new DiziListe();
l.SonaEkle(5); l.SonaEkle(2); l.SonaEkle(9); l.SonaEkle(1);
l.ArayaEkle(1, 7);
Yaz("Başlangıç", l);
Console.WriteLine($"VarMi(9): {l.VarMi(9)}, Bul(9): {l.Bul(9)}");
l.Sirala(); Yaz("Sirala", l);
l.TerCevir(); Yaz("TerCevir", l);
l.Sil(0); Yaz("Sil(endeks 0)", l);
l.Sil((object)2); Yaz("Sil(eleman 2)", l);
l[0] = 100; Yaz("Endeksleyici l[0]=100", l);
l.Temizle(); Console.WriteLine($"Temizle sonrası ElemanSayisi: {l.ElemanSayisi}");

static void Yaz(string baslik, DiziListe l)
{
    Console.Write(baslik + ": ");
    foreach (object o in l) Console.Write(o + " ");
    Console.WriteLine($"(ElemanSayisi={l.ElemanSayisi})");
}
