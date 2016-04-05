Unity Android Serial Finder
=================
A simple API designed to simplify the communication between Unity 3D and Arduino Compatible Boards. 

The basis of this API is that both Arduino and Unity projects share a **Handshake** code, it is basically a 
# Installation
Download the directory from this [Github Page](https://github.com/lucascassiano/Unity-Arduino-Serial-Port-Finder/) and follow the steps on bellow (for each Arduino and Unity 3D).


### Arduino Installation
1. Copy the files inside the folder **SerialFinder _ Arduino** to your Arduino Code's Folder, for example called "mySketch".
2. Then inside your "/mySketch/" directory you must have the following files:
   - *SerialFinder.cpp*
   - *SerialFinder.h*
   - *keywords.txt*
3. In your sketch's main code include the library, using:
    ```C++ 
    #include "SerialFinder.h"
    ```
    
### Unity Installation
1. Copy the file **Arduino.cs** from *SerialFinder _ Unity* to your project *assets* folder. **That's all :)**

# Get Started
## Arduino
- Create a *SerialFinder* variable that is global:
    ```C++
    #include "SerialFinder.h"
    
    SerialFinder finder;
    ```
- In order to initialize 
    *mySketch.ino*
    ```C++
    #include "SerialFinder.h"
    
    SerialFinder finder;
    
    void setup(){
      Serial.begin(9600);
      //Protocol will receive from unity, what will send for Unity
      //You can change both Protocols, but be sure to change both on Unity as well
      //finder = SerialFinder("hey arduino","whats up unity");
      finder = SerialFinder("JAN");
      //You can implement more protocols under this line
      //SerialFinder will write directly into your serial connection
    }
    
    void loop(){
      //Will yield until connection be stabilshed
      if(!finder.findMe()){
        return;
      }
      // Your code here
      Serial.println("Hello Unity");
    }
    ```

### Version
>1.0

### Updates List
* (Feb 2015) - Main Classes created for Arduino
* (April 2016) - Implementation of simple connection Method

### License
----
Under MIT License
> Copyright (c) 2015-2016 Lucas Cassiano

> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

>The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

>THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


