using System.Collections;
using Proje5;

SiraliListe s = new SiraliListe();
s.Ekle(30, "otuz"); s.Ekle(10, "on"); s.Ekle(20, "yirmi"); s.Ekle(5, "beş"); s.Ekle(40, "kırk");
Yaz("Başlangıç", s);
Console.WriteLine($"s[20] = {s[20]}, HashUret(20) = {s.HashUret(20)}");
s.Sil((object)10); Yaz("Sil(anahtar 10)", s);
s.Sil(0); Yaz("Sil(endeks 0)", s);
Console.WriteLine("Anahtarlar: " + string.Join(", ", s.Anahtarlar));
Console.WriteLine("Degerler: " + string.Join(", ", s.Degerler));
s.Temizle();
Console.WriteLine($"Temizle sonrası ElemanSayisi: {s.ElemanSayisi}");

static void Yaz(string baslik, SiraliListe s)
{
    Console.Write(baslik + ": ");
    foreach (DictionaryEntry e in s) Console.Write($"[{e.Key}={e.Value}] ");
    Console.WriteLine();
}
