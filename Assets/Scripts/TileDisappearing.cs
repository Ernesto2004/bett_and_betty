using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDisappearing : MonoBehaviour
{
    public float cycleOffset;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cycle());   
    }

    IEnumerator Cycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(cycleOffset);

            target.SetActive(!target.activeSelf);
        }
    }
}
