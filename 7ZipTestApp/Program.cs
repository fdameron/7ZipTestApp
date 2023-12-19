using SevenZip;
using System;
using System.Diagnostics;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;
using System.Text;
using Ionic;
using Ionic.Zip;

namespace _7ZipTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var workingPath = @"C:\Test\";
            //var fullArchivePath = @"C:\Test\Bosch_SparkPlugs_2021-07-26_Images.zip";
            //var fullArchivePath = @"C:\Test\Images_FelPro_Complete.zip";
            var fullArchivePath = @"C:\Test\Extract64.zip";
            //var fullArchivePath = @"C:\Test\MERITOR_IMAGES 4.27.2022.zip";
            //var archList = FileUpload.ListArchive(workingPath, fullArchivePath, recursive: true);
            //Console.WriteLine(fullArchivePath);
            //Console.WriteLine(archList.Count);
            //foreach (var arch in archList)
            //{
            //    Console.WriteLine(arch);
            //}
            //ExtractArchive(fullArchivePath, @"C:\Test\Extract");
            WriteArchive(@"C:\Test\Compress.zip", @"C:\Test\Compress");
            Console.ReadKey();
            System.IO.File.Delete(@"C:\Test\Compress.zip");
            Console.WriteLine("Zip File Deleted");
            Console.ReadKey();
        }

        static private void ExtractArchive(string archivePath, string extractPath)
        {
            Console.WriteLine(Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, "x64"));
            SevenZipBase.SetLibraryPath("x64\\7z.dll");
            var myEx = new SevenZipExtractor(archivePath);
            myEx.ExtractArchive(extractPath);
        }

        static private void WriteArchive(string archivePath, string compressPath)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            //SevenZipBase.SetLibraryPath("x86\\7z.dll");
            //var myCom = new SevenZipCompressor();
            //myCom.CompressionLevel = CompressionLevel.Fast;
            //myCom.CompressionMethod = CompressionMethod.Deflate64;
            //myCom.ArchiveFormat = OutArchiveFormat.Zip;
            //myCom.CompressDirectory(compressPath, archivePath);
            
            //var parms = $"a \"{archivePath}\" -y -mm=Deflate64 \"{compressPath}\"";
            //Console.WriteLine("parms:" + parms);
            //sevenZip(parms, @"C:\Program Files\7-Zip");
            //a -r -mx3 foo.7z "src/scons/*.py

            using(ZipFile zip = new ZipFile()) 
            {
                //zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                //zip.CompressionMethod = Ionic.Zip.CompressionMethod.BZip2;
                zip.UseZip64WhenSaving = Zip64Option.AsNecessary;
                zip.AddDirectory(compressPath);
                zip.Save(archivePath);
            }
            
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.Elapsed.Minutes} & {watch.Elapsed.Seconds}");
        }

        static public void sevenZip(string parms, string workingDirectory)
        {
            using(var proc = new Process())
            { 
                //0 No error
                //1 Warning (Non fatal error(s)). For example, one Or more files were locked by some other application, so they were Not compressed.
                //2 Fatal error
                //7 Command line error
                //8 Not enough memory for operation
                //255 User stopped the process

                ProcessStartInfo psi = new ProcessStartInfo();
                var myDir = AppDomain.CurrentDomain.BaseDirectory;
                psi.FileName = workingDirectory + "\\7z.exe";

                //2
                //Use 7 - zip
                //specify a = archive And -tgzip = gzip
                //And then target file in quotes followed by source file in quotes

                psi.Arguments = parms;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.UseShellExecute = false;
                psi.RedirectStandardError = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                psi.WorkingDirectory = workingDirectory;
                proc.StartInfo = psi;

                //3.
                //Start Process And wait for it to exit

                proc.Start();

                proc.WaitForExit();
            }
        }
    }
}
