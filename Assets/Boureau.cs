using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO.Ports;


[System.Serializable]
public struct Vibratation
{
	public int intensity;
	public float duration;

	public Vibratation(int i, float t)
	{
		this.intensity = i;
		this.duration = t;
	}

	public void SubTime(float t)
    {
		this.duration -= t;
    }
}








public class Boureau : MonoBehaviour
{
	public static Boureau instance;

	[SerializeField]
	private int maxAirblow;

	[SerializeField]
	private List<Vibratation> vibers = new List<Vibratation>();

	private int currentIntensity = 0;

	private bool connected = false;

	private bool gatling = false;

	SerialPort serial;


	void Start()
	{
		instance = this;
		string the_com = "C";


		foreach (string mysps in SerialPort.GetPortNames()) // on cherche le dernier port com de la liste
		{
			print(mysps);
			if (mysps != "COM1")
			{
				the_com = mysps; // on récupère le dernier port com
				break;
			}
		}
		serial = new SerialPort("\\\\.\\" + the_com, 1000000);


		if (!serial.IsOpen)
		{
			print("Opening " + the_com + ", baud 1000000");
			serial.Open();
			serial.ReadTimeout = 100;
			serial.Handshake = Handshake.None;
			if (serial.IsOpen) { print("Open"); connected = true; }
		}
	}


	void Update()
	{
		Debug.Log("Current Vibration Intensity : " + currentIntensity);
		for (int i = vibers.Count - 1; i >= 0; i--)
		{
			Vibratation temp = vibers[i];
			temp.duration -= Time.deltaTime * 1000;
			vibers[i] = temp;

			if (vibers[i].duration <= 0.0f)
			{
				vibers.RemoveAt(i);
				if (vibers.Count > 0)
				{
					Vibre(vibers[0].intensity, (int)vibers[0].duration);
					currentIntensity = vibers[0].intensity;
				}
			}
		}

		if (vibers.Count == 0)
        {
			if (gatling)
			{
                RegisterViber(10, 100);
				currentIntensity = 10;
            }
			else
			{
				Debug.Log("None");
                Vibre(0, 0);
                currentIntensity = 0;
            }
		}
	}

	// ferme le port série en sortie du programme
	void OnApplicationQuit()
	{
		print("Closing Serial Port");
		serial.Close();
	}



	private void Vibre(int intensity, int time)
	{
		serial.Write("Vibrate" + intensity + "_" + time + "\n");
	}

	public void Airblow(int time)
	{
		serial.Write("Airshoot" + Random.Range(0, maxAirblow) + "_" + time + "\n");
	}



	// Lit i sur le port série
	private void ReadVal()
	{
		byte[] msg = { 0x00, 0x00 };

		if (serial.BytesToRead >= 2)
		{
			serial.Read(msg, 0, 2);
		}
	}

	public void SetGatling(bool value)
	{
		gatling = value;
	}





	public Vibratation RegisterViber(int intensity, int time)
    {
		Debug.Log(intensity + " ; " + time);
		Vibratation v = new Vibratation(intensity, time);
		vibers.Add(v);
		vibers.Sort(SortByIntensity);
		vibers.Reverse();
		if (vibers[0].intensity > currentIntensity)
		{
			Debug.Log("VIBRRATE");
			currentIntensity = vibers[0].intensity;
			Vibre(vibers[0].intensity, (int)vibers[0].duration);
		}

		return v;
	}



	static int SortByIntensity(Vibratation v1, Vibratation v2)
	{
		return v1.intensity.CompareTo(v2.intensity);
	}
}
