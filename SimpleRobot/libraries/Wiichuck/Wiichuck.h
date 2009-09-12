/*
 * Wiichuck library -- Talk to a Wii Nunchuck
 * Based off code from:
 * 2007 Tod E. Kurt, http://todbot.com/blog/
 * The Wii Nunchuck reading code originally from Windmeadow Labs
 *   http://www.windmeadow.com/node/42
 */

#ifndef Wiichuck_h
#define Wiichuck_h

#include "WConstants.h"
#include "../Wire/Wire.h"

class Wiichuck {
  private:
    static const int Address = 0x52;
    
    // Wiichuck "encodes" data in some silly way.
    static inline byte decode(byte b) { return (b ^ 0x17) + 0x17; }
    
    // This is the retrieved data, it can be
    // accessed either as an array of bytes,
    // or parsed out as a struct.
    union {
      byte buffer[6];
      struct {
        byte joyX;
        byte joyY;
        byte accelX;
        byte accelY;
        byte accelZ;
        byte buttonZ : 1;
        byte buttonC : 1;
        byte lsbX : 2;
        byte lsbY : 2;
        byte lsbZ : 2;
      } parsed;
    } data;
    
		
  public:
    // Set powerPin and groundPin to 0 if you've plugged
    // them into VCC and GND
		void init(int powerPin, int groundPin);
		
		void init();
    
    // Requests data from the nunchuck
    boolean poll();
    
    void print();

    inline byte joyX() { return data.parsed.joyX; }    
    inline byte joyY() { return data.parsed.joyY; }

    inline unsigned int accelX() { return (data.parsed.accelX << 2) | data.parsed.lsbX; }
    inline unsigned int accelY() { return (data.parsed.accelY << 2) | data.parsed.lsbY; }
    inline unsigned int accelZ() { return (data.parsed.accelZ << 2) | data.parsed.lsbZ; }
    
    inline boolean buttonZ() { return !data.parsed.buttonZ; }
    inline boolean buttonC() { return !data.parsed.buttonC; }
};

#endif

