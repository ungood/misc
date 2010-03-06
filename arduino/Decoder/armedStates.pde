void armingEnter() {
  resetTimer(armingTime);
  lcd.printArming();
  beep(1);
}

void armingUpdate() {
  if(!areKeysIn() || !red.isPressed()) {
    stateMachine.transitionTo(DisarmedState);
    return;
  }
  
  if(decrementTimer()) {
    if(timer <= 0) {
      stateMachine.transitionTo(ArmedState);
      return;
    }
    
    lcd.printTime(timer);
    beep(1);
  }
}

void armingExit() {
  setArmedStatus(Armed);
  resetTimer(getGameLength());
  lcd.printArmed();
  beep(5);
}

void armedEnter() {
  lcd.printArmed();
}

void armedUpdate() {
  if(green.isPressed()) {
    if(areKeysIn())
      stateMachine.transitionTo(DisarmingState);
    else
      stateMachine.transitionTo(PermaAlarmState);
    return;
  }
  
  if(decrementTimer()) {
    if(timer <= 0) {
      stateMachine.transitionTo(PermaAlarmState);
      return;
    }
    
    lcd.printTime(timer);
  }
}
