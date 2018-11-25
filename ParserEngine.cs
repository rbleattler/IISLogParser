using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace IISLogParser
{
    public class ParserEngine : IDisposable
    {
        public string FilePath { get; set; }
        public bool MissingRecords { get; private set; } = true;
        public int MaxFileRecord2Read { get; set; } = 1000000;
        public int CurrentFileRecord { get; private set; }
        private readonly StreamReader _logfile;
        private string[] _headerFields;
        Hashtable dataStruct = new Hashtable();
        private readonly int _mbSize;

        public ParserEngine(string filePath)
        {
            if(File.Exists(filePath))
            {
                FilePath = filePath;
                _logfile = new StreamReader(FilePath);
                _mbSize = (int)new FileInfo(filePath).Length / 1024 / 1024;
            }
            else
            {
                throw new Exception($"Could not find File {filePath}");
            }
        }

        public IEnumerable<IISLogEvent> ParseLog()
        {
            if (_mbSize < 50)
            {
                return QuickProcess();
            }
            else
            {
                return LongProcess();
            }           
        }

        private IEnumerable<IISLogEvent> QuickProcess()
        {
            List<IISLogEvent> events = new List<IISLogEvent>();
            var lines = File.ReadAllLines(FilePath);
            foreach (string line in lines)
            {
                ProcessLine(line, events);
            }
            MissingRecords = false;
            return events;
        }

        private IEnumerable<IISLogEvent> LongProcess()
        {
            string line;
            List<IISLogEvent> events = new List<IISLogEvent>();
            MissingRecords = false;
            while ((line = _logfile.ReadLine()) != null)
            {
                ProcessLine(line, events);
                if (events?.Count > 0 && events?.Count % MaxFileRecord2Read == 0)
                {
                    MissingRecords = true;
                    break;
                }
            }
            return events;
        }

        private void ProcessLine(string line, List<IISLogEvent> events)
        {
            if (line.StartsWith("#Fields:"))
            {
                _headerFields = line.Replace("#Fields: ", string.Empty).Split(' ');
            }
            if (!line.StartsWith("#") && _headerFields != null)
            {
                string[] fieldsData = line.Split(' ');
                FillDataStruct(fieldsData, _headerFields);
                events?.Add(NewEventObj());
                CurrentFileRecord++;
            }
        }

        private IISLogEvent NewEventObj()
        {
            return new IISLogEvent
            {
                DateTimeEvent = GetEventDateTime(),
                sSitename = dataStruct["s-sitename"]?.ToString(),
                sComputername = dataStruct["s-computername"]?.ToString(),
                sIp = dataStruct["s-ip"]?.ToString(),
                csMethod = dataStruct["cs-method"]?.ToString(),
                csUriStem = dataStruct["cs-uri-stem"]?.ToString(),
                csUriQuery = dataStruct["cs-uri-query"]?.ToString(),
                sPort = dataStruct["s-port"] != null ? int.Parse(dataStruct["s-port"]?.ToString()) : (int?)null,
                csUsername = dataStruct["cs-username"]?.ToString(),
                cIp = dataStruct["c-ip"]?.ToString(),
                csVersion = dataStruct["cs-version"]?.ToString(),
                csUserAgent = dataStruct["cs(User-Agent)"]?.ToString(),
                csCookie = dataStruct["cs(Cookie)"]?.ToString(),
                csReferer = dataStruct["cs(Referer)"]?.ToString(),
                csHost = dataStruct["cs-host"]?.ToString(),
                scStatus = dataStruct["sc-status"] != null ? int.Parse(dataStruct["sc-status"]?.ToString()) : (int?)null,
                scSubstatus = dataStruct["sc-substatus"] != null ? int.Parse(dataStruct["sc-substatus"]?.ToString()) : (int?)null,
                scWin32Status = dataStruct["sc-win32-status"] != null ? long.Parse(dataStruct["sc-win32-status"]?.ToString()) : (long?)null,
                scBytes = dataStruct["sc-bytes"] != null ? int.Parse(dataStruct["sc-bytes"]?.ToString()) : (int?)null,
                csBytes = dataStruct["cs-bytes"] != null ? int.Parse(dataStruct["cs-bytes"]?.ToString()) : (int?)null,
                timeTaken = dataStruct["time-taken"] != null ? int.Parse(dataStruct["time-taken"]?.ToString()) : (int?)null
            };
        }

        private DateTime GetEventDateTime()
        {
            DateTime finalDate = DateTime.Parse($"{dataStruct["date"]} {dataStruct["time"]}");
            return finalDate;
        }
        private void FillDataStruct(string[] fieldsData, string[] header)
        {
            dataStruct.Clear();            
            for (int i = 0; i < header.Length; i++)
            {
                dataStruct.Add(header[i], fieldsData[i] == "-" ? null : fieldsData[i]);
            }
        }
        public void Dispose()
        {
            _logfile?.Close();
        }
    }

}
