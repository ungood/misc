#include <Wire.h>
#include <AFMotor.h>
#include <Wiichuck.h>

Wiichuck chuck;
AF_DCMotor lMotor(1, MOTOR12_1KHZ);
AF_DCMotor rMotor(2, MOTOR12_1KHZ);

void setup() {
  Serial.begin(9600);
  chuck.init();
  release();
}

void loop() {
  chuck.poll();
  if(chuck.buttonC())
    chuck.calibrate();
  else if(!chuck.buttonZ())
    release();
  else 
    go();
}

void go() {
  int x = constrain(chuck.joyX(), -90, 90);
  int y = constrain(chuck.joyY(), -90, 90);
  
  x = map(x, -90, 90, -255, 255);
  y = map(y, -90, 90, -255, 255);
  
  int l = constrain(y + x, -255, 255);
  int r = constrain(y - x, -255, 255);

  return;
  left(l);
  right(r);
}

void left(int speed) {
  lMotor.setSpeed(Math.abs(speed));
  lMotor.run(speed >= 0 ? FORWARD : BACKWARD);
}

void right(int speed) {
  rMotor.setSpeed(Math.abs(speed));
  rMotor.run(speed >= 0 ? BACKWARD : FORWARD); // I soldered the wires backwards.
}

void release() {
  lMotor.run(RELEASE);
  rMotor.run(RELEASE);
}

void brake() {
  lMotor.setSpeed(0);
  rMotor.setSpeed(0);
}





