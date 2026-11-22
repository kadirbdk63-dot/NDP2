using Proje2;

Kuyruk k = new Kuyruk();
for (int i = 1; i <= 6; i++) k.KuyrugaGir(i * 10);
Console.WriteLine($"Bak: {k.Bak()}, ElemanSayisi: {k.ElemanSayisi}");
Console.WriteLine($"KuyruktanCik: {k.KuyruktanCik()}, {k.KuyruktanCik()}");
k.KuyrugaGir(70); k.KuyrugaGir(80);
Console.Write("Numerator: ");
foreach (object o in k) Console.Write(o + " ");
Console.WriteLine();
Console.WriteLine("Diziye: " + string.Join(", ", k.Diziye()));
k.Temizle();
Console.WriteLine($"Temizle sonrası ElemanSayisi: {k.ElemanSayisi}");
