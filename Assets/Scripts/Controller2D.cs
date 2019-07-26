// By Ernesto July, 11, 2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    // public variables
    public float moveSpeed = 8f;
    public float jumpPower = 18f;
    public float climbSpeed = 5f;
    public bool died = false;
    public bool canJump;
    public bool canWalk;

    public GameObject cam;
    // KeyCodes
    public KeyCode toJump;
    public KeyCode goLeft;
    public KeyCode goRight;
    public KeyCode climbUp;
    public KeyCode climbDown;
    // private variables
    private Animator Anim;
    public bool isGrounded;
    private bool isWalking;
    private Vector3 movement;
    private float defaultGravity;
    private void Start()
    {
        Anim = GetComponent<Animator>();
        defaultGravity = GetComponent<Rigidbody2D>().gravityScale;
    }

    private void Update()
    {
        isWalking = false;
        if (canWalk == true)
        {
            Anim.SetFloat("VelocityY", GetComponent<Rigidbody2D>().velocity.y);
            // CALCULATING
            if (Input.GetKey(goLeft))
            {
                transform.position += Vector3.left * Time.deltaTime * moveSpeed;
                // MOVING THE PLAYER
                GetComponent<SpriteRenderer>().flipX = true;

                isWalking = true;
            }
            else if(Input.GetKey(goRight))
            {
                transform.position += Vector3.right * Time.deltaTime * moveSpeed;
                // MOVING THE PLAYER
                GetComponent<SpriteRenderer>().flipX = false;

                isWalking = true;
            }
            // RUNS OR STOPS THE WALK ANIM
            if (isWalking)
            {
                Anim.SetFloat("Speed", 1f);
            }
            else
            {
                Anim.SetFloat("Speed", 0f);
                GetComponent<SpriteRenderer>().flipX = false;
            }

            // JUMP OS
            if (Input.GetKeyDown(toJump) && canJump == true)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        if (isGrounded == true)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Finish")
        {
            GetComponent<BoxCollider2D>().enabled = true;
            isGrounded = true;
            canJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Finsih")
        {
            isGrounded = false;
        }
        else if (collision.gameObject.tag == "Ladder")
        {
            // SETS THE GRAVITY TO 0
            GetComponent<Rigidbody2D>().gravityScale = defaultGravity;
            // ALLOWS PLAYER TO JUMP
            canJump = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder" && Input.GetKey(climbUp))
        {
            canJump = false;
            // SETS THE GRAVITY TO 0
            GetComponent<Rigidbody2D>().gravityScale = 0f;

            // WHOLE SCRIPT
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, climbSpeed);

        }
        else if (collision.gameObject.tag == "Ladder" && Input.GetKey(climbDown))
        {
            canJump = false;
            // SETS THE GRAVITY TO 0
            GetComponent<Rigidbody2D>().gravityScale = 0f;

            // WHOLE SCRIPT
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -climbSpeed);
        }
        else
        {
            if (collision.gameObject.tag == "Ladder")
            {
                canJump = false;
                // SETS THE GRAVITY TO 0
                GetComponent<Rigidbody2D>().gravityScale = 0f;

                // STOPS THE PLAYER ON THE LADDER
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    AudioSource AddAudio(AudioClip Clip)
    {
        var newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = Clip;
        newAudio.playOnAwake = false;
        return newAudio;
    }
}
