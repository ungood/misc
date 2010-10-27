const int redButton = 2;

void setup() {
  pinMode(redButton, INPUT);
  
  lcdSetup();
  lcdSetState();
}

void loop() {
  if(digitalRead(redButton)) {
    progressBar(redButton, 10*1000); // 10 seconds;
  }
}
