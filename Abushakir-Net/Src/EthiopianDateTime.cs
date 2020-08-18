using System;

namespace Abushakir_Net
{
    public class EthiopianDateTime
    {
        private long moment;
        private long @fixed;
        private readonly EthiopianCalendarResource _calendarResource = new EthiopianCalendarResource();
        private readonly DateTime StartDate = new DateTime(1970, 1, 1);
        public EthiopianDateTime(int year, int month, int date, int hour, int minute, int second, int millisecond)
        {
            @fixed = fixedFromEthiopic(year, month, date);
            moment = dateToEpoch(year, month, date, hour, minute, second, millisecond);
        }
        public EthiopianDateTime(int year, int month, int date) : this(year, month, date, 0, 0, 0, 0) { }
        public EthiopianDateTime(long value)
        {
            if (Math.Abs(value) > 9999)
                fromMillisecondsSinceEpoch(value);
            else
            {
                @fixed = fixedFromEthiopic((int)value, 1, 1);
                moment = dateToEpoch((int)value, 1, 1, 0, 0, 0, 0);
            }
        }
        public EthiopianDateTime()
        {
            long ellpsedMilliSeconds = (long)DateTime.Now.Subtract(StartDate).TotalMilliseconds;
            @fixed = fixedFromUnix(ellpsedMilliSeconds);
            moment = ellpsedMilliSeconds;
        }
        public EthiopianDateTime(DateTime dateTime) : this((long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds) { }
        #region Private Methods
        private long fixedFromEthiopic(int year, int month, int day)
        {
            decimal value = _calendarResource.EthiopicEpoch - 1 + 365 * (year - 1) + year / 4 + 30 * (month - 1) + day;
            return (long)Math.Floor(value);
        }
        private long fixedFromUnix(decimal ms)
        {
            return _calendarResource.UnixEpoch + (long)Math.Floor(ms / 86400000);
        }
        private long dateToEpoch(int year, int month, int date, int hour, int minute, int second, int millisecond)
        {
            return (
              (this.fixedFromEthiopic(year, month, date) - _calendarResource.UnixEpoch) * _calendarResource.DayMilliSec +
              hour * _calendarResource.HourMilliseeconds +
              minute * _calendarResource.MinMillisecond +
              second * _calendarResource.SecMilliSec +
              millisecond
            );
        }
        private decimal _yearFirstDay()
        {
            decimal ameteAlem = _calendarResource.AmeteFida + this.Year;
            decimal rabeet = Math.Floor(ameteAlem / 4);
            return (ameteAlem + rabeet) % 7;
        }
        private string fourDigits(decimal n)
        {
            decimal absN = Math.Abs(n);
            string sign = n < 0 ? "-" : string.Empty;
            if (absN >= 1000)
                return $"{n}";
            if (absN >= 100)
                return $"{sign}0{ absN}";
            if (absN >= 10)
                return $"{ sign}00{ absN}";
            return $"{ sign} 000{ absN}";
        }
        private string sixDigits(decimal n)
        {
            if (n < -9999 || n > 9999)
                throw new Exception("Year out of scope");
            decimal absN = Math.Abs(n);
            string sign = n < 0 ? "-" : "+";
            if (absN >= 100000)
                return $"{sign}{absN}";
            return $"{ sign}0{ absN}";
        }
        private string threeDigits(decimal n)
        {
            if (n >= 100)
                return $"{n}";
            if (n >= 10)
                return $"0{n}";
            return $"00{ n}";
        }
        private string twoDigits(decimal n)
        {
            if (n >= 10) return $"{n}";
            return $"0{n}";
        }
        private void fromMillisecondsSinceEpoch(long millisecondsSinceEpoch)
        {
            this.moment = millisecondsSinceEpoch;
            @fixed = (long)fixedFromUnix(millisecondsSinceEpoch);
            if (
              Math.Abs(millisecondsSinceEpoch) > _calendarResource.MaxMillisecondsSinceEpoch ||
              Math.Abs(millisecondsSinceEpoch) == _calendarResource.MaxMillisecondsSinceEpoch
            )
                throw new Exception($"Calendar out side valid range { _calendarResource.MaxMillisecondsSinceEpoch }");
        }
        #endregion
        #region Operator overloading
        public override bool Equals(object obj) => this is EthiopianDateTime && this == (EthiopianDateTime)obj;
        public override int GetHashCode() => base.GetHashCode();
        public static bool operator ==(EthiopianDateTime dateTime1, EthiopianDateTime dateTime2) => dateTime1.@fixed == dateTime2.@fixed && dateTime1.moment == dateTime2.moment;
        public static bool operator !=(EthiopianDateTime dateTime1, EthiopianDateTime dateTime2) => !(dateTime1 == dateTime2);
        public static EthiopianDateTime operator +(EthiopianDateTime dateTime1, EthiopianDateTime dateTime2) => new EthiopianDateTime(dateTime1.moment + dateTime2.moment);
        public static EthiopianDateTime operator +(EthiopianDateTime dateTime1, Duration duration) => new EthiopianDateTime(dateTime1.moment + duration.InMilliseconds());
        public static EthiopianDateTime operator -(EthiopianDateTime dateTime, Duration duration) => new EthiopianDateTime(dateTime.moment - duration.InMilliseconds());
        #endregion

        public static EthiopianDateTime Now { get { return new EthiopianDateTime((long)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds); } }

        public int Year
        {
            get
            {
                decimal val = (4 * (@fixed - _calendarResource.EthiopicEpoch) + 1463);
                return (int)(Math.Floor(val) / 1461);
            }
        }
        public int Month
        {
            get
            {
                decimal val = (@fixed - fixedFromEthiopic(this.Year, 1, 1)) / 30;
                return (int)(Math.Floor(val) + 1);
            }
        }
        public string MonthGeezName
        {
            get { return _calendarResource.Months[(this.Month - 1) % 13]; }
        }
        public int Day
        {
            get
            {

                return (int)(@fixed + 1 - fixedFromEthiopic(this.Year, this.Month, 1));
            }
        }
        public string DayGeez
        {
            get { return _calendarResource.DayNumbers[(this.Day - 1) % 30]; }
        }
        public int Hour
        {
            get
            {
                decimal val = this.moment / _calendarResource.HourMilliseeconds;
                return (int)Math.Floor(val) % 24;
            }
        }
        public decimal Minute
        {
            get
            {
                decimal val = this.moment / _calendarResource.MinMillisecond;
                return Math.Floor(val) % 60;
            }
        }
        public decimal Second
        {
            get
            {
                decimal val = (this.moment / _calendarResource.SecMilliSec);
                return Math.Floor(val % 60);
            }
        }
        public bool IsLeap { get { return this.Year % 4 == 3; } }
        public decimal Millisecond { get { return this.moment % 1000; } }
        decimal YearFirstDay { get { return this._yearFirstDay(); } }
        public decimal WeekDay { get { return (this.YearFirstDay + (this.Month - 1) * 2) % 7; } }
        (int year, int month, int day) Date { get { return (Year, Month, Day); } }
        (int Hour, decimal Minute, decimal Second) Time { get { return (Hour, Minute, Second); } }
        public override string ToString()
        {
            string y = this.fourDigits(this.Year);
            string m = this.twoDigits(this.Month);
            string d = this.twoDigits(this.Day);
            string h = this.twoDigits(this.Hour);
            string min = this.twoDigits(this.Minute);
            string sec = this.twoDigits(this.Second);
            string ms = this.threeDigits(this.Millisecond);
            return $"{y}-{m}-{ d} { h}:{ min}:{ sec}.{ ms}";
        }
        public string ToJsonString()
        {
            return System.Text.Json.JsonSerializer.Serialize(new
            {
                year = this.fourDigits(this.Year),
                month = this.twoDigits(this.Month),
                date = this.twoDigits(this.Day),
                hour = this.twoDigits(this.Hour),
                min = this.twoDigits(this.Minute),
                sec = this.twoDigits(this.Second),
                ms = this.threeDigits(this.Millisecond),
            });
        }

        public string ToIso8601String()
        {
            string y = this.Year >= -9999 && this.Year <= 9999 ? this.fourDigits(this.Year) : this.sixDigits(this.Year);
            string m = this.twoDigits(this.Month);
            string d = this.twoDigits(this.Day);
            string h = this.twoDigits(this.Hour);
            string min = this.twoDigits(this.Minute);
            string sec = this.twoDigits(this.Second);
            string ms = this.threeDigits(this.Millisecond);
            return $"{y}-{m}-{ d}T{ h}:{ min}:{ sec}.{ ms}";
        }
        public bool IsBefore(EthiopianDateTime other)
        {
            return @fixed < other.@fixed || this.moment < other.moment;
        }
        public bool IsAfter(EthiopianDateTime other)
        {
            return @fixed > other.@fixed || this.moment > other.moment;
        }
        public int CompareTo(EthiopianDateTime other)
        {
            if (this.IsBefore(other)) return -1;
            else if (this.Equals(other)) return 0;
            else return 1;
        }

        public Duration Difference(EthiopianDateTime other)
        {
            long value = @fixed - other.@fixed;
            return new Duration((int)Math.Abs(value), 0, 0, 0, 0, 0);
        }
        public DateTime ToDateTime() => StartDate.AddMilliseconds(moment);

    }
}
