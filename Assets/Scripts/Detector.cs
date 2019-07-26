using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public bool resetable;
    public bool onlyOnetimePerGame;
    public bool resetDialogData;
    private bool debounce = true;
    public Dialogue dialogue;
    // Start is called before the first frame update
    private void Start()
    {
        if (resetDialogData)
        {
            PlayerPrefs.DeleteKey(gameObject.name);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (debounce == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (!onlyOnetimePerGame)
                {
                    FindObjectOfType<DialogueManager>().changeSentences(dialogue);
                    if (debounce == true)
                    {
                        if (resetable == false)
                        {
                            debounce = false;
                            Destroy(gameObject.GetComponent<Detector>());
                        }
                        else
                        {
                            debounce = false;
                            ResetDebounce();
                        }

                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt(gameObject.name, 0) == 0)
                    {
                        FindObjectOfType<DialogueManager>().changeSentences(dialogue);
                        if (debounce == true)
                        {
                            PlayerPrefs.SetInt(gameObject.name, 1);
                            if (resetable == false)
                            {
                                debounce = false;
                                Destroy(gameObject.GetComponent<Detector>());
                            }
                            else
                            {
                                debounce = false;
                                ResetDebounce();
                            }

                        }
                    }
                }
            }
        }
    }

    IEnumerator ResetDebounce()
    {
        yield return new WaitForSeconds(1);
        debounce = true;
    }
}
