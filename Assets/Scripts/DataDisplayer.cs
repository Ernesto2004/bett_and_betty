using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDisplayer : MonoBehaviour
{
    public Text toDisplay;
    public string DataName;
    // Start is called before the first frame update
    void Start()
    {
        toDisplay.text = PlayerPrefs.GetInt(DataName).ToString();
    }
}
