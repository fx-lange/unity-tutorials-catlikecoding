using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour {

    public Text averageFPSLabel, highestFPSLabel, lowestFPSLabel;
    FPSCounter fpsCounter;

    //private so that it won't show up in the global namespace. Make it serializable so that it can be exposed by the Unity editor.
    [System.Serializable]
    private struct FPSColor
    {
        public Color color;
        public int minimumFPS;
    }

    //We'd typically add a public field for that, but we can't do that because the struct itself is private. 
    //So make the array private as well and give it the SerializeField attribute so Unity exposes it in the editor and saves it.
    [SerializeField]
    private FPSColor[] coloring;

    void Awake()
    {
        fpsCounter = GetComponent<FPSCounter>();

    }

    void Update()
    {
        Display(averageFPSLabel,fpsCounter.AverageFPS);
        Display(lowestFPSLabel, fpsCounter.LowestFPS);
        Display(highestFPSLabel, fpsCounter.HighestFPS);
    }

    void Display(Text label, int fps)
    {
        label.text = Mathf.Clamp(fps, 0, 99).ToString();
        for (int i = 0; i < coloring.Length; i++)
        {
            if (fps >= coloring[i].minimumFPS)
            {
                label.color = coloring[i].color;
                break;
            }
        }
    }
}
