using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunger
{
    class FootballManager : ParsedDataContext
    {        
        // Column indecies
        const short COL_INDEX = 0, COL_TEAM = 1, COL_PLAYERS = 2, COL_WINS = 3, COL_LOSSES = 4, COL_DRAWS = 5,
            COL_FOR = 6, COL_AGAINST = 7, COL_PTS = 8;
                     
        public FootballManager(string path)
        {
            rowData = new List<string[]>();
            // Column widths in fixed-width text data file      
            widths = new int[] { 7, 16, 6, 5, 4, 5, 7, 6, 3 };
            assignDataSetFromFile(path);
        }

        public int findTeamWithMinGoalDiff()
        {
            int rv = -1;

            var team = new List<string[]>(rowData.Where(y => rowData.IndexOf(y) == findMinDiff(COL_FOR, COL_AGAINST)));
            if (team.Count > 0)
                rv = SafelyConvertToInt32(team[0][COL_INDEX]);           

            return rv;
        }
    }
}
