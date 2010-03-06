#include <wprogram.h>

const int lcdTx = 19;
const int lcdRx = 20;

SoftwareSerial sserial(lcdRx, lcdTx);

class Lcd {
  private:
    void printNum(int num);
  
  public:
    void setup();
    void bootUp();
    void clearState();
    void printAlarmMessage();
    void printTime(int time);
    void printDisarming();
    void printDisarmed();
    void printArming();
    void printArmed();
};

void Lcd::setup() {
  pinMode(lcdTx, OUTPUT);
  sserial.begin(9600);
}

void Lcd::bootUp() {
  sserial.print("?f");    // Clear screen
  sserial.print("?*");
  delay(2000);
}

void Lcd::clearState() {
  sserial.print("?f");
  sserial.print(" CLEARING STATE ");
  delay(2000);
  sserial.print("?BFF");                 delay(100); // Backlight: full
  sserial.print("?G216");                delay(100); // Display: 2x16
  sserial.print("?C0  DECODER GAME  ");  delay(100); // Setup boot screen
  sserial.print("?C1   BOOTING UP   ");  delay(100);
  sserial.print("?S0");                  delay(100);
  sserial.print("?c0");                  delay(100); // Cursor: off
}

void Lcd::printNum(int num) {
  if(num < 10)
    sserial.print("0");
  sserial.print(num, DEC);
}

void Lcd::printAlarmMessage() {
  sserial.print("?f");
  sserial.print(" !ALARM ALARM!  ");
  sserial.print(" KEYS & REBOOT  ");
}

void Lcd::printTime(int time) {
  sserial.print("?y0?l    ");
  this->printNum(time / 3600);
  sserial.print(":");
  this->printNum((time / 60) % 60);
  sserial.print(":");
  this->printNum(time % 60);
}

void Lcd::printDisarming() {
  sserial.print("?y1?l    DISARMING   ");
}

void Lcd::printDisarmed() {
  sserial.print("?y1?l    DISARMED    ");
}

void Lcd::printArming() {
  sserial.print("?y1?l     ARMING     ");
}

void Lcd::printArmed() {
  sserial.print("?y1?l     ARMED!     ");
}
