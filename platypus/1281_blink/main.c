#include <avr/io.h>
#include <util/delay.h>

void delayms(uint16_t millis) {
  _delay_ms(millis);
}

int main(void) {
  DDRF |= 1<<PF0; /* set PB0 to output */
  while(1) {
    PORTF &= ~(1<<PF0); /* LED on */
    delayms(100);
    PORTF |= 1<<PF0; /* LED off */
    delayms(900);
  }
  return 0;
}
