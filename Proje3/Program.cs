using Proje3;

Yigin y = new Yigin();
foreach (string s in new[] { "bir", "iki", "üç", "dört", "beş" }) y.Doldur(s);
Console.WriteLine($"Bak: {y.Bak()}, ElemanSayisi: {y.ElemanSayisi}");
Console.WriteLine($"Cek: {y.Cek()}, {y.Cek()}");
Console.Write("Numerator: ");
foreach (object o in y) Console.Write(o + " ");
Console.WriteLine();
Console.WriteLine("Diziye: " + string.Join(", ", y.Diziye()));
y.Temizle();
Console.WriteLine($"Temizle sonrası ElemanSayisi: {y.ElemanSayisi}");
