using System.Collections;
using Proje4;

HashTablo h = new HashTablo();
h.Ekle("TR", "Türkiye");
h.Ekle("DE", "Almanya");
h.Ekle("FR", "Fransa");
for (int i = 0; i < 10; i++) h.Ekle("K" + i, i); // büyütmeyi test eder
Console.WriteLine($"ElemanSayisi: {h.ElemanSayisi}, HashUret(\"TR\") = {h.HashUret("TR")}");
Console.WriteLine("h[\"DE\"] = " + h["DE"]);
Console.WriteLine("Sil(\"FR\"): " + h.Sil("FR") + ", Sil(\"XX\"): " + h.Sil("XX"));
Console.WriteLine("Anahtarlar: " + string.Join(", ", h.Anahtarlar));
Console.WriteLine("Degerler: " + string.Join(", ", h.Degerler));
foreach (DictionaryEntry e in h) Console.Write($"[{e.Key}={e.Value}] ");
Console.WriteLine();
h.Temizle();
Console.WriteLine($"Temizle sonrası ElemanSayisi: {h.ElemanSayisi}");
