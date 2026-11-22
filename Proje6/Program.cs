using Proje6;

Liste<string> isimler = new Liste<string>();
isimler.Ekle("Zeynep"); isimler.Ekle("Ali"); isimler.Ekle("Mehmet");
isimler.Ekle(1, "Ayşe");
Yaz("Başlangıç", isimler);
Console.WriteLine($"VarMi(\"Ali\"): {isimler.VarMi("Ali")}, Bul(\"Mehmet\"): {isimler.Bul("Mehmet")}");
isimler.Sirala(); Yaz("Sirala", isimler);
isimler.TerCevir(); Yaz("TerCevir", isimler);
isimler.Sil("Ali"); Yaz("Sil(eleman)", isimler);
isimler.Sil(0); Yaz("Sil(endeks)", isimler);

Liste<double> sayilar = new Liste<double>();
sayilar.Ekle(3.5); sayilar.Ekle(1.25); sayilar.Ekle(2.0);
sayilar.Sirala((a, b) => b.CompareTo(a)); // azalan
Yaz("double azalan", sayilar);
sayilar.Temizle();
Console.WriteLine($"Temizle sonrası ElemanSayisi: {sayilar.ElemanSayisi}");

static void Yaz<T>(string baslik, Liste<T> l)
{
    Console.Write(baslik + ": ");
    foreach (T o in l) Console.Write(o + " ");
    Console.WriteLine($"(ElemanSayisi={l.ElemanSayisi})");
}
