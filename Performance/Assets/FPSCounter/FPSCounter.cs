using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour {
    int fps;
    public int AverageFPS
    {
        get { return fps; }
        private set { fps = value; }
    }

    //in short: public int FPS { get; private set; }

    public int HighestFPS { get; private set; }
    public int LowestFPS { get; private set; }


    public int frameRange = 60;
    int[] fpsBuffer;
    int fpsBufferIndex;

    void InitializeBuffer()
    {
        if (frameRange <= 0)
        {
            frameRange = 1;
        }
        fpsBuffer = new int[frameRange];
        fpsBufferIndex = 0;
    }

    void UpdateBuffer()
    {
        fpsBuffer[fpsBufferIndex] = (int)(1f / Time.unscaledDeltaTime);
        fpsBufferIndex += 1;
        fpsBufferIndex %= frameRange;
    }

    void CalculateFPS()
    {
        int sum = 0;
        int highest = 0;
        int lowest = int.MaxValue;

        for (int i = 0; i < frameRange; i++)
        {
            int fps = fpsBuffer[i];
            sum += fps;

            if(fps > highest)
            {
                highest = fps;
            }

            if(fps < lowest)
            {
                lowest = fps;
            }
        }
        AverageFPS = sum / frameRange;
        HighestFPS = highest;
        LowestFPS = lowest;
    }

    // Update is called once per frame
    void Update () {
        if (fpsBuffer == null || fpsBuffer.Length != frameRange)
        {
            InitializeBuffer();
        }

        UpdateBuffer();
        CalculateFPS();
    }
}
