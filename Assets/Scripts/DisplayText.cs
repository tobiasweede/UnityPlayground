using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DisplayText : MonoBehaviour
{
    public TMP_Text date;
    public TMP_Text dilation;

    float slowUpdateRate = 0.2f;

    private MovingAverage dilationAverage = new MovingAverage();

    void Start()
    {
        InvokeRepeating("SlowUpdate", 0.0f, slowUpdateRate);
    }


    // Update is called once per frame
    void Update()
    {
        dilationAverage.ComputeAverage(RandomPupilDilation());
    }

    void SlowUpdate()
    {
        date.text = DateTime.Now.ToString();
        dilation.text = Math.Round(dilationAverage.Average, 3).ToString();
    }

    decimal RandomPupilDilation()
    {
        return (decimal)UnityEngine.Random.value;
    }

    // void OnGUI() 
    // {
    //     GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
    // }
}
