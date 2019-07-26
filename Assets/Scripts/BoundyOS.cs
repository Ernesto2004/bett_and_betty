using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundyOS : MonoBehaviour
{

    public float ESpeed = 4f;
    public float MMTOGO = 10f;
    private bool debounce = false;
    private bool going = false;
    private Vector3 PosToGo;
    private bool ready = true;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().speed = (ESpeed / ESpeed);
    } 

    // Update is called once per frame
    void Update()
    {
        Setup();
    }
    void Setup()
    {
        if (going == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            if (debounce == false)
            {
                PosToGo = new Vector3(transform.localPosition.x + MMTOGO, transform.position.y, transform.localPosition.z);

                debounce = true;
                going = true;
            }
            else
            {
                PosToGo = new Vector3(transform.localPosition.x - MMTOGO, transform.position.y,  transform.localPosition.z);
                debounce = false;
                going = true;
            }
        }
        else
        {
            if (transform.position == PosToGo)
            {
                going = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, PosToGo, ESpeed * Time.deltaTime);
                GetComponent<Animator>().SetFloat("Speed", PosToGo.x);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && ready == true)
        {
            ready = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
