using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DataMunger
{
    class ParsedDataContext
    {
        // this class provides common functionality between Weatherman.cs and FootballManager.cs
        // as well as any other classes which represent a data model for a fixed-width text file

        // row data read from the data file and chopped into fields based on the widths column
        protected List<string[]> rowData;
        // fixed column widths; instantiated with values in derived classes
        protected int[] widths;

        // this method has to be run before anything can be done with the class 
        protected void assignDataSetFromFile(string file)
        {
            rowData = DataParser.parseFixedWidthData(file, widths);
        }

        // prevents dumb exceptions that break the flow of the program due to data variance
        protected int SafelyConvertToInt32(string s)
        {
            Regex nonNumbersOnly = new Regex(@"\D+");
            s = nonNumbersOnly.Replace(s, "");
            int rv = int.MinValue;
            
            try
            {
                rv = Convert.ToInt32(s);
            }
            catch (FormatException e)
            { /* swallow these exceptions with a healthy dose of salt, because we know there may be incorrect data */ }
            return rv;
        }

        // returns the index of the row with the minimum difference between the two field values given (first value minus second value)
        // uses absolute value to find smallest possible difference between the two numbers on number line, so negative results do not receive priority
        protected int findMinDiff(int col1, int col2)
        {                     
            int result = rowData.FindIndex(x => Math.Abs(SafelyConvertToInt32(x[col1]) - SafelyConvertToInt32(x[col2])) ==
                rowData.Min(y => Math.Abs(SafelyConvertToInt32(y[col1]) - SafelyConvertToInt32(y[col2]))));
            return result;
        }
    }
}
