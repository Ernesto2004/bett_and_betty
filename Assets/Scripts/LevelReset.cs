using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
    private bool triggeredOneTime = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Controller2D controller = other.gameObject.GetComponent<Controller2D>();
            controller.cam.transform.parent = GameObject.Find("Grid").transform;
            other.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            if (!triggeredOneTime == true)
            {
                triggeredOneTime = true;
                FindObjectOfType<ManageScript>().ResetTheScene(SceneManager.GetActiveScene().name);
                controller.died = true;
            }
        }
    }
}
