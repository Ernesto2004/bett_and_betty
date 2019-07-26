using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort : MonoBehaviour
{
    public int[] arrayList;
    // Start is called before the first frame update
    void Start()
    {
        arrayList[0] = 4;
        arrayList[1] = 12;
        arrayList[2] = 32;
        arrayList[3] = 7;
        arrayList[4] = 2;
        for (int i = 0; i < arrayList.Length; i++)
        {
            for (int j = i + 1; j < arrayList.Length; j++)
            {
                if (arrayList[j] > arrayList[i])
                {
                    int temp = arrayList[i];
                    arrayList[i] = arrayList[j];
                    arrayList[j] = temp;
                }
            }
        }

        for (int i = 0; i < arrayList.Length; i++)
        {
            print(arrayList[i].ToString());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
