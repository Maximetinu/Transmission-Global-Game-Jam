using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{

    private float LightIntensity;
    public float TurningDuration = 2.0f;

    // Use this for initialization
    void Start()
    {
        LightIntensity = GetComponent<Light>().intensity;
        StartCoroutine(TurnOnLight());
    }

    public void TurnOn(){
        StartCoroutine(TurnOnLight());
    }

    public void TurnOff(){
        StartCoroutine(TurnOffLight());
    }

    IEnumerator TurnOnLight(){
        float elapsed = 0.0f;
        float start = Time.time;
        GetComponent<Light>().intensity = 0.0f;
        do
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / TurningDuration, 0, 1);
            GetComponent<Light>().intensity = Mathf.Lerp(0.0f, TurningDuration, normalisedTime);
            yield return null;
        }
        while (elapsed < TurningDuration);
    }

    IEnumerator TurnOffLight()
    {
        float elapsed = 0.0f;
        float start = Time.time;
        GetComponent<Light>().intensity = LightIntensity;
        do
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / TurningDuration, 0, 1);
            GetComponent<Light>().intensity = Mathf.Lerp(TurningDuration, 0.0f, normalisedTime);
            yield return null;
        }
        while (elapsed < TurningDuration);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
