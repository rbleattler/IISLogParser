This library allows the import and parsing of IIS log files from the filesystem to a List <IISLogEvent>. This project arose from the need to have a component that allows both the import of small log files as well as larger files (> 1Gb).

Build with NET Standard 2.0.3, can be used with .Net Framework or .Net Core

## Processing

The processing engine detects the size of the file to be processed and if it is less than 50 Mb it does a single read to memory and treats the data from there, for larger files to avoid OutOfMemory, reading is done line by line. 

## Properties

**FilePath**   [*string*] -> Path to the file

**MissingRecords**   [*bool*] -> Flag that indicates if there are any missing records. For larger files, this property can be flagged as true.  

**CurrentFileRecord**   [*int*] -> When processing large files this will store the currently file record index. For example, if the file has 1.000.000 log events and the processing is done in blocks of 250000(*MaxFileRecord2Read*), we'll have 4 cycles each on with this flag set with 250.000, 500.000, 750.000 and finally 1.000.000

**MaxFileRecord2Read**   [*int*] ->  Controls the maximum limit of items that the <IISLogEvent> List can have. If the number of events in the log file exceeds MaxFileRecord2Read the MissingRecords variable assumes the value of true and we can perform one more reading of a MaxFileRecord2Read set.
For files less than 50Mb this value has no effect because the engine performs a single read to memory and treats the data from there. 
For example, if the file has  1.000.000 log events and this is set to 250.000, will perform 4 cycle each one extracting a  List<IISLogEvent> with a count of 250.000


Usage : 

            List<IISLogEvent> logs = new List<IISLogEvent>();
            using (ParserEngine parser = new ParserEngine([filepath]))
            {
                while (parser.MissingRecords)
                {
                    logs = parser.ParseLog().ToList();
                }
            }
