using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("Music"))
        {
            gameObject.tag = "Music";
            DontDestroyOnLoad(gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
}
