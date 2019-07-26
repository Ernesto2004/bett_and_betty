using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDetector : MonoBehaviour
{
    public int onPlatform = 0;
    public string nextScene;
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(detectWin());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onPlatform++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onPlatform--;
        }
    }

    IEnumerator detectWin()
    {

        yield return new WaitForSeconds(1f);
        if (onPlatform == 2)
        {

            onPlatform = 0;
            GameObject.Find("Bett").GetComponent<Controller2D>().canWalk = false;
            GameObject.Find("Bett").GetComponent<Controller2D>().canJump = false;

            GameObject.Find("Betty").GetComponent<Controller2D>().canWalk = false;
            GameObject.Find("Betty").GetComponent<Controller2D>().canJump = false;
            int GemsGlobalData = PlayerPrefs.GetInt("Gems", 0);
            int GemsGlobalDataForBett = PlayerPrefs.GetInt("GemsBett", 0);
            int GemsGlobalDataForBetty = PlayerPrefs.GetInt("GemsBetty", 0);
            PlayerPrefs.SetInt("Gems", (FindObjectOfType<ManageScript>().globalCollected + GemsGlobalData));
            PlayerPrefs.SetInt("GemsBett", (FindObjectOfType<ManageScript>().BettCollected + GemsGlobalDataForBett));
            PlayerPrefs.SetInt("GemsBetty", (FindObjectOfType<ManageScript>().BettyCollected + GemsGlobalDataForBetty));
            PlayerPrefs.SetString("Level", nextScene);
            FindObjectOfType<ManageScript>().ResetTheScene(nextScene);
            FindObjectOfType<ManageScript>().OnLevelWin();
        }
    }
}
