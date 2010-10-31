const int topPin = 3;
const int bottomPin = 4;
const int upPin = 5;
const int downPin = 6;
const int sensorPin = 0; // analog pin

const int nightThreshold = 50;
const int dayThreshold = 900;

void setup() {
  // Set internal pullupresistors on the switches
  pinMode(topPin, INPUT);
  digitalWrite(topPin, HIGH);
  pinMode(bottomPin, INPUT);
  digitalWrite(bottomPin, HIGH);
  
  pinMode(upPin, OUTPUT);
  pinMode(downPin, OUTPUT);
  stop();
  
  pinMode(sensorPin, INPUT);
}

void close() {
  digitalWrite(upPin, LOW);
  digitalWrite(downPin, HIGH);
}

void open() {
  digitalWrite(downPin, LOW);
  digitalWrite(upPin, HIGH);
}

void stop() {
  digitalWrite(downPin, LOW);
  digitalWrite(upPin, LOW);
}

boolean isNight(int lightValue) {
  return lightValue <= nightThreshold && digitalRead(bottomPin) == HIGH;
}

boolean isDay(int lightValue) {
  return lightValue >= dayThreshold && digitalRead(topPin) == HIGH;  
}

void loop() {
  int lightValue = analogRead(sensorPin);
  
  if(isNight(lightValue)) {
    close();
  } else if(isDay(lightValue)) {
    open();
  } else {
    stop();
  }
}
