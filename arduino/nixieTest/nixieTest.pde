const int outPin = 6;
const int clearPin = 5;

const int latchPin = 3;
const int clockPin = 2;
const int dataPin = 4;

void setup() {
  pinMode(outPin, OUTPUT);
  pinMode(clearPin, OUTPUT);
  pinMode(latchPin, OUTPUT);
  pinMode(clockPin, OUTPUT);
  pinMode(dataPin, OUTPUT);
  
  digitalWrite(clearPin, HIGH);
  digitalWrite(latchPin, HIGH);
  display(0x11);
  
  Serial.begin(9600);
}

void loop() {
  for(int i = 0; i < 256; i++) {
    display(i);
    delay(100);
    //while(Serial.read() < 0);
  }
}

void display(int data) {
  Serial.println(data, BIN);
  digitalWrite(latchPin, LOW);
  shiftOut(clockPin, dataPin, LSBFIRST, data);
  shiftOut(clockPin, dataPin, LSBFIRST, data);
  digitalWrite(latchPin, HIGH);
}



