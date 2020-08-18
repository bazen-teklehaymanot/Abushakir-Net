using System;

namespace Abushakir_Net.Extensions
{
    public static class DateTimeExtensions
    {
        public static EthiopianDateTime ToEthiopianDateTime(this DateTime dateTime) => new EthiopianDateTime(dateTime);
    }
}
