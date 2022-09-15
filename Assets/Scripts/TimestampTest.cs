using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimestampTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        long timestamp = 1000099492990814326;
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long divider = 100;
        dateTime = dateTime.AddTicks(timestamp / divider);
        Debug.Log(dateTime.ToString());
        Debug.Log(dateTime.ToLongTimeString());


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
