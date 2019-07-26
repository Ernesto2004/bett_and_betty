using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
        
public class TimingDataDisplayer : MonoBehaviour
{
    public string DataName = "displayingTime";
    // Start is called before the first frame update
    void Start()
    {
        string Data = PlayerPrefs.GetString(gameObject.name + DataName, "00:00:00");
        GetComponent<Text>().text = gameObject.name + ": " + Data;
    }
}
