#pragma once

#include <inttypes.h>
typedef uint8_t byte;  

class DateTime
{
private:
	int year;
	byte month;
	byte date;
	byte day;
	byte hour;
	byte minute;
	byte second;
public:
	DateTime(int _year, byte _month, byte _date, byte _day, byte _hour, byte _minute, byte _second) {
		set(_year, _month, _date, _day, _hour, _minute, _second);
	}
	
	void set(int _year, byte _month, byte _date, byte _day, byte _hour, byte _minute, byte _second);
		
	inline int getYear() const { return year; }
	inline byte getMonth() const { return month; }
	inline byte getDate() const { return date; }
	inline byte getDay() const { return day; }
	inline byte getHour() const { return hour; }
	inline byte getMinute() const { return minute; }
	inline byte getSecond() const { return second; }
};
