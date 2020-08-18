using System;
using System.Collections.Generic;

namespace Abushakir_Net
{
    public class BahireHasab
    {
        private int _year;
        private readonly EthiopianCalendarResource _calendarResource = new EthiopianCalendarResource();

        public BahireHasab(int year)
        {
            if (year < 0)
                _year = new EthiopianDateTime().Year;
            else
                _year = year;
        }

        public long AmeteAlem
        {
            get { return _calendarResource.AmeteFida + _year; }
        }

        string getEvangelist(bool returnName = false)
        {
            int evangelist = (int)(AmeteAlem % 4);
            if (returnName)
            {
                return _calendarResource.EvangeLists[evangelist];
            }
            return evangelist.ToString();
        }

        public string GetMeskeremOne(bool returnName = false)
        {
            decimal val = this.AmeteAlem / 4;
            int rabeet = (int)Math.Floor(val);
            long result = (this.AmeteAlem + rabeet) % 7;
            if (returnName)
                return _calendarResource.WeekDays[(int)result];
            return result.ToString();
        }

        public long Wenber
        {
            get { return (this.AmeteAlem % 19) - 1 < 0 ? 0 : (this.AmeteAlem % 19) - 1; }
        }

        public long Abekte
        {
            get { return (this.Wenber * _calendarResource.TinteAbekte) % 30; }
        }

        public long Metkih
        {
            get
            {
                return this.Wenber == 0 ? 30 : (this.Wenber * _calendarResource.TinteMetkih) % 30;
            }
        }

        public int YebealeMetkihWer()
        {
            if (this.Metkih > 14)
            {
                return 1;
            }
            else return 2;
        }

        public (string month, int date) Nenewe()
        {
            var meskerem1 = this.GetMeskeremOne(true);
            var month = this.YebealeMetkihWer();
            long date;
            int dayTewsak = default;

            foreach (var el in _calendarResource.YeeletTewsak)
            {
                if (el.name == _calendarResource.WeekDays[(int)(_calendarResource.WeekDays.IndexOf(meskerem1) + this.Metkih - 1) % 7])
                    dayTewsak = el.val;
            }

            string monthName = dayTewsak + this.Metkih > 30 ? "የካቲት" : "ጥር";
            if (month == 2)
            {
                monthName = "የካቲት";
                string tikimt1 = _calendarResource.WeekDays[(_calendarResource.WeekDays.IndexOf(meskerem1) + 2) % 7];
                string metkihElet = _calendarResource.WeekDays[(int)(_calendarResource.WeekDays.IndexOf(tikimt1) + this.Metkih - 1) % 7];
                foreach (var al in _calendarResource.YeeletTewsak)
                {
                    if (al.name == _calendarResource.WeekDays[_calendarResource.WeekDays.IndexOf(metkihElet)]) dayTewsak = al.val;
                }
            }
            date = this.Metkih + dayTewsak;
            int d = date % 30 == 0 ? 30 : (int)(date % 30);
            return (monthName, d);
        }

        public List<(string beal, object day)> AllAtswamat
        {
            get
            {
                var mebajaHamer = this.Nenewe();
                List<(string beal, object day)> result = new List<(string beal, object day)>();
                foreach (var key in _calendarResource.YebealTewsaak.Keys)
                {
                    decimal val = mebajaHamer.date + _calendarResource.YebealTewsaak[key];
                    int index = (int)(_calendarResource.Months.IndexOf(mebajaHamer.month) + Math.Floor(val) / 30);
                    result.Add((key, new
                    {
                        month = _calendarResource.Months[index],
                        date = (mebajaHamer.date + _calendarResource.YebealTewsaak[key]) % 30 == 0 ? 30 : (mebajaHamer.date + _calendarResource.YebealTewsaak[key]) % 30
                    }));
                }
                return result;
            }
        }

        public bool IsMovableHoliday(string holidayName)
        {
            if (_calendarResource.YebealTewsaak.ContainsKey(holidayName))
            {
                return true;
            }
            else
                throw new Exception("FEASTNAME ERROR: Holiday or Feast is not a movable one. Please provide holidays between 'ነነዌ' and ጾመ 'ድህነት'");
        }

        object GetSingleBealOrTsom(string name)
        {
            bool status = this.IsMovableHoliday(name);
            if (status)
            {
                var mebajaHamer = this.Nenewe();
                var target = _calendarResource.YebealTewsaak[name];
                var a = new
                {
                    month = _calendarResource.Months[_calendarResource.Months.IndexOf(mebajaHamer.month) + ((mebajaHamer.date + target) / 30)],
                    date = (mebajaHamer.date + target) % 30 == 0 ? 30 : (mebajaHamer.date + target) % 30,
                };
                return a;
            }
            return null;
        }
    }

}
