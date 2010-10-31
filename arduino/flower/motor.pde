void motorSetup() {
  pinMode(UpPin, OUTPUT);
  pinMode(DownPin, OUTPUT);
  pinMode(TopStopPin, INPUT);
  pinMode(BottomStopPin, INPUT);
}

void stop() {
  digitalWrite(DownPin, LOW);
  digitalWrite(UpPin, LOW);
}

void moveDown() {
  if(atBottom())
    return;
    
  digitalWrite(UpPin, LOW);
  digitalWrite(DownPin, HIGH);
}

void moveUp() {
  if(atTop())
    return;
  
  digitalWrite(DownPin, LOW);
  digitalWrite(UpPin, HIGH);
}

boolean atTop() {
  return digitalRead(TopStopPin) == HIGH;
}

boolean atBottom() {
  return digitalRead(BottomStopPin) == HIGH;
}

void resetMotor() {
  lcd.print("?y2?x00?l");
  lcd.print("RESET");

  moveUp();
  while(!atTop());
  stop();
  
  lcd.print("?y2?x00?l");
}

void bloom() {
  lcd.print("?y2?x00?l");
  lcd.print("BLOOM");

  moveDown();
  delay(TotalMotorTime / NumSensors);
  stop();

  lcd.print("?y2?x00?l");
  last = millis();
}

