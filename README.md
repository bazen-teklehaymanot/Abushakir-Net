# Abushakir-Net

"Bahire Hasab /'bəhrɛ həsəb'/ " simply means "An age with a descriptive and chronological number". In some books it can also be found as "Hasabe Bahir", in a sense giving time an analogy, resembling a sea.

The words Bahire Hasab originate from the ancient language of Ge'ez, \( Arabic: Abu Shakir\) is a time-tracking method, devised by the 12th pope of Alexandria, Pope St. Dimitri.

This package allows developers to implement Ethiopian Calendar and Datetime System in their application\(s\)\`.

This package is implemented using the [UNIX EPOCH](https://en.wikipedia.org/wiki/Unix_time) which means it's not a conversion of any other calendar system into Ethiopian, for instance, Gregorian Calendar.

Unix Epoch is measured using milliseconds since 01 Jan, 1970 UTC. In UNIX EPOCH leap seconds are ignored.

## Getting started
PM> Install-Package imobiledevice-net

## Import it

## Example
 new EthiopianDateTime(2012, 7, 7).ToString(); // 2012-07-07 00:00:00.000
 new EthiopianDateTime(1585731446021).ToString();// 2012-07-23 08:57:26.021
 new EthiopianDateTime(DateTime.Now); // intializes Ethioopian datetime object with the current date and time
 
 DateTime.Now.ToEthiopianDateTime(); // converts DateTime into EthiopianDateTime or gregorian calendar to Ethiopian Calendar
 
 var date = new EthiopianDateTime(1585731446021);
 date.ToDateTime(); // returns the DateTime equevalent for the EthiopianDateTime
 
## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/Nabute/AbushakirJs/blob/master/LICENSE) file for details
