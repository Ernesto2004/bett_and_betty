using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Image fade;
    public float fadeSpeed;
    public Image buttons;
    public Button Continue, NewGame, Exit;

    private Animator buttonsSlide;
    private bool debounce = true;
    private string StartLevel = "Tutorial";
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning(PlayerPrefs.GetInt("Gems", 0) + " Gems collected.");
        Debug.LogWarning(PlayerPrefs.GetInt("GemsBett", 0) + " Bett collected.");
        Debug.LogWarning(PlayerPrefs.GetInt("GemsBetty", 0) + " Betty collected.");

        buttonsSlide = buttons.gameObject.GetComponent<Animator>();
        fade.enabled = true;
        StartCoroutine(FadeImage(true, "Null", false));
        if (PlayerPrefs.GetString("Level","NoLevel") == "NoLevel")
        {
            print("No level detected.");
            Continue.interactable = false;
        }
    }


    public void OnContinue()
    {
        if (debounce)
        {
            if (PlayerPrefs.GetString("Level", "Menu") != "Ending")
            {
                string DataLevel = PlayerPrefs.GetString("Level");

                buttonsSlide.SetBool("toSlide", true);
                StartCoroutine(FadeImage(false, DataLevel, false));
                debounce = false;
            }
            else
            {
                Debug.LogWarning("Warning! Level data is set to 'Ending'");
                buttonsSlide.SetBool("toSlide", true);
                StartCoroutine(FadeImage(false, "Level1", false));
                debounce = false;
            }
        }
    }


    public void OnNewGame()
    {
        if (debounce)
        {
            string judgeMode = PlayerPrefs.GetString("JudgeMode");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("JudgeMode", judgeMode);
            PlayerPrefs.SetString("Level", StartLevel);

            OnContinue();
            debounce = false;
        }
    }

    public void OnExit()
    {
        if (debounce)
        {
            buttonsSlide.SetBool("toSlide", true);
            StartCoroutine(FadeImage(false, "Null", true));

            debounce = false;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("JudgeMode", "false");
    }

    public IEnumerator FadeImage(bool fadeAway, string LevelToLoad, bool quitOnFade)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * fadeSpeed)
            {
                fade.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }

        else
        {
            yield return new WaitForSeconds(1f);
            for (float i = 0; i <= 1; i += Time.deltaTime * fadeSpeed)
            {
                fade.color = new Color(1, 1, 1, i);
                yield return null;
            }
            if (quitOnFade)
            {
                Application.Quit();
            }
        }
        if (LevelToLoad != "Null")
        {
            SceneManager.LoadScene(LevelToLoad, LoadSceneMode.Single);
        }
    }
}
