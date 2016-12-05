using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunger
{
    class Weatherman : ParsedDataContext
    {
        // Column indecies
        const short COL_DAYS = 0, COL_MXT = 1, COL_MNT = 2, COL_AVT = 3, COL_HDDAY = 4, COL_AVDP = 5,
            COL_1HRP = 6, COL_TPCPN = 7, COL_WXTYPE = 8, COL_PDIR = 9, COL_AVSP = 10, COL_DIR = 11, COL_MXS = 12,
            COL_SKYC = 13, COL_MXR = 14, COL_MNR = 15, COL_AVSLP = 16;
                            
        public Weatherman(string path)
        {
            rowData = new List<string[]>();
            // Column widths in fixed-width text data file
            widths = new int[] { 5, 6, 6, 6, 6, 6, 5, 6, 7, 5, 5, 4, 4, 5, 4, 3, 6 };
            assignDataSetFromFile(path);
        }
        
        public int findDayWithMinTemperatureDiff()
        {
            int rv = -1;

            var day = new List<string[]>(rowData.Where(y => rowData.IndexOf(y) == findMinDiff(COL_MXT, COL_MNT)));
            if(day.Count > 0)
                rv = SafelyConvertToInt32(day[0][COL_DAYS]);           

            return rv;
        }
    }
}
