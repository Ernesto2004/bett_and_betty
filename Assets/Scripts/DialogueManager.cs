using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{


    public int index;

    private bool ready = true;

    public float typingSpeed;
    public float dotwaitTime;

    public Text ContentText;
    public Text NextS;

    public AudioClip TickSFX;

    private AudioSource TickSFXs;

    public GameObject BG;
    public GameObject NB;

    public string[] storeSentences;

    private void Start()
    {
        TickSFXs = AddAudio(TickSFX);
        BG.SetActive(false);
    }

    public IEnumerator Type(string sentenceTorender)
    {
        GameObject.Find("Bett").GetComponent<Controller2D>().canWalk = false;
        GameObject.Find("Betty").GetComponent<Controller2D>().canWalk = false;

        NB.SetActive(false);
        foreach (char letter in storeSentences[index].ToCharArray())
        {
            if (ready == true)
            {
                ContentText.text += letter;
                TickSFXs.Play();
                yield return new WaitForSeconds(typingSpeed);
                if (letter.ToString() == "." || letter.ToString() == "," || letter.ToString() == "?" || letter.ToString() == ":")
                {
                    ready = false;
                    yield return new WaitForSeconds(dotwaitTime);
                    ready = true;
                }
            }  
        }
        if (index == storeSentences.Length - 1)
        {
            NextS.text = "CLOSE";
        }
        else if (storeSentences != null && ready == true)
        {
            NextS.text = "CONTINUE";
        }
        NB.SetActive(true); 
    }

    public void changeSentences(Dialogue dialogue)
    {
        storeSentences = dialogue.sentences;
        BG.SetActive(true);
        NextSentence();
    }

    public void NextSentence()
    {
        if (storeSentences != null && ready == true)
        {
          
            if (index != storeSentences.Length - 1)
            {

                index = index + 1;
                string sentence = storeSentences[index];

                ContentText.text = "";
                StartCoroutine(Type(sentence));
            }
            else if (index == storeSentences.Length - 1)
            {
                index = -1;
                ContentText.text = "";
                BG.SetActive(false);

                GameObject.Find("Bett").GetComponent<Controller2D>().canWalk = true;
                GameObject.Find("Betty").GetComponent<Controller2D>().canWalk = true;
            }
        }
    }

    AudioSource AddAudio(AudioClip Clip)
    {
        var newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = Clip;
        newAudio.playOnAwake = false;
        newAudio.volume = 0.1f;
        return newAudio;
    }

}
