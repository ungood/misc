#include <AFMotor.h>

AF_DCMotor left(1, MOTOR12_64KHZ);
AF_DCMotor right(2, MOTOR12_64KHZ);

const int leftPin = 1;
const int rightPin = 0;

void setup() {
  pinMode(leftPin, INPUT);
  pinMode(rightPin, INPUT);
  
  delay(5000);

  right.setSpeed(255);
  left.setSpeed(255);
  forward();
  delay(1000);
}

void backward() {
  right.run(FORWARD);
  left.run(FORWARD);
}

void forward() {
  right.run(BACKWARD);
  left.run(BACKWARD);
}

void rightBackward() {
  backward();
  delay(1000);
  left.run(BACKWARD);
  delay(500);
  forward();
}

void leftBackward() {
  backward();
  delay(1000);
  right.run(BACKWARD);
  delay(500);
  forward();
}

int smooth(int data, float filterVal, float smoothedVal){
  if (filterVal > 1){      // check to make sure param's are within range
    filterVal = .99;
  }
  else if (filterVal <= 0){
    filterVal = 0;
  }

  smoothedVal = (data * (1 - filterVal)) + (smoothedVal  *  filterVal);

  return (int)smoothedVal;
}

float leftSmooth  = 0.0;
float rightSmooth = 0.0;
void loop() {
  int left = analogRead(leftPin);
  int right = analogRead(rightPin);
  
  leftSmooth = smooth(left, 0.5, leftSmooth);
  rightSmooth = smooth(right, 0.5, rightSmooth);
  
  if(leftSmooth - rightSmooth > 5)
    rightBackward();
  else if(rightSmooth - leftSmooth > 5)
    leftBackward();
//  boolean reverseLeft  = analogRead(leftPin) >= threshold;
//  boolean reverseRight = analogRead(rightPin) >= threshold;
//  reverseLeft  ? leftBackward()  : leftForward();
//  reverseRight ? rightBackward() : rightForward();
}
