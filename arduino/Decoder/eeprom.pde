const byte ArmedStatusAddress = 0;
const byte GameLengthAddress = 1;

void setGameLengthIndex(byte index) {
  EEPROM.write(GameLengthAddress, index);
}

byte getGameLengthIndex() {
  return EEPROM.read(GameLengthAddress);
}

int getGameLength() {
  return gameLengths[getGameLengthIndex()] * 60;
}

void setArmedStatus(byte status) {
  EEPROM.write(ArmedStatusAddress, status);
}

byte getArmedStatus() {
  return EEPROM.read(ArmedStatusAddress);
}
