using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForTrent : MonoBehaviour
{
    public KeyCode walkLeftKey, walkRightKey;
    public AudioClip landingSound, walkingSound;

    private AudioSource landingSoundS;
    private AudioSource walkingSoundS;

    private int timeSpent;
    private int timeDefault = 15;

    public bool isGrounded;

    private void Start()
    {
        landingSoundS = AddAudio(landingSound);
        walkingSoundS = AddAudio(walkingSound);
    }
    // Update is called once per frame
    void Update()
    {
        if (timeSpent >= timeDefault && isGrounded)
        {
            if (Input.GetKey(walkLeftKey) || Input.GetKey(walkRightKey))
            {

                timeSpent = 0;
                walkingSoundS.Play();

            }
        }
        else
        {

            timeSpent++;
        }
        print(isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            landingSoundS.Play();
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            isGrounded = false;
    }

    AudioSource AddAudio(AudioClip Clip)
    {
        var newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = Clip;
        newAudio.playOnAwake = false;
        return newAudio;
    }
}
