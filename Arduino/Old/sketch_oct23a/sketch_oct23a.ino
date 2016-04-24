float RGB[3];
int redLed = 5; // red LED in Digital Pin 11 (PWM)
int greenLed = 6; // green LED in Digital Pin 10 (PWM)
int blueLed = 3; // blue LED in Digital Pin 9 (PWM)
int delaymood = 10; // delay for mood light

void setup() {
  
}

void loop() {
  for (int y = 0; y <= 3; y++) {
    if (y = 1) {
      for (int x = 0; x <= 255; x++) {
        RGB[0] = 255 - x;
        RGB[1] = 0;
        RGB[2] = 0 + x;
        analogWrite(redLed, RGB[0]);
        analogWrite(greenLed, RGB[1]);
        analogWrite(blueLed, RGB[2]);
        delay(delaymood);
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
        delay(delaymood);
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
        delay(delaymood);
      }
    }
  }
}
