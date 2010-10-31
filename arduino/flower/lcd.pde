void lcdSetup() {
  pinMode(TxPin, OUTPUT);
  lcd.begin(9600);
  delay(500); 
  lcd.print("?f");
}

void displaySensorState() {
  lcd.print("?y3?x00");
  for(int spaces = 0; spaces < (20 - NumSensors) / 2; spaces++)
    lcd.print(" ");
  
  for(int i = 0; i < NumSensors; i++) {
    switch(sensors[i].state) {
      case ACTIVE:
        lcd.print("O");
        break;
      case TRIGGERED:
        lcd.print("#");
        break;
      case INACTIVE:
        lcd.print("X");
        break;
    }
    lcd.print("");
  }
}

void printNum(int num) {
  if(num < 10)
    lcd.print("0");
  lcd.print(num, DEC);
}

void printTime(int time) {
  lcd.print("?y2?x00?l  FULL BLOOM ");
  printNum((time / 60) % 60);
  lcd.print(":");
  printNum(time % 60);
}
