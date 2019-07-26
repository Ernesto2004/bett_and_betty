using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePliteScript : MonoBehaviour
{
    public Sprite pressedSprite, unpressedSprite;
    public bool oneTime;
    private AudioSource pressSound;
    public GameObject TileToOpen;
    private bool onePerson = true;
    private string currentStander = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && onePerson == true && currentStander == "")  
        {
            currentStander = collision.gameObject.name;
            onePerson = false;
            GetComponent<AudioSource>().Play();
            gameObject.GetComponent<SpriteRenderer>().sprite = pressedSprite;
            TileToOpen.SetActive(!TileToOpen.activeSelf);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && onePerson == false && collision.gameObject.name == currentStander && oneTime == false)
        {
            currentStander = "";
            onePerson = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = unpressedSprite;
            collision.gameObject.GetComponent<Controller2D>().isGrounded = true;
            TileToOpen.SetActive(!TileToOpen.activeSelf);

            StartCoroutine(ResetGround(collision));
        }
    }

    IEnumerator ResetGround(Collider2D collision)
    {
        yield return new WaitForSeconds(0.1f);
        collision.gameObject.GetComponent<Controller2D>().isGrounded = true;

    }
}
