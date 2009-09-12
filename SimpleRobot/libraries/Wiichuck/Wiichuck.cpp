#include "WProgram.h"
#include "Wiichuck.h"

void Wiichuck::init() {
	init(17, 16);
}

void Wiichuck::init(int powerPin, int groundPin) {
	// Set the output pins to VCC and GND
	if(powerPin > 0) {
	  pinMode(powerPin, OUTPUT);
	  digitalWrite(powerPin, HIGH);
	}
	
	if(groundPin > 0) {
	  pinMode(groundPin, OUTPUT);
	  digitalWrite(groundPin, LOW);
	}
	
	delay(100);
	
	Wire.begin();
	Wire.beginTransmission(Address);
	Wire.send(0x40);
	Wire.send(0x00);
	Wire.endTransmission();
}

boolean Wiichuck::poll() {
	Wire.requestFrom(Address, 6);// request data from nunchuck
      
	int bytes = 0;
	while(Wire.available() && bytes < 6) {
		// receive byte as an integer
		data.buffer[bytes++] = decode(Wire.receive());
	}
      
	// send request for next data payload
	Wire.beginTransmission(Address);
	Wire.send(0x00); 
	Wire.endTransmission();
      
	delay(100);
	return bytes >= 5;
}

void Wiichuck::print() {
	Serial.print("joy:");
	Serial.print(joyX(), DEC);
	Serial.print(",");
	Serial.print(joyY(), DEC);
	Serial.print("  \t");
	
	Serial.print("acc:");
	Serial.print(accelX(), DEC);
	Serial.print(",");
	Serial.print(accelY(), DEC);
	Serial.print(",");
	Serial.print(accelZ(), DEC);
	Serial.print("  \t");
	
	Serial.print("but:");
	Serial.print(buttonZ() ? "X" : "O");
	Serial.print(",");
	Serial.println(buttonC() ? "X" : "O");
}

