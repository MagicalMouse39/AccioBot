using FFMediaToolkit;
using FFMediaToolkit.Decoding;
using System.Diagnostics;

Console.WriteLine(Environment.CurrentDirectory);

var file = MediaFile.Open(@"info.sdp");
while (file.Video.TryGetNextFrame(out var imageData))
{
    File.WriteAllBytes("test.bmp", imageData.Data.ToArray());
    new Process() { StartInfo = new ProcessStartInfo() { FileName = "test.bmp" } }.Start();
    Console.ReadKey();
}