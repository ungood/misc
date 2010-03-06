void permaAlarmsEnter() {
  lcd.printAlarmMessage();
}

void permaAlarmsUpdate() {
  for(int i = 0; i < numBuzzers; i++) {
    digitalWrite(buzzerPins[i], HIGH);
    delay(250);
    digitalWrite(buzzerPins[i], LOW);
  }
}
