using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeTrigger : MonoBehaviour
{

    public static TextMesh narrativeText;
    public NarrativeSet mySet;
    float fadeInDuration = 3.0f;
    float readDuration = 2.0f;
    float fadeOutDuration = 3.0f;

    public AudioClip TextSound;

    public bool ActivateText;

    private bool currentlyRunning = false;

    // Use this for initialization
    void Start()
    {
        if (narrativeText == null)
        {
            narrativeText = GameObject.FindGameObjectsWithTag("NarrativeText")[0].GetComponent<TextMesh>();
            if (narrativeText == null) Debug.Log("Canvas > Text no encontrado! Taggear con NarrativeText");
        }
    }

    void OnValidate()
    {
        if (ActivateText && Application.isPlaying)
        {
            ShowNarrative();
            ActivateText = false;
        }
        else if (ActivateText && !Application.isPlaying)
        {
            Debug.Log("Show text only available in play mode!");
            ActivateText = false;
        }

    }

    public void ShowNarrative()
    {
        AudioSource.PlayClipAtPoint(TextSound, transform.position);
        if (!currentlyRunning)
            StartCoroutine(ShowText());

        BlurController.instance.ShowBlur();
    }

    IEnumerator ShowText()
    {
        currentlyRunning = true;
        string randomTextFromSet = mySet.set[Random.Range(0, mySet.set.Length - 1)];

        narrativeText.text = randomTextFromSet;
        narrativeText.GetComponent<MeshRenderer>().material.SetFloat("_Level", 1.0f);

        float start = Time.time;
        float elapsed = 0f;
        do
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / fadeInDuration, 0, 1);
            narrativeText.GetComponent<MeshRenderer>().material.SetFloat("_Level", Mathf.Lerp(1.0f, 0.0f, normalisedTime));
            yield return null;
        }
        while (elapsed < fadeInDuration);

        StartCoroutine(WaitForRead());
    }

    IEnumerator WaitForRead()
    {
        yield return new WaitForSeconds(readDuration);
        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        //narrativeText.color = endingColor;

        float start = Time.time;
        float elapsed = 0f;
        do
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / fadeOutDuration, 0, 1);
            narrativeText.GetComponent<MeshRenderer>().material.SetFloat("_Level", Mathf.Lerp(0.0f, 1.0f, normalisedTime));
            yield return null;
        }
        while (elapsed < fadeOutDuration);
        narrativeText.text = "";
        //GetComponent<ParticleSystem>().Stop();
        GetComponent<ParticleSystem>().Stop();
        currentlyRunning = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"){
            ShowNarrative();
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<SpotLightController>().TurnOff();
        }
    }
}
