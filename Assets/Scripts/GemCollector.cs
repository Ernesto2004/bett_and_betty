using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    public int pointsToadd = 1;
    private bool collected = false;
    public ManageScript ManageScripts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (!collected && collision.gameObject.tag == "Player")
        {
            collected = true;
            GetComponent<AudioSource>().Play();
            ManageScripts.AddAGem(pointsToadd, collision.gameObject.layer);
            GetComponent<Animator>().SetBool("isDisappearing", true);
            Destroy(GetComponent<GemCollector>());
        }
    }
}
