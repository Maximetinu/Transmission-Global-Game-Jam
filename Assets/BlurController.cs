using UnityEngine;
using UnityEngine.PostProcessing;
using System.Collections;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class BlurController : MonoBehaviour
{

    public static BlurController instance;
    private PostProcessingBehaviour PPBehaviour;
    DepthOfFieldModel.Settings settings;
    float blurDuration = 2.0f;
    float waitBlurryTime = 4.0f;
    float initialFocalLength = 00.0f;
    float maxFocalLength = 200.0f;

    public bool ActivateBlur = false;

    private bool currentlyRunning = false;

    void OnValidate()
    {
        if (ActivateBlur && Application.isPlaying)
        {
            ShowBlur();
            ActivateBlur = false;
        }
        else if (ActivateBlur && !Application.isPlaying)
        {
            Debug.Log("Show text only available in play mode!");
            ActivateBlur = false;
        }

    }

    // Use this for initialization
    void Start()
    {
        instance = this;
        PPBehaviour = GetComponent<PostProcessingBehaviour>();

        settings = PPBehaviour.profile.depthOfField.settings;
        settings.focalLength = initialFocalLength;
        PPBehaviour.profile.depthOfField.settings = settings;
    }

    public void ShowBlur()
    {
        if (!currentlyRunning)
            StartCoroutine(BlurIn());
    }

    IEnumerator BlurIn()
    {
        currentlyRunning = true;
        float elapsed = 0.0f;
        float start = Time.time;
        do
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / blurDuration, 0, 1);
            settings.focalLength = Mathf.Lerp(initialFocalLength, maxFocalLength, normalisedTime);
            PPBehaviour.profile.depthOfField.settings = settings;
            yield return null;
        }
        while (elapsed < blurDuration);

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitBlurryTime);
        StartCoroutine(BlurOut());
    }

    IEnumerator BlurOut()
    {
        float elapsed = 0.0f;
        float start = Time.time;
        do
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / blurDuration, 0, 1);
            settings.focalLength = Mathf.Lerp(maxFocalLength, initialFocalLength, normalisedTime);
            PPBehaviour.profile.depthOfField.settings = settings;
            yield return null;
        }
        while (elapsed < blurDuration);

        currentlyRunning = false;
    }
}
