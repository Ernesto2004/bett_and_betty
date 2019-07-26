using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingReset : MonoBehaviour
{
    public Image fade;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeImage(true));
        StartCoroutine(FadeImage(false));
    }

  

    public IEnumerator FadeImage(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * 1.5f)
            {
                fade.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }

        else
        {
            yield return new WaitForSeconds(21f);
            for (float i = 0; i <= 1; i += Time.deltaTime * 1.5f)
            {
                fade.color = new Color(1, 1, 1, i);
                yield return null;
            }
            PlayerPrefs.SetString("Level", "Level1");
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
