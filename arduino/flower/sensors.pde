void sensorSetup() {
  sensors[0].pin = 7;
  sensors[1].pin = 8;
  sensors[2].pin = 9;
  sensors[3].pin = 10;
  sensors[4].pin = 11;
//  sensors[5].pin = 12;
  
  for(int i = 0; i < NumSensors; i++) {
    pinMode(sensors[i].pin, INPUT);
    // enable internal pullup resistor
    // so that when a sensor is removed it will act as if it is always triggered.
    digitalWrite(sensors[i].pin, HIGH);
  }
  
  lcd.print("?f");
  lcd.print("?x00?y0");  
  lcd.print("    Calibrating");
  lcd.print("?x00?y1");
  lcd.print("      sensors");
  
  for(int i = 0; i < CalibrationTime; i++) {
    lcd.print(".");
    delay(1000);
  }
  
  lcd.print("?f");
  lcd.println("         Done");
  delay(DisplayDelay);
  
  lcd.print("?f");
  lcd.println("   SENSORS ACTIVE");
  lcd.print("?x00?y1");
  delay(DisplayDelay);

  lcd.print("?f");
  lcd.print("?x00?y0");
  lcd.print("    NOXIOUS WEEDS");
  lcd.print("?x00?y1");
  lcd.print("      BRC 2010");
  delay(DisplayDelay);
}

void resetSensors() {
  for(int i = 0; i < NumSensors; i++) {
    sensors[i].state = ACTIVE;
    sensors[i].activity = 0;
  }
}

void checkSensors(int ms) {
  // if a sensor is HIGH, add to the activity value, otherwise lower it.
  for(int i = 0; i < NumSensors; i++) {
    if(sensors[i].state != ACTIVE)
      continue;
    
    if(digitalRead(sensors[i].pin) == HIGH)
      sensors[i].activity += ms;
    else
      sensors[i].activity -= ms * SensorSensitivity;
    
    if(sensors[i].activity < 0)
      sensors[i].activity = 0;
    
    if(sensors[i].activity > (SensorThreshold * 1000)) 
      sensors[i].state = TRIGGERED;
    
    Serial.println(sensors[i].activity, DEC);
  }
}

boolean triggerSensors() {
  int activeCount = 0;
  
  for(int i = 0; i < NumSensors; i++) {
    if(sensors[i].state == TRIGGERED) {
      bloom();
      sensors[i].state = INACTIVE;
    }
    
    if(sensors[i].state == ACTIVE)
      activeCount++;
  }
  
  return activeCount == 0;
}
