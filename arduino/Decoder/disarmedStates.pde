void disarmedEnter() {
  setArmedStatus(Disarmed);
  lcd.printTime(getGameLength());
  lcd.printDisarmed();
}

void disarmedUpdate() {
  if(areKeysIn()) {
    if(red.isPressed()) {
      stateMachine.transitionTo(ArmingState);
      return;
    }
  } else {
    int newIndex = 200;

    if(red.uniquePress())
      newIndex = (getGameLengthIndex() + 1);
       
    if(green.uniquePress())
      newIndex = (getGameLengthIndex() - 1);
 
    if(newIndex != 200) {
      if(newIndex >= numLengths)
        newIndex = 0;
      if(newIndex < 0)
        newIndex = numLengths - 1;
        
      setGameLengthIndex(newIndex);
      lcd.printTime(getGameLength());
    }
  }
}

int saveTimer = 0;
void disarmingEnter() {
  saveTimer = timer;
  resetTimer(disarmTime);
  lcd.printDisarming();
  beep(1);
}

void disarmingUpdate() {
  if(!green.isPressed()) {
    resetTimer(saveTimer);
    stateMachine.transitionTo(ArmedState);
    return;
  }
  
  if(decrementTimer()) {
    if(timer <= 0) {
      lcd.printDisarmed();
      beep(5);
      stateMachine.transitionTo(DisarmedState);
      return;
    }
    
    lcd.printTime(timer);
    beep(1);
  }
}

