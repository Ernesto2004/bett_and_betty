using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeModeActivation : MonoBehaviour
{
    private string[] judgeActivationPattern;
    private int index;
    private bool canActivate = true;
    public GameObject judgeNote;
    private void Start()
    {
        if (PlayerPrefs.GetString("JudgeMode") == "true")
        {
            judgeNote.SetActive(true);
            canActivate = false;
        }
        print(PlayerPrefs.GetString("JudgeMode", "false"));
        judgeActivationPattern = new string[] { "j", "u", "d", "g", "e" };
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        detectCode();   
    }

    void detectCode()
    {
        if (canActivate == true)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(judgeActivationPattern[index]))
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }

            if (index == judgeActivationPattern.Length)
            {
                if (PlayerPrefs.GetString("JudgeMode", "false") == "false")
                {
                    print("Judge mode activated.");
                    PlayerPrefs.SetString("JudgeMode", "true");
                    judgeNote.SetActive(true);
                    canActivate = false;
                    index = 0;
                }
            }
        }
    }
}
