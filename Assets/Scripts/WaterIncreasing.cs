using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIncreasing : MonoBehaviour
{
    private float Y;
    private int time;
    public float studsUp;
    public int neededTime;
    // Update is called once per frame
    void Update()
    {
        if (time >= neededTime)
        {
            Y = transform.position.y + studsUp;
            transform.position = new Vector3(transform.position.x, Y, transform.position.z);
            time = 0;
        }
        else
        {
            time++;
        }
    }
}
