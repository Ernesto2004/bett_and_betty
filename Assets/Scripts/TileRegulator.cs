using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRegulator : MonoBehaviour
{
    public string layerName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (layerName != collision.gameObject.tag)
            {
                collision.gameObject.GetComponent<Controller2D>().canJump = false;
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
