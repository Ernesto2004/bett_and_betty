using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelTransition : MonoBehaviour
{
    public Text parentText;
    public string nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
       if (PlayerPrefs.GetString("JudgeMode") == "true")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        print("Transition...");
        GameObject.Find("LevelManager").GetComponent<ManageScript>().isReseting = true;
        StartCoroutine(GameObject.Find("LevelManager").GetComponent<ManageScript>().FadeImage(false, nextSceneName));
        parentText.text = "Transitioning...";
        gameObject.GetComponent<Button>().interactable = false;
    }

}
