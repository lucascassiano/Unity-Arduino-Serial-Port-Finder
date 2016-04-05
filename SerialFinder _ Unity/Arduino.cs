/*
 * Arduino
 * (c) Lucas Cassiano - 2015 - lucascassiano.github.io
 * Class developed for communication with SerialFinder library for Arduino. Make sure you are NOT using .NET 2.0 Subset, change it for only .NET 2.0.
 *
 */
 
using UnityEngine;
using System.Collections;
using System;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;

public abstract class Arduino : MonoBehaviour{
	//SerialPort attributes
	private string portName = null; //non-default -- change it if needed (implement handshake on next versions)
	private int baudRate = 9600; //non-default
	protected SerialPort serial;
	protected bool isActive = false;
	protected int[] receivedData = null;
	protected bool read = false;
	protected string receivedStr = null;

	//Thread
	private Thread ioThread = null;

	public Arduino(){

	}

	/// <summary>
	/// Open a Serial Connection with the SerialPort (default is COM3)
	/// </summary>
	public bool Open()
	{

		serial = new SerialPort(portName,baudRate);
		serial.ReadTimeout = 2000;
		serial.WriteTimeout = 500;
		if(serial.IsOpen)
			return false;
		serial.Open();
		ioThread = new Thread (ProcessIO);
		ioThread.Start ();
		read = true;
		return true;

	}

	/// <summary>
	/// Open the specified portName.
	/// </summary>
	/// <param name="portName">SerialPort name.</param>
	public bool Open(string portName){
		this.portName = portName;
		return Open ();
	}

	/// <summary>
	/// Open the specified portName at baudRate.
	/// </summary>
	/// <param name="portName">Port name.</param>
	/// <param name="baudRate">Baud rate.</param>
	public bool Open(string portName, int baudRate){
		this.portName = portName;
		this.baudRate = baudRate;
		return Open ();
	}

	public bool Open(string handShake, bool find){
		if(find)
			portName = FindPort (handShake);
		if (portName == null)
			return false;
		else
			return Open ();
	}

	private string FindPort(string handShake){
		string[] portList = SerialPort.GetPortNames();
		foreach(string port in portList){
			Debug.Log("Trying open port> "+port);
			if(port!="COM1"){
				try{
					SerialPort currentPort = new SerialPort(port,baudRate);
					if(!currentPort.IsOpen){
						currentPort.Open ();
						Debug.Log("Opened port> "+port);
						currentPort.Write("connect");
						string received = currentPort.ReadLine();
						Debug.Log("Opened port> "+port+" and received= "+received);
						currentPort.Close();
						if(received.Equals(handShake)){
							return port;
						}
					}
				}
				catch(Exception e){
					//Do nothing
				}
			}

		}

		return null;
	}

    	private string FindPort(string protocol1, string handShake){
		string[] portList = SerialPort.GetPortNames();
		foreach(string port in portList){
			Debug.Log("Trying open port> "+port);
			if(port!="COM1"){
				try{
					SerialPort currentPort = new SerialPort(port,baudRate);
					if(!currentPort.IsOpen){
						currentPort.Open ();
						Debug.Log("Opened port> "+port);
						currentPort.Write(protocol1);
						string received = currentPort.ReadLine();
						Debug.Log("Opened port> "+port+" and received= "+received);
						currentPort.Close();
						if(received.Equals(handShake)){
							return port;
						}
					}
				}
				catch(Exception e){
					//Do nothing
				}
			}

		}

		return null;
	}

	private void ProcessIO(){
		while (read) {
			string incomingStr = serial.ReadLine ();
			receivedStr = incomingStr;
			OnReceiveData(incomingStr);
				//receivedData = int.Parse (incomingStr);
				isActive = true;
			}
		}


	protected abstract void OnReceiveData(string receivedData);

	void OnApplicationQuit() {
		this.Close ();
	}

	/// <summary>
	/// Close the serial connections - [IMPORTANT] -> Always close the serial connection before end program execution
	/// </summary>
	public void Close(){
		if (ioThread != null) {
			ioThread.Interrupt ();
			read = false;
			serial.Write("close");
			serial.Close ();
			serial = null;
			ioThread = null;
			isActive = false;
		}
	}


	/*Static methods */
	public static float map(float x, float  in_min, float in_max, float out_min, float out_max)
	{
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}

	public string getRawData(){
		return receivedStr;
	}

	public int GetClosestNumber(List<int> source, int value){
		int dif= 0;
		double smallestDif = 0;
		int closest = 0;
		foreach (int i in source)
		{
			dif= Math.Abs(i - value);
			if(smallestDif == 0)
				smallestDif = dif;

			if(smallestDif > dif){
				smallestDif = dif;
				closest = i;
			}
		}

		return closest;

	}
}
