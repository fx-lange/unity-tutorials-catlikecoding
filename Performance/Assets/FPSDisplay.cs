using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour {

    public Text averageFPSLabel, highestFPSLabel, lowestFPSLabel;
    FPSCounter fpsCounter;

    void Awake()
    {
        fpsCounter = GetComponent<FPSCounter>();

    }

    void Update()
    {
        averageFPSLabel.text = Mathf.Clamp(fpsCounter.AverageFPS, 0, 99).ToString();
        lowestFPSLabel.text = Mathf.Clamp(fpsCounter.LowestFPS, 0, 99).ToString();
        highestFPSLabel.text = Mathf.Clamp(fpsCounter.HighestFPS, 0, 99).ToString();
    }
}
