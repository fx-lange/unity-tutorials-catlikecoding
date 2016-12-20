using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Sketch : MonoBehaviour {

    public Transform hours, minutes, seconds;

    private const float hoursToDegrees = 360f / 12f;
    private const float minutesToDegrees = 360f / 60f;
    private const float secondsToDegrees = 360f / 60f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DateTime time = DateTime.Now;

        hours.localRotation = Quaternion.Euler(0, 0, time.Hour * -hoursToDegrees);
        minutes.localRotation = Quaternion.Euler(0, 0, time.Minute * -minutesToDegrees);
        seconds.localRotation = Quaternion.Euler(0, 0, time.Second * -secondsToDegrees);
    }
}
