using System;

namespace LicenseGeneratorWorkflow
{
    public class DataDumper
    {
         
        public static void Dump(string data)
        {
            using (var sw = System.IO.File.AppendText(@"c:\temp\postdata.txt"))
            {
                var formattedData = DateTime.Now.ToString("yyyy MMM dd hh:mm:ss") + " - " + data;
                sw.WriteLine(formattedData);
            }
        }
    }
}