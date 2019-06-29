using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implieds
{
    public static class LogHelper
    {
        public static void ReadLog(string filename)
        {
            //var fs = new FileStream(filename, FileMode.Open);
            var reader = new StreamReader(filename);

            DateTime startTime = DateTime.MinValue, endTime = DateTime.MinValue;
            Double startMs = double.MinValue, endMs = double.MinValue;
            double total = 0;
            int count = 0;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var split = line.Split(new char[] { ',' });
                if (split.Length == 2 && split[1].StartsWith("BEGIN"))
                {
                    /*string timeStr = line.Substring(0, 12);
                    startTime = DateTime.Parse(timeStr);*/
                    string msStr = line.Substring(14, 8);
                    startMs = double.Parse(msStr);
                }
                else if (split.Length == 2 && split[1].StartsWith("END"))
                {
                    /*string timeStr = line.Substring(0, 12);
                    endTime = DateTime.Parse(timeStr);
                    TimeSpan span = endTime.Subtract(startTime);
                    total += span.TotalMilliseconds;*/
                    
                    string msStr = line.Substring(14, 8);
                    endMs = double.Parse(msStr);
                    total += endMs - startMs;

                    ++count;
                }
            }

            Console.WriteLine("Average calculation time for {0} trials (ms): {1:F4}", count, total / count);
        }

    } // END OF CLASS LogHelper
} // END OF NAMESPACE
