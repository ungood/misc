#include <EEPROM.h>
#include <Button.h>
#include <SoftwareSerial.h>
#include <FiniteStateMachine.h>
#include "lcd.h"

typedef enum {
  Disarmed = 0,
  Armed = 1
} ArmedStatus;

const int numBuzzers = 3;
int buzzerPins[] = { 9, 10, 11 };

const int numKeys = 3;
int keyPins[] = { 3, 4, 5 };

const int redPin = 6;
const int greenPin = 7;

const int numLengths = 5;
int gameLengths[] = { 60, 90, 120, 30, 45 };
//int gameLengths[] = { 1, 2, 3, 4, 5 };

const int armingTime = 10;
const int disarmTime = 10;

Lcd lcd;
Button red(redPin, PULLUP);
Button green(greenPin, PULLUP);
int timer = 0;
unsigned long ms = 0;
unsigned long last = 0;

// States
State InitialState(checkKeys);
State ClearEepRomState(clearEepRom, nop, nop);
State BootUpState(bootUp, nop, nop);
State PermaAlarmState(permaAlarmsEnter, permaAlarmsUpdate, nop);
State DisarmingState(disarmingEnter, disarmingUpdate, nop);
State DisarmedState(disarmedEnter, disarmedUpdate, nop);
State ArmingState(armingEnter, armingUpdate, armingExit);
State ArmedState(armedEnter, armedUpdate, nop);

FSM stateMachine(InitialState);

void setup() {
  lcd.setup();
  delay(1000);
  
  for(int i = 0; i < numBuzzers; i++)
    pinMode(buzzerPins[i], OUTPUT);
    
  for(int i = 0; i < numKeys; i++) {
    digitalWrite(keyPins[i], HIGH); // turn on internal pullup
  }
  
  digitalWrite(redPin, HIGH);  // internal pullups
  digitalWrite(greenPin, HIGH); 
  
  last = millis();
}

void loop() {
  unsigned long current = millis();
  ms += (current - last);
  last = current;
  
  stateMachine.update();
}

boolean areKeysIn() {
  for(int i = 0; i < numKeys; i++) {
    if(digitalRead(keyPins[i]) == HIGH) // Keys are on pullup resistor.
      return false;
  }
  
  return true;
}

void resetTimer(int newTimer) {
  ms = 0;
  timer = newTimer;
  lcd.printTime(timer);
}

boolean decrementTimer() {
  boolean decremented = false;
  while(ms > 1000) {
    timer--;
    ms -= 1000;
    decremented = true;
  }
  
  return decremented;
}

void beep(int beeps) {
  for(int j = 0; j < beeps; j++) {
    for(int i = 0; i < numBuzzers; i++)
      digitalWrite(buzzerPins[i], HIGH);
      
    delay(125);
    
    for(int i = 0; i < numBuzzers; i++)
      digitalWrite(buzzerPins[i], LOW);
     
    delay(125);
  }
}

void nop() {}



