using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapMove : MonoBehaviour
{
    public bool toRight;
    public bool vertical;
    public float studsAway;
    public float speed = 5f;
    public float waitTimeOnReach;
    private bool isMoving;
    Vector3 posToGo;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;   
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        if (isMoving == false)
        {
            if (vertical == false)
            {
                yield return new WaitForSeconds(waitTimeOnReach);
                if (toRight == true)
                {
                    posToGo = transform.localPosition + new Vector3(studsAway, 0, 0);
                    isMoving = true;
                }
                else
                {
                    posToGo = transform.localPosition + new Vector3(studsAway - studsAway - studsAway, 0, 0);
                    isMoving = true;
                }
            }
            else
            {
                yield return new WaitForSeconds(waitTimeOnReach);
                if (toRight == true)
                {
                    posToGo = transform.localPosition + new Vector3(0, studsAway, 0);
                    isMoving = true;
                }
                else
                {
                    posToGo = transform.localPosition + new Vector3(0, studsAway - studsAway - studsAway, 0);
                    isMoving = true;
                }
            }
        }
        else
        {
            if (transform.localPosition != posToGo)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, posToGo, speed * Time.deltaTime);
            }
            else
            {
                isMoving = false;
                toRight = !toRight;
            }
        }
    }
}
