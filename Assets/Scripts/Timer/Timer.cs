using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    float ms;
    float ss;
    float mm;

    Text text;

    void OnEnable()
    {
        text = GetComponent<Text>();
        ms = 0;
        ss = 0;
        mm = 0;
    }

    void FixedUpdate()
    {
        ms += Time.deltaTime;
        if(ms > 1)
        {
            ms = 0;
            ss++;
            if(ss > 60)
            {
                ss = 0;
                mm++;
            }

            text.text = "Time: " + mm + ":" + ss;
        }
    }
}
