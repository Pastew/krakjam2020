int potPin = 2;
int potVal = 0;

void setup() {
  Serial.begin(250000); // You can choose any baudrate, just need to also change it in Unity.
  //while (!Serial); // wait for Leonardo enumeration, others continue immediately
}

void loop() {
  potVal = analogRead(potPin);
  potVal = 1023 - potVal;
  potVal = map(potVal, 0, 1023, 0, 1000);
  sendData(potVal);
  delay(5); // Choose your delay having in mind your ReadTimeout in Unity3D
}

void sendData(int data) {
  Serial.println(data); // need a end-line because wrmlh.csharp use readLine method to receive data
}
