using System.Collections.Generic;

namespace Abushakir_Net
{
    public class EthiopianCalendarResource
    {
        private long unixEpoch = 719163;
        private long ethiopicEpoch = 2796;
        private long _dayMilliSec = 86400000;
        private long _hourMilliSec = 3600000;
        private long _minMilliSec = 60000;
        private long _secMilliSec = 1000;
        private long _maxMillisecondsSinceEpoch = 8640000000000000;

        private List<string> _evangelists = new List<string> { "ዮሐንስ", "ማቴዎስ", "ማርቆስ", "ሉቃስ" };

        private int _ameteFida = 5500;

        private int _tinteAbekte = 11;
        private int _tinteMetkih = 19;

        private readonly List<string> months = new List<string> {
          "መስከረም",
          "ጥቅምት",
          "ኅዳር",
          "ታኅሳስ",
          "ጥር",
          "የካቲት",
          "መጋቢት",
          "ሚያዝያ",
          "ግንቦት",
          "ሰኔ",
          "ኃምሌ",
          "ነሐሴ",
          "ጷጉሜን",
        };

        private readonly List<string> dayNumbers = new List<string> {
          "፩",
          "፪",
          "፫",
          "፬",
          "፭",
          "፮",
          "፯",
          "፰",
          "፱",
          "፲",
          "፲፩",
          "፲፪",
          "፲፫",
          "፲፬",
          "፲፭",
          "፲፮",
          "፲፯",
          "፲፰",
          "፲፱",
          "፳",
          "፳፩",
          "፳፪",
          "፳፫",
          "፳፬",
          "፳፭",
          "፳፮",
          "፳፯",
          "፳፰",
          "፳፱",
          "፴",
        };

        private Dictionary<string, int> yebealTewsak = new Dictionary<string, int>
        {
            ["ነነዌ"] = 0,
            ["ዓቢይ ጾም"] = 14,
            ["ደብረ ዘይት"] = 41,
            ["ሆሣዕና"] = 62,
            ["ስቅለት"] = 67,
            ["ትንሳኤ"] = 69,
            ["ርክበ ካህናት"] = 93,
            ["ዕርገት"] = 108,
            ["ጰራቅሊጦስ"] = 118,
            ["ጾመ ሐዋርያት"] = 119,
            ["ጾመ ድህነት"] = 121,
        };
        private List<(string name, int val)> yeeletTewsak = new List<(string name, int val)>
        {
            ( "አርብ", 2),
            ( "ሐሙስ",  3 ),
            ( "ረቡዕ",  4 ),
            ( "ማግሰኞ", 5 ),
            (  "ሰኞ", 6 ),
            ( "እሁድ", 7 ),
            ( "ቅዳሜ",8 ),
        };
        private readonly List<string> weekdays = new List<string> { "ሰኞ", "ማግሰኞ", "ረቡዕ", "ሐሙስ", "አርብ", "ቅዳሜ", "እሁድ" };

        public long UnixEpoch { get { return unixEpoch; } }
        public long EthiopicEpoch { get { return ethiopicEpoch; } }
        public List<string> Months { get { return months; } }
        public List<string> DayNumbers { get { return dayNumbers; } }
        public List<string> WeekDays { get { return weekdays; } }
        public long HourMilliseeconds { get { return _hourMilliSec; } }
        public long MinMillisecond { get { return _minMilliSec; } }
        public long SecMilliSec { get { return _secMilliSec; } }
        public long DayMilliSec { get { return _dayMilliSec; } }
        public long MaxMillisecondsSinceEpoch { get { return _maxMillisecondsSinceEpoch; } }
        public List<string> EvangeLists { get { return _evangelists; } }
        public long AmeteFida { get { return _ameteFida; } }
        public long TinteAbekte { get { return _tinteAbekte; } }
        public long TinteMetkih { get { return _tinteMetkih; } }
        public Dictionary<string, int> YebealTewsaak { get { return yebealTewsak; } }
        public List<(string name, int val)> YeeletTewsak { get { return yeeletTewsak; } }
    }
}
