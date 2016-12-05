using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace DataMunger
{
    static class DataParser
    {
        // parses all data lines from a given text file into a list of records, with fields split up into individual strings for processing
        public static List<string[]> parseFixedWidthData(string file, int[] widths)
        {
            List<string[]> parsedRows = new List<string[]>();
            using (var reader = new StreamReader(file))
            {                          
                Regex oneOrMoreDigits = new Regex(@"\d+");

                string header;
                string[] values;
                header = reader.ReadLine();
                // this seems pretty inefficient (have to instantiate a new TextFieldParser and StringReader for each line in the file)
                // but hey, using ReadFields() directly on file input doesn't give me any opportunity to sanitize the input first...
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    // we only care about numerical data... for now
                    if (!oneOrMoreDigits.IsMatch(line))
                        continue;
                    // padding for "short" lines, usually this results from aggregate data lines at the end of a report                        
                    line = line.PadRight(header.Length);

                    using (var lineParser = new TextFieldParser(new StringReader(line)))
                    {
                        lineParser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.FixedWidth;
                        lineParser.TrimWhiteSpace = false;
                        if (widths.Length > 0)
                            lineParser.SetFieldWidths(widths);
                        else
                            throw new ArgumentException();

                        values = lineParser.ReadFields();                                                                        
                    }
                    if (values != null)
                    {
                        parsedRows.Add(values);
                    }
                }
            }

            return parsedRows;
        }
    }
}
