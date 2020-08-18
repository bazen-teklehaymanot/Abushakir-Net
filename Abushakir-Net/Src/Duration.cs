using System;

namespace Abushakir_Net
{
    public class Duration
    {
        private long microsecondsPerMillisecond = 1;
        private long millisecondsPerSecond = 1000;
        private long secondsPerMinute = 60;
        private long minutesPerHour = 60;
        private long hoursPerDay = 24;

        private long microsecondsPerSecond;
        private long microsecondsPerMinute;
        private long microsecondsPerHour;
        private long microsecondsPerDay;

        private long millisecondsPerMinute;
        private long millisecondsPerHour;
        private long millisecondsPerDay;

        private long secondsPerHour;
        private long secondsPerDay;

        private long minutesPerDay;

        static long _duration;

        public Duration()
        {
            microsecondsPerSecond = microsecondsPerMillisecond * millisecondsPerSecond;
            microsecondsPerMinute = microsecondsPerSecond * secondsPerMinute;
            microsecondsPerHour = this.microsecondsPerMinute * this.minutesPerHour;
            microsecondsPerDay = this.microsecondsPerHour * this.hoursPerDay;
            millisecondsPerMinute = this.millisecondsPerSecond * this.secondsPerMinute;
            millisecondsPerHour = this.millisecondsPerMinute * this.minutesPerHour;
            millisecondsPerDay = this.millisecondsPerHour * this.hoursPerDay;
            secondsPerHour = this.secondsPerMinute * this.minutesPerHour;
            secondsPerDay = this.secondsPerHour * this.hoursPerDay;
            minutesPerDay = this.minutesPerHour * this.hoursPerDay;
        }
        public Duration(long milliSeconds) : this()
        {
            SetMicroseconds(milliSeconds);
        }
        public Duration(params int[] args) : this()
        {
            if (args.Length > 0 && args.Length < 7)
            {
                this.SetMicroseconds(
                  this.microsecondsPerDay * args[0] +
                    this.microsecondsPerHour * args[1] +
                    this.microsecondsPerMinute * args[2] +
                    this.microsecondsPerSecond * args[3] +
                    this.microsecondsPerMillisecond * args[4] +
                    args[5]
                    );
            }
            else throw new Exception("ARGUMENT ERROR: Invalid argument.");
        }

        public void SetMicroseconds(decimal microseconds)
        {
            Duration._duration = (long)Math.Floor(microseconds);
        }

        public long MillisecondDuration { get { return _duration; } }


        public decimal InDays()
        {
            return Math.Abs(Duration._duration / microsecondsPerDay);
        }

        public decimal InHours
        {
            get { return Math.Abs(Duration._duration / this.microsecondsPerHour); }
        }

        public decimal InMinutes
        {
            get { return Math.Abs(Duration._duration / this.microsecondsPerMinute); }
        }

        public decimal InSeconds
        {
            get { return Math.Abs(Duration._duration / this.microsecondsPerSecond); }
        }

        public long InMilliseconds()
        {
            return (long)Math.Abs(Duration._duration / this.microsecondsPerMillisecond);
        }

        public decimal InMicroseconds { get { return Duration._duration; } }

        public bool IsNegative { get { return Duration._duration < 0; } }

        // operations
        public Duration Abs()
        {
            return new Duration((long)Math.Abs(Duration._duration));
        }

        Duration add(Duration other)
        {
            return new Duration(Duration._duration + other.MillisecondDuration);
        }

        Duration subtract(Duration other)
        {
            return new Duration(Duration._duration - other.MillisecondDuration);
        }

        Duration multiply(decimal factor)
        {
            return new Duration((long)Math.Round(Duration._duration * factor));
        }

        Duration divide(decimal quotient)
        {
            if (quotient == 0) throw new Exception("INTEGERDIVISIONBYZERO: Integer can not be divided by zero.");
            decimal val = Duration._duration / quotient;
            return new Duration((long)Math.Floor(val));
        }

        bool gt(Duration other)
        {
            return Duration._duration > other.MillisecondDuration;
        }

        bool gte(Duration other)
        {
            return Duration._duration >= other.MillisecondDuration;
        }

        bool lt(Duration other)
        {
            return Duration._duration < other.MillisecondDuration;
        }

        bool lte(Duration other)
        {
            return Duration._duration <= other.MillisecondDuration;
        }

        bool Equal(Duration other)
        {
            return Duration._duration == other.InMicroseconds;
        }

        int CompareTo(Duration other)
        {
            if (this.lt(other)) return -1;
            else if (this.Equal(other)) return 0;
            else return 1;
        }

        public override string ToString()
        {
            if (this.IsNegative) return $"-{this}";
            string min = TwoDigits(this.InMinutes % this.minutesPerHour);
            string sec = TwoDigits(this.InSeconds % this.secondsPerMinute);
            string micSec = SixDigits(this.InMicroseconds % this.microsecondsPerSecond);
            return $"{ InHours }:{ min }:${ sec}.{ micSec}";
        }
        private string SixDigits(decimal n)
        {
            if (n >= 100000) return $"{ n}";
            if (n >= 10000) return $"0{ n}";
            if (n >= 1000) return $"00{ n}";
            if (n >= 100) return $"000{ n}";
            if (n >= 10) return $"0000{ n}";
            return $"00000${ n }";
        }

        private string TwoDigits(decimal n)
        {
            if (n >= 10) return $"{ n}";
            return $"0{ n}";
        }
    }

}
