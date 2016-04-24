float RGB[3];
const int redLed = 5; // red LED in Digital Pin 11 (PWM)
const  int greenLed = 6; // green LED in Digital Pin 10 (PWM)
const int blueLed = 3; // blue LED in Digital Pin 9 (PWM)
int delaymood = 10; // delay for mood light

boolean moodShiftOn = true;

void setup() {
  Serial.begin(9600);
  //while(!Serial);
}

void loop() {
  
}

void checkSerial()
{
  if(Serial)
  {
    while(Serial.available() > 0)
    {
      delaymood = Serial.parseInt();
      delaymood = constrain(delaymood, 0, 4000);
      Serial.readString();
      Serial.println(delaymood);
     }
  }
}

void moodLoop(){
  for (int y = 0; y <= 3; y++) {
    if (y = 1) {
      for (int x = 0; x <= 255; x++) {
        RGB[0] = 255 - x;
        RGB[1] = 0;
        RGB[2] = 0 + x;
        analogWrite(redLed, RGB[0]);
        analogWrite(greenLed, RGB[1]);
        analogWrite(blueLed, RGB[2]);
        checkSerial();
        if (delaymood > 0){
          delay(delaymood);
        } else {
          x=x+9;
        }
      }
    }
    if (y = 2) {
      for (int x = 0; x <= 255; x++) {
        RGB[0] = 0;
        RGB[1] = 0 + x;
        RGB[2] = 255 - x;
        analogWrite(redLed, RGB[0]);
        analogWrite(greenLed, RGB[1]);
        analogWrite(blueLed, RGB[2]);
        checkSerial();
       if (delaymood > 0){
          delay(delaymood);
        } else {
          x=x+9;
        }
      }
    }
    if (y = 3) {
      for (int x = 0; x <= 255; x++) {
        RGB[0] = 0 + x;
        RGB[1] = 255 - x;
        RGB[2] = 0;
        analogWrite(redLed, RGB[0]);
        analogWrite(greenLed, RGB[1]);
        analogWrite(blueLed, RGB[2]);
        checkSerial();
        if (delaymood > 0){
          delay(delaymood);
        } else {
          x=x+9;
        }
      }
    }
  }
}

