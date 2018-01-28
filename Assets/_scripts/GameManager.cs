using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public GameObject LlamaPrefabReference;

    public AudioClip llamaClip;

    public GameObject key;
    public GameObject boss;

    public void SpawnKey(){
        key.SetActive(true);
    }

    public void SpawnBoss(){
        boss.SetActive(true);
    }

    public void Win(){
        Debug.Log("!!!Has GANADO!!!");
    }

    public void GameOver(){
        StartCoroutine(GameManager.instance.FadeOut());
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
       
        //DontDestroyOnLoad(gameObject);
    }


    void Start ()
    {
        Physics.gravity = new Vector3(0, 20.0f, 0);
        StartGame();
    }
    

    public void StartGame()
    {
        StartCoroutine(FadeIn());
    }


    public IEnumerator FadeOut()
    {
         float fadeTime = GetComponent<Fading>().BeginFade(1);
         yield return new WaitForSeconds(fadeTime);
         SceneManager.LoadScene("NewMain");
    }
    

    public IEnumerator FadeIn() { 
        float fadeTime = 0.0f;
        if (GetComponent<Fading>() != null)
            fadeTime = GetComponent<Fading>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }
}
