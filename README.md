# DataMunger
My solution to exercise 4 from Dave Thomas's infamous CodeKata exercises. This is an exercise in text data parsing and DRY design.
The program has two objectives. Analyze the weather data file (weather.dat) collected from Morristown, New Jersey and find the day with the smallest difference in temperature limits; and also, analyze the association football league file (football.dat) and find the team with the smallest difference in overall "for" and "against" goals for the season.

# Design & Hierarchy
The program accomplishes its objective using a data model class to represent the loaded object. The instructions for parsing and loading the data into said model are stored away in a static helper class. A super-class (which functions more as an abstract class would) defines common functionality for all data model classes. Since only fixed-width text data files are used, the static helper class is only designed to handle fixed-width files.

## DataParser
This is a static helper class which does little else except provide a place for the parseFixedWidthData method to reside. Other forms of text data could also be parsed here easily if the implementation was provided.
###  parseFixedWidthData(string file, int[] widths)
   For a given filename and a given set of field widths, this method will find and parse numerical data fields in your fixed-width text data file. It ignores any lines that do not have digits on them, due to only numerical data being analyzed in the scope of the exercise.

## ParsedDataContext
This is a super class which defines functionality common to all of the data models made (and to be made) within this application. All models which derive from this class are assumed to be models for fixed-width text data files.
###   protected int[] widths
This member stores each of the text field widths to be used on the text data file when reading data, in order from left to right. The first column's width in the data file will be in index 0, second width at index 1, and so on.
###   protected List<string[]> rowData
This member stores the parsed text data. Each string array holds the text fields from one line, split up according to the widths attribute.
###   protected void assignDataSetFromFile(string file)
All this method does is make a call to DataParser.parseFixedWidthData() using the "file" parameter and the widths attribute, and store the results in rowData. It is called in each derived constructor right after the widths array is instantiated.
###   protected int SafelyConvertToInt32(string s)
A wrapper for Convert.toInt32() which sanitizes the input string and swallows FormatExceptions (but only FormatExceptions) due to the somewhat unclean nature of raw text data files. 
###   protected int findMinDiff(int col1, int col2)
For two given column indecies, this method finds the index of the row which has the smallest (absolute value) difference between the column values. For example, if we received a data set of weather information per day, and col1 correlates to maximum temperature and col2 correlates to minimum temperature, then it would find the List index of the row in rowData with the smallest temperature difference.

## FootballManager
This is a class which derives from ParsedDataContext. It is designed to model fixed-width text data similar to that found in "football.dat." 
###   const short COL_INDEX, COL_TEAM, COL_PLAYERS, COL_WINS, COL_LOSSES, COL_DRAWS, COL_FOR, COL_AGAINST, COL_PTS
These constants indicate the index of the respectively named data column on the input file. 
###   public int findTeamWithMinGoalDiff()
Uses inherited method findMinDiff() to find the team with the smallest difference between "For" and "Against" goals for the season.

## Weatherman
This is a class which derives from ParsedDataContext. It is designed to model fixed-width text data similar to that found in "weather.dat."
###   const short COL_DAYS, COL_MXT, COL_MNT, COL_AVT, COL_HDDAY, COL_AVDP, COL_1HRP, COL_TPCPN, COL_WXTYPE, COL_PDIR, COL_AVSP, COL_DIR, COL_MXS, COL_SKYC, COL_MXR, COL_MNR, COL_AVSLP
These constants indicate the index of the respectively named data column on the input file. 
###   public int findDayWithMinTemperatureDiff()
Uses inherited method findMinDiff() to find the day with the smallest difference between minimum and maximum temperature values.
