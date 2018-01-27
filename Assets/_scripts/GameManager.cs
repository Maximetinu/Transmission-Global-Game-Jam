using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
       
        DontDestroyOnLoad(gameObject);
    }


    void Start ()
    {
        Physics.gravity = new Vector3(0, 20.0F, 0);
        StartGame();
    }
	

	void Update () {
	}
    

    public void StartGame()
    {
        StartCoroutine(FadeIn());
    }


    public IEnumerator FadeOut()
    {
         float fadeTime = GetComponent<Fading>().BeginFade(1);
         yield return new WaitForSeconds(fadeTime);
    }
    

    public IEnumerator FadeIn() { 
        float fadeTime = GetComponent<Fading>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }
}
