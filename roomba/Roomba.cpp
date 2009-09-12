#include "Roomba.h"

// contains offset and size information for sensor packets
// NOTE: Only packet IDs 0-3 are available in RoombaClassic
static const byte RoombaPackets1[59][2] = {
	{  0, 26 },	// Packet Group 0,  7 - 26
	{  0, 10 }, // Packet Group 1,  7 - 16
	{ 10,  6 }, // Packet Group 2, 17 - 20
	{ 16, 10 }, // Packet Group 3, 21 - 26
	{ 27, 14 }, // Packet Group 4, 27 - 34
	{ 40, 12 }, // Packet Group 5, 35 - 42
	{  0, 52 }, // Packet Group 6,  7 - 42
	{  0,  1 }, // Packets 7 - 58
	{  1,  1 },
	{  2,  1 },
	{  3,  1 },
	{  4,  1 },
	{  5,  1 },
	{  6,  1 },
	{  7,  1 },
	{  8,  1 },
	{  9,  1 },
	{ 10,  1 },
	{ 11,  1 },
	{ 13,  2 },
	{ 15,  2 },
	{ 16,  1 },
	{ 18,  2 },
	{ 20,  2 },
	{ 21,  1 },
	{ 23,  2 },
	{ 25,  2 },
	{ 27,  2 },
	{ 29,  2 },
	{ 31,  2 },
	{ 33,  2 },
	{ 35,  2 },
	{ 36,  1 },
	{ 38,  2 },
	{ 39,  1 },
	{ 40,  1 },
	{ 41,  1 },
	{ 42,  1 },
	{ 43,  1 },
	{ 45,  2 },
	{ 47,  2 },
	{ 49,  2 },
	{ 51,  2 },
	{ 53,  2 },
	{ 55,  2 },
	{ 56,  1 },
	{ 58,  2 },
	{ 60,  2 },
	{ 62,  2 },
	{ 64,  2 },
	{ 66,  2 },
	{ 68,  2 },
	{ 69,  1 },
	{ 70,  1 },
	{ 72,  2 },
	{ 74,  2 },
	{ 76,  2 },
	{ 78,  2 },
	{ 79,  1 }
};

static const byte RoombaPackets2[8][2] = {
	{  0, 80 }, // Packet Group 100,  7 - 58
	{ 53, 28 }, // Packet Group 101, 43 - 58
	{  0,  0 },
	{  0,  0 },
	{  0,  0 },
	{  0,  0 },
	{ 58, 12 }, // Packet Group 106, 46 - 51
	{ 72,  9 }, // Packet Group 107, 54 - 58
};

RoombaBase::RoombaBase(NewSoftSerial* _serial, byte _ddPin) :
	ddPin(_ddPin)
{
	serial = _serial;
	pinMode(ddPin, OUTPUT);
	digitalWrite(ddPin, HIGH);
}

void RoombaBase::select19200() {
	for(int i = 0; i < 3; i++) {
		digitalWrite(ddPin, LOW);
		delay(100);
		digitalWrite(ddPin, HIGH);
		delay(100);
	}
}

void RoombaBase::setBaud(RoombaBaudRate baud) {
	serial->print(129, BYTE);
	serial->print(baud, BYTE);
}

void RoombaBase::wakeUp() {
	digitalWrite(ddPin, LOW);
	delay(500);
	digitalWrite(ddPin, HIGH);
	delay(2000);
}

void RoombaBase::start() {
	serial->print(128, BYTE);
	delay(20);
}

void RoombaBase::control() {
	serial->print(130, BYTE);
	delay(20);
}

void RoombaBase::safe() {
	serial->print(131, BYTE);
	delay(20);
}

void RoombaBase::full() {
	serial->print(132, BYTE);
	delay(20);
}

void RoombaBase::sleep() {
	serial->print(133, BYTE);
	delay(20);
}

void RoombaBase::spot() {
	serial->print(134, BYTE);
	delay(20);
}

void RoombaBase::clean() {
	serial->print(135, BYTE);
	delay(20);
}

void RoombaBase::maxClean() {
	serial->print(136, BYTE);
	delay(20);
}

void RoombaBase::seekDock() {
	serial->print(143, BYTE);
	delay(20);
}

void RoombaBase::drive(int velocity, int radius) {
	serial->print(137, BYTE);
	serial->print(velocity >> 8, BYTE);
	serial->print(velocity & 0x00FF, BYTE);
	serial->print(radius >> 8, BYTE);
	serial->print(radius & 0x00FF, BYTE);
}

void RoombaBase::setMotorsRaw(byte motors) {
	serial->print(138, BYTE);
	serial->print(motors, BYTE);
}

void RoombaBase::setLedsRaw(byte leds, byte powerColor, byte powerIntensity) {
	serial->print(139, BYTE);
	serial->print(leds, BYTE);
	serial->print(powerColor, BYTE);
	serial->print(powerIntensity, BYTE);
}

void RoombaBase::defineSong(byte songNumber, byte numNotes, byte* song) {
	serial->print(140, BYTE);
	serial->print(songNumber, BYTE);
	serial->print(numNotes, BYTE);
	for(int i = 0; i < numNotes * 2; i++)
		serial->print(song[i], BYTE);
}

void RoombaBase::playSong(byte songNumber) {
	serial->print(141, BYTE);
	serial->print(songNumber, BYTE);
}

bool RoombaBase::readSensors(byte* rawData, byte offset, byte len) {
	delay(100);
	
	byte end = offset + len;
	while(serial->available() && offset < end) {
		byte b = serial->read();
		if(b == -1)
			return false;
		rawData[offset] = b;
		offset++;
	}
	
	return offset == end;
}

#ifdef ROOMBA_CLASSIC

RoombaClassic::RoombaClassic(NewSoftSerial* _serial, byte _ddPin)
	: RoombaBase(_serial, _ddPin) {}

RoombaClassicData* RoombaClassic::poll(byte packet) {
	packet = constrain(packet, 0, 3);
	if(readSensors((byte*)&data, RoombaPackets1[packet][0], RoombaPackets2[packet][1]))
		return &data.parsed;
	else
		return NULL;
}

#endif

#ifdef ROOMBA_500

Roomba500::Roomba500(NewSoftSerial* _serial, byte _ddPin)
	: RoombaBase(_serial, _ddPin) {}

#endif