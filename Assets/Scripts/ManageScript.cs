using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageScript : MonoBehaviour
{
    public Stopwatch timer;
    public int globalCollected;
    public int BettCollected;
    public int BettyCollected;
    public GameObject fadeG;
    public Image fade;
    public Text BettGem, BettGemB;
    public Text BettyGem, BettyGemB;
    public float fadeSpeed = 2.5f;
    public GameObject ExitMenu;
    public Text timerDisplay, timerDisplayBack;

    public bool isReseting = false;
    private bool debounce = true;
    private void Start()
    {
        /*
        LOADING HIGHSCORE DATA FOR THIS LEVEL.
        */

        string displayableTime = PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "displayingTime", "00:00:00");
        print(SceneManager.GetActiveScene().name);
        int privateMSData = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "TimerMS", 100000000);
        print("The Record Time For This Level Is: " + displayableTime + "." + " In MS It Is: " + privateMSData.ToString());

        timer = new Stopwatch();
        timer.Start();
        fadeG.SetActive(true);
        StartCoroutine(FadeImage(true, "nothing"));
        PreviewUi();
    }

    private void PreviewUi()
    {
        BettGem.text = "Gems: " + BettCollected;
        BettGemB.text = "Gems: " + BettCollected;
        BettyGemB.text = "Gems: " + BettyCollected;
        BettyGem.text = "Gems: " + BettyCollected;
    }

    public void ResetTheScene(string sceneName)
    {
        StartCoroutine(FadeImage(false, sceneName));
        isReseting = true;
    }

    public void AddAGem(int toAdd, int person)
    {
        globalCollected += toAdd;

        if (person == 10)
        {
            BettCollected += toAdd;
        } else if (person == 9)
        {
            BettyCollected += toAdd;
        }
        PreviewUi();
    }

    void Update()
    {
        openExitMenu();
        if (timerDisplay != null)
        {
            string time = timer.Elapsed.ToString().Substring(0, timer.Elapsed.ToString().Length - 8);
            timerDisplay.text = time;
            timerDisplayBack.text = time;
        }
    }

    void openExitMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("Bett").GetComponent<Controller2D>().canWalk = !debounce;
            GameObject.Find("Betty").GetComponent<Controller2D>().canWalk = !debounce;

            GameObject.Find("Bett").GetComponent<Controller2D>().canJump = !debounce;
            GameObject.Find("Betty").GetComponent<Controller2D>().canJump = !debounce;
            ExitMenu.SetActive(debounce);
            debounce = !debounce;
        }
    }

    public void OnContinueMenu()
    {
        GameObject.Find("Bett").GetComponent<Controller2D>().canWalk = !debounce;
        GameObject.Find("Betty").GetComponent<Controller2D>().canWalk = !debounce;

        GameObject.Find("Bett").GetComponent<Controller2D>().canJump = !debounce;
        GameObject.Find("Betty").GetComponent<Controller2D>().canJump = !debounce;
        ExitMenu.SetActive(debounce);
        debounce = !debounce;
    }

    public void OnBackToMenu()
    {
        ExitMenu.GetComponent<Animator>().SetBool("toSlide", true);
        isReseting = true;
        StartCoroutine(FadeImage(false, "Menu"));
    }

    public void OnRetryGame()
    {
        ExitMenu.GetComponent<Animator>().SetBool("toSlide", true);
        isReseting = true;
        ResetTheScene(SceneManager.GetActiveScene().name);
    }

    public void OnLevelWin()
    {
        timer.Stop();
        int levelTimerDataInMS = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "TimerMS", 1000000000);
        if (levelTimerDataInMS > timer.ElapsedMilliseconds)
        {
            string toDisplay = timer.Elapsed.ToString().Substring(0, timer.Elapsed.ToString().Length - 8);
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "displayingTime", toDisplay);

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "TimerMS", (int)timer.ElapsedMilliseconds);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("JudgeMode", "false");
    }

    public IEnumerator FadeImage(bool fadeAway, string sceneName)
    {
        fadeG.SetActive(true);
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
        }
        ExitMenu.SetActive(false);
        if (isReseting == true)
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            fadeG.SetActive(false);
        }

    }
}
