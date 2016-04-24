
#define versionNumber "0.1"
#define numberOfStrips 1
// pins for the LEDs:
int RGBpins[numberOfStrips][3] = {{5, 6, 3}}; //redPin, greenPin, bluePin
int RGBset[numberOfStrips][4] = {{0, 0, 0, 0}}; //red, green, blue, mode

void setup() {
  // put your setup code here, to run once:
  updateLights();
  Serial.begin(9600);
  Serial.print("RGBControllerVersion: " versionNumber " numberOfStrips: ");
  Serial.println(numberOfStrips);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial.available()){
  	if(readSerial()){
  		updateLights();
  	}
  }
}

/**
* this method updates all ports in acordance with the RGBset Array
**/
void updateLights(){
	for (int i = 0; i<numberOfStrips; i++){
		if(RGBset[i][3] == 0){ //if mode is set to 0 aka static colour
		 	RGB(i, RGBset[i][0], RGBset[i][1], RGBset[i][2]);   
		}
	}
}

/**
* overloaded method that sets RGB values to first element in array
**/
void RGB(int red, int green, int blue){
	RGB(0, red, green, blue);
}


/**
* this method sets PWN outputs to produce desired colour
**/
void RGB(int index, int red, int green, int blue){
	analogWrite(RGBpins[index][0], red);
	analogWrite(RGBpins[index][1], green);
	analogWrite(RGBpins[index][2], blue);
}

/**
*method that try's to read the next controll string
*returns true if it updated the RGBset array in any way
*
*format for input is "S(indexOfStrip),(velueRed),(velueGreen),(velueBlue),(mode)E"
*example is "S0,0,0,0,1E" which should set strip 0 into mode 1
*example is "S0,255,0,255,0E" which should set strip 0 to colour 255, 0, 255 which should be purple
**/
bool readSerial(){
	if (Serial.find("S")){
		int index = Serial.parseInt();
		int red = Serial.parseInt();
		int green = Serial.parseInt();
		int blue = Serial.parseInt();
		int mode = Serial.parseInt();
		char ending = Serial.read();
		if (ending != 'E'){
			return false; //in case ending is not E something went wrong and arduino will wait for next input string. This should never happen as long as input is correct
		}
		RGBset[index][0] = red;
		RGBset[index][1] = green;
		RGBset[index][2] = blue;
		RGBset[index][3] = mode;
		return true;
	} 
	return false;
}
