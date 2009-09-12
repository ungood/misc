#ifndef ROOMBA_H
#define ROOMBA_H

#include <WProgram.h>
#include <NewSoftSerial.h>

// Delete these lines to only build the code for a specific Roomba type (either pre-500 or 500+ models)
// This will save space on your arduino.
// Don't forget to recompile your libraries for this change to take effect (either select a board, or restart the IDE)
#define ROOMBA_500
#define ROOMBA_CLASSIC

enum RoombaBaudRate {
	BAUD_300 = 0,
	BAUD_600,
	BAUD_1200,
	BAUD_2400,
	BAUD_4800,
	BAUD_9600,
	BAUD_14400,
	BAUD_19200,
	BAUD_28800,
	BAUD_38400,
	BAUD_57600,
	BAUD_115200
};

enum RoombaDirection {
	DIRECTION_STRAIGHT	= 32768,
	DIRECTION_LEFT			= -1,
	DIRECTION_RIGHT			= 1,
};

enum RoombaChargeState {
	CHARGING_NOTCHARGING = 0,
	CHARGING_RECOVERY,
	CHARGING_CHARGING,
	CHARGING_TRICKLE,
	CHARGING_WAITING,
	CHARGING_ERROR
};

class RoombaBase {
	protected:
		byte ddPin;
		NewSoftSerial* serial;
		bool readSensors(byte* ptr, byte offset, byte end);
	public:
		// Construction & Setup
		RoombaBase(NewSoftSerial* _serial, byte _ddPin);
		void select19200();                // Use the DD pin to select baud 19200
		void setBaud(RoombaBaudRate baud); // Select any valid baud.
	
		// Change mode
		void wakeUp();		// Wakes the Roomba up if it's asleep.  Mode will be either Off or Passive
		void start();		// Start -> Passive Mode
		void control();		// Passive -> Safe Mode
		void safe();		// Full -> Safe Mode
		void full();		// Safe -> Full
		void sleep();		// Safe/Full -> Passive. Puts Roomba to sleep.
	
		// Cleaning functions
		void spot();		// Starts a spot cleaning cycle.
		void clean();		// Starts a normal cleaning cycle.
		void maxClean();	// Starts a max time cleaning cycle.
		void seekDock();	// Forces the roomba to attempt to dock next time it's able.
		
		// Driving functions - Passive/Safe -> Safe, Full -> Full
		void drive(int velocity, int radius);
		inline void turnLeft(int velocity) { drive(velocity, DIRECTION_LEFT); }
		inline void turnRight(int velocity) { drive(velocity, DIRECTION_RIGHT); }
		inline void forward(int velocity) { drive(velocity, DIRECTION_STRAIGHT); }
		inline void backward(int velocity) { drive(velocity * -1, DIRECTION_STRAIGHT); }
		inline void stop() { drive(0, DIRECTION_STRAIGHT); }

		// Cleaning Motors - Safe/Full
		void setMotorsRaw(byte motors);
		
		// LEDs - Safe/Full
		void setLedsRaw(byte leds, byte powerColor, byte powerIntensity);
		
		// Define/Play songs - Passive(Define)/Safe/Full
		// songNumber - 0-15, the song to define
		// songLength - 1-15, number of notes in the song
		// song - Array of length 2*songLength, alternate note (31-127) with duration to play (0-255) in 64ths of a second.
		void defineSong(byte songNumber, byte numNotes, byte* song);
		void playSong(byte songNumber);
};

#ifdef ROOMBA_CLASSIC

struct RoombaClassicData {
	struct {
		unsigned rightBump	: 1;
		unsigned leftBump	: 1;
		unsigned rightWheel	: 1;
		unsigned leftWheel	: 1;
		unsigned caster		: 1;
	} wheels;
	byte wall;
	struct {
		byte left;
		byte frontLeft;
		byte frontRight;
		byte right;
	} cliff;
	byte virtualWall;
	struct {
		unsigned sideBrush	: 1;
		unsigned vacuum		: 1;
		unsigned mainBrush	: 1;
		unsigned driveRight	: 1;
		unsigned driveLeft	: 1;
	} overcurrent;
	struct {
		byte left;
		byte right;
	} dirtDetector;
	byte remoteOpcode;
	struct {
		unsigned max		: 1;
		unsigned clean		: 1;
		unsigned spot		: 1;
		unsigned power		: 1;
	} buttons;
	int distance;
	int angle;
	struct {
		RoombaChargeState chargingState;
		int voltage;
		int current;
		byte temperature;
		int charge;
		int capacity;
	} battery;
};

class RoombaClassic : public RoombaBase {
	protected: 
		union {
			RoombaClassicData parsed;
			byte raw[26];
		} data;
		
	public:
		RoombaClassic(NewSoftSerial* _serial, byte _ddPin);
		
		RoombaClassicData* poll(byte packet);
};

#endif 

#ifdef ROOMBA_500

struct Roomba500Data {
};

class Roomba500 : public RoombaBase {
	protected: 
		union {
			Roomba500Data parsed;
			byte raw[26];
		} data;
	
	public:
		Roomba500(NewSoftSerial* _serial, byte _ddPin);
		
		inline void setMotors(bool main, bool vacuum, bool side) {
			setMotorsRaw(main << 2 | vacuum << 1 | side);
		}
		
		inline void setLeds(bool redStatus, bool greenStatus, bool spot, bool clean, bool max, bool dirtDetect, byte powerColor, byte powerIntensity) {
			setLedsRaw(redStatus << 5 | greenStatus << 4 | spot << 3 | clean << 2 | max << 1 | dirtDetect, powerColor, powerIntensity); 
		}
		
		Roomba500Data* poll(byte packet);
};

#endif

#endif