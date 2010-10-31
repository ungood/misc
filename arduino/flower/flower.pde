#include <SoftwareSerial.h>

// sensors
const int NumSensors = 5;
const int SensorThreshold = 3;      // How much motion (in seconds) is needed to make the thing open up.
const int SensorSensitivity = 50; // How much to compensate for false postives.
const int CalibrationTime = 1;      // Sensor calibration time, in seconds.

enum SensorState {
  OFF = 0,
  ACTIVE,
  TRIGGERED,
  INACTIVE
};

struct Sensor {
  int pin;
  SensorState state;
  int activity;
} sensors[NumSensors];

// LCD
const int DisplayDelay = 500;
const int TxPin = 14;
const int RxPin = 0;
SoftwareSerial lcd =  SoftwareSerial(RxPin, TxPin);

// motor
const int UpPin = 1;
const int DownPin = 2;
const int TopStopPin = 3;
const int BottomStopPin = 4;
const int TotalMotorTime = 1 * 1000; // Time it takes for motor to travel entire distance
const long BloomTime = 1;              // Time to keep the flower bloomed, in minutes.


// timer
unsigned long ms = 0;
unsigned long last = 0;

void setup() {
  lcdSetup();
  sensorSetup();
  motorSetup();
  reset();

  Serial.begin(9600);
  Serial.println("hi");
}

void loop() {
  unsigned long current = millis();
  ms = (current - last);
  last = current;
  
  checkSensors(ms);
  displaySensorState();
  if(triggerSensors()) {
    displaySensorState();
    for(int time = BloomTime * 60; time > 0; time--) {
      printTime(time);
      delay(1000);
    }
    reset();
  }
}

void reset() {
  last = millis();
  resetSensors();
  resetMotor();
}
