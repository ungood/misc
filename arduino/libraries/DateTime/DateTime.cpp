#include "WProgram.h"
#include "DateTime.h"

void DateTime::set(int _year, byte _month, byte _date, byte _day, byte _hour, byte _minute, byte _second)
{
	year = _year;
	month = _month;
	date = _date;
	day = _day;
	hour = _hour;
	minute = _minute;
	second = _second;	
}