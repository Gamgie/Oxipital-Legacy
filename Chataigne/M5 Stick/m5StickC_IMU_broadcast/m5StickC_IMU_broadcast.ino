
#include <M5StickCPlus.h>
#include <WiFi.h>
#include <ESPmDNS.h>
#include <WiFiClient.h>
#include <Preferences.h>
#include <WiFiUdp.h>
#include <OSCMessage.h>
#include <OSCBundle.h>
#include <OSCData.h>
#include "AXP192.h"

WiFiUDP Udp;
IPAddress remoteIp(192,168,17,232);       // target IP, will update if message is received
bool memorizeRemoteIP = true;             // if true, remoteIp will be erased with previous value
const unsigned int remotePort = 12000;    // target port, won't change
const unsigned int localPort = 12345;     // port receiving OSC

char ssid[] = "Gamgie’s iPhone";          // your network SSID (name)
char pass[] = "12345678";                    // your network password

Preferences preferences;

float accX, accY, accZ, gyroX, gyroY, gyroZ, pitch, roll, yaw = 0.0F;

void setup() {
    // INIT SCREEN
    M5.begin();
    M5.Lcd.setRotation(3);  // Rotate the screen. -
    M5.Lcd.fillScreen(BLUE);
    M5.Lcd.setTextSize(1);
    
    // INIT WIFI
    WiFi.mode(WIFI_STA);
    WiFi.disconnect();
    WiFi.begin(ssid, pass);
  
    M5.Lcd.setCursor(30, 15);
    M5.Lcd.println("Searching for network:");
    M5.Lcd.setCursor(30, 30);
    M5.Lcd.println(String(ssid) + " / "+String(pass));
    M5.Lcd.setCursor(30, 45);
    M5.Lcd.println("...");
    while (WiFi.status() != WL_CONNECTED) {
      delay(500);
      Serial.print(".");
    }
    Serial.println("WiFi connected");
    Serial.println("IP address: ");
    Serial.println(WiFi.localIP());
    
    // INIT MDNS
    M5.Lcd.fillScreen(GREEN);
    M5.Lcd.println("MDNS...");
    if (MDNS.begin("M5Stick_IMU"))
    {
      MDNS.addService("_osc", "_udp", localPort);
      Serial.println("Done !");
    }
    else
    {
      Serial.println("could not set up mDNS instance");
      M5.Lcd.fillScreen(RED);
      delay(3000);
    }

    // INIT PREFERENCES
    preferences.begin("network");
    if (memorizeRemoteIP && preferences.isKey("remoteIp"))
    {
      remoteIp.fromString(preferences.getString("remoteIp"));
      Serial.println("override remote IP with previous value: "+remoteIp.toString());
    }
    preferences.end();
    
    // INIT OSC
    Serial.println("Listening on " + String(localPort));
    Udp.begin(localPort);
    Udp.flush();
    
    // INIT IMU AND DISPLAY
    M5.Imu.Init(); 
    M5.Lcd.fillScreen(BLACK);
}

void loop()
{
  M5.update(); // update button state
   
  checkOSC(); // update target IP if OSC msg is received
  
  // GET SENSOR VALUES
  M5.IMU.getGyroData(&gyroX, &gyroY, &gyroZ);
  M5.IMU.getAccelData(&accX, &accY, &accZ);
  M5.IMU.getAhrsData(&pitch, &roll, &yaw);
  //static float temp = 0;
  //M5.IMU.getTempData(&temp);

  // DISPLAY
  displayValues();

  // SEND VALUES
  sendValues("/gyro", gyroX, gyroY, gyroZ);
  sendValues("/acc", accX, accY, accZ);
  sendValues("/rotation", pitch, roll, yaw);
}

void sendValues(String adress, float valx, float valy, float valz)
{
  OSCMessage msg(adress.c_str());
  msg.add(valx);
  msg.add(valy);
  msg.add(valz);
  
  Udp.beginPacket(remoteIp, remotePort);
  msg.send(Udp);
  Udp.endPacket(); 
  msg.empty();
}

void displayValues()
{
  M5.Lcd.setCursor(50, 5);
  M5.Lcd.println("  X       Y       Z");

  M5.Lcd.setCursor(5, 15);
  M5.Lcd.print("gyro");
  M5.Lcd.setCursor(50, 15);
  M5.Lcd.printf("%6.2f  %6.2f  %6.2f      ", gyroX, gyroY, gyroZ);
  M5.Lcd.setCursor(190, 15);
  M5.Lcd.print("o/s");
  M5.Lcd.setCursor(5, 25);
  M5.Lcd.print("acc");
  M5.Lcd.setCursor(50, 25);
  M5.Lcd.printf(" %5.2f   %5.2f   %5.2f   ", accX, accY, accZ);
  M5.Lcd.setCursor(190, 25);
  M5.Lcd.print("G");
  
  M5.Lcd.setCursor(50, 45);
  M5.Lcd.println("  Pitch   Roll    Yaw");
  
  M5.Lcd.setCursor(5, 55);
  M5.Lcd.print("angle");
  M5.Lcd.setCursor(50, 55);
  M5.Lcd.printf(" %5.2f   %5.2f   %5.2f   ", pitch, roll, yaw);

  M5.Lcd.setCursor(5, 80);
  M5.Lcd.print("battery");
  float battPercent = (M5.Axp.GetBatVoltage() - 3.1f) / (4.15f - 3.1f);
  M5.Lcd.setCursor(60, 80);
  M5.Lcd.print(String(100*battPercent) + "%");
  M5.Lcd.setCursor(110, 80);
  M5.Lcd.printf("%.2f V ", M5.Axp.GetBatVoltage());
  delay(5);

  M5.Lcd.setCursor(10, 100);
  M5.Lcd.println("send any OSC message at");
  M5.Lcd.setCursor(10, 110);
  M5.Lcd.println(WiFi.localIP().toString() + " @ "+String(localPort));
  M5.Lcd.setCursor(10, 120);
  M5.Lcd.println("to receive values on port 12000");
}

void checkOSC()
{
  OSCMessage incMsg;
  int size;
  if ((size = Udp.parsePacket()) > 0)
  {
      while (size--)
          incMsg.fill(Udp.read());
          
      // if remote is new, update remoteIp
      if (!incMsg.hasError())
      {
        if (remoteIp != Udp.remoteIP())
        {
          // update IP
          remoteIp = Udp.remoteIP();
          
          // save value in prefs
          preferences.begin("network");
          preferences.putString("remoteIp", Udp.remoteIP().toString());
          preferences.end();
          Serial.println("new target IP:" + Udp.remoteIP().toString());
        }
      }
  }
}
