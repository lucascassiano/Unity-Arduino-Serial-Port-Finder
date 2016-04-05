Unity Android Serial Finder
=================
A simple API designed to simplify the communication between Unity 3D and Arduino Compatible Boards. 

The basis of this API is that both Arduino and Unity projects share a **Handshake** code, it is a simple way to connect to Arduino Devices without knowing previously the Serial Port (e.g. "COM3"). It can also be used to give different "names" per Arduino Board.

The Library also handles a thread for continous reading/writing from the SerialPort (in Unity Net 2.0).

# Installation
Download the directory from this [Github Page](https://github.com/lucascassiano/Unity-Arduino-Serial-Port-Finder/) and follow the steps on bellow (for each Arduino and Unity 3D).


### Arduino Installation
1. Copy the files inside the folder **SerialFinder _ Arduino** to your Arduino Code's Folder, for example called "mySketch".
2. Then inside your "/mySketch/" directory you must have the following files:
   - *SerialFinder.cpp*
   - *SerialFinder.h*
   - *keywords.txt*
3. In your sketch's main code include the library, using:

```c
    #include "SerialFinder.h"
```
    
### Unity Installation
1. Copy the file **Arduino.cs** from *SerialFinder _ Unity* to your project *assets* folder. **That's all :)**

# Get Started
## Arduino
- Create a *SerialFinder* variable that is global:

```c
    #include "SerialFinder.h"
    
    SerialFinder finder;
```
    
- The **Handshake Code** is defined in the *Constructor*, in this example it's *"CODE"* :

```c
    void setup(){
    //Initialize a Serial Connection
        Serial.begin(9600);//this baudrate must match the defined at Unity's Plugin.
        
        finder = SerialFinder("CODE");
    }
```
    
- Another way to initialize the *SerialFinder* is define an *Input Handshake* and an *Output Handshake*, this way the Arduino will only respond to specific Unity Programs: 

```c
        finder = SerialFinder("Input", "Output");
```
    
- Inside the *loop* method we will respond for the first Handshake request, calling the method **findMe()**:

```c
    void loop(){
        if(!finder.findMe()){
            return; //will block the loop in this point, until receive a proper handshake
        }
        //your usual code goes here.
        Serial.println("Hello Unity");
    }
```
    
- Final Code:
- 
```c
    #include "SerialFinder.h"
    
    SerialFinder finder;

    void setup(){
      Serial.begin(9600);
      finder = SerialFinder("CODE");
    }
    
    void loop(){
      if(!finder.findMe()){
        return;
      }
      Serial.println("Hello Unity");
    }
```
    
## Unity
A good aproach is to extend the class **Arduino** (*this already extends a MonoBehavior*):

```java
using UnityEngine;
using System.Collections;
public class Arduino_Connection : Arduino{
    void Start(){
        //Open Connection Here
    }
    
    void Update(){
    
    }
}
```

##### Open Connection
The Unity Plugin provides few ways to open a connection:
1. **Simple Connection**: The first one is the most simple, where you don't really defines a HandShake. Instead, it opens a direct Serial Connetion and Handles the Thread for input/output data:

```java
    void setup(){
        Open("COM3"); //9600 bauds *default* 
    }
```
    
    1.1. If you want to use a non-default baudrate (e.g. 115200) you can open this way:
    
```java
    Open(string portName, int baudRate)
```
    
    ex.:
    
```java
    Open("COM3", 115200);
```
    
2. **Single Handshake**: this way Unity will send a *default handshake* to arduino and what to a set response:

```java
    public bool Open(string handShake, bool find)
```
    
    A good example is to give "names" to Arduino Boards, so you can specifiy wich device you want to connect, just like connect to a robot:
    
```java
    Open("Robot_0", true);
```
    
3. **Double Handshake**: this way Unity sends a code to the Arduino and waits a specific response.

```java
    public bool Open(string handShake1, string handShake2)
```
    
    ex.:
    
```java
    Open("Device_0", "Game_1")
```

##### Discovery Ports
In the case you only want to know if a port is connected and also responds to specified handshaks use one of the methods bellow, both will return "COM#" where *#* is the Port Number, or *null* if there is no device connected:

1. *Single Handshake*:

```java
	private string FindPort(string handShake)
```
2. *Double Handshake*:
```java
	private string FindPort(string handShake1 , string handShake2)
```

##### Read Data From Serial
As an extension of the *Arduino* class, the main connection class can implement the 
```java
	protected override void OnReceiveData (string receivedData)
	{
		Debug.Log (receivedData);
	}
```

#### Write Data to Serial
    Still TODO :(

### Version
>0.9

### Updates List
* (Feb 2015) - Main Classes created for Arduino
* (April 2016) - Implementation of simple connection Method

### TODO List
* Test on Linux OS
* Test on Mac OS

### License
----
Under MIT License
> Copyright (c) 2015-2016 Lucas Cassiano

> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

>The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

>THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


