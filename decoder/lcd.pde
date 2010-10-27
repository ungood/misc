#include <SoftwareSerial.h>

const int lcdRx  = 19; // DO NOT USE
const int lcdTx  = 14;

SoftwareSerial lcd = SoftwareSerial(lcdRx, lcdTx);

// Really only need to call this once to setup LCD geometry, backlight, etc...
void lcdSetState() {
  // Geometry: 4x20
  lcd.print("?G420");
  delay(100);
  
  // Backlight: Full
  lcd.print("?BFF");
  delay(100);
  
  // Cursor: Off
  lcd.print("?c0");
  delay(100);
  
  // Bootscreen: Off, define custom screen.
  lcd.print("?S0"); delay(100);
   
  // Custom Characters
  lcd.print("?D00000000000000000"); delay(100); // 
  lcd.print("?D11010101010101010"); delay(100); // |
  lcd.print("?D21818181818181818"); delay(100); // ||
  lcd.print("?D31C1C1C1C1C1C1C1C"); delay(100); // |||
  lcd.print("?D41E1E1E1E1E1E1E1E"); delay(100); // ||||
  lcd.print("?D51F1F1F1F1F1F1F1F"); delay(100); // |||||
  
  lcd.print("?D60E1B11111F1F1B1F"); delay(100); // Locked
  lcd.print("?D70E1B10101F1F1B1F"); delay(100); // Unlocked
}

void lcdSetup() {
  pinMode(lcdTx,  OUTPUT);
  lcd.begin(9600);
  delay(1000);
  lcd.print("?f");
}

/* PROGRESS BAR */
void progressTick(int& full, int& partial) {
  lcd.print("?b?");
  lcd.print(partial, DEC);
  
  if(partial >= 5) {
    lcd.print("?0");
    delay(5);
    full++;
    partial = 0;
  }
  
  partial++;
}

boolean progressBar(int pin, int timeout) {
  lcd.print("?y1?x03?l"); // Position cursor, clear line.
  lcd.print("?0");
  delay(5);

  int full = 0, partial = 1;  
  int current, last;
  current = last = millis();
  
  while(timeout > 0 && digitalRead(pin)) {
    current = millis();
    int ms = current - last;
    while(ms > 200) {
      progressTick(full, partial);
      last = current;
      timeout -= ms;
      ms -= 200;
    }
  }
  
  return timeout <= 0;
}
