void checkKeys() {
  boolean keysAreIn = areKeysIn();
  if(keysAreIn)
    stateMachine.transitionTo(ClearEepRomState);
  else
    stateMachine.transitionTo(BootUpState);
}

void clearEepRom() {
  lcd.clearState();
  setGameLengthIndex(0);
  setArmedStatus(Disarmed);
  
  stateMachine.transitionTo(BootUpState);
}

void bootUp() {
  lcd.bootUp();
  
  if(getArmedStatus() == Armed) {
    stateMachine.transitionTo(PermaAlarmState);
  } else {
    stateMachine.transitionTo(DisarmedState);
  } 
}
