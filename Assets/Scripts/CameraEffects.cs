using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraEffects : MonoBehaviour
{
    Camera cam;
    Vector3 originalPos;

    [SerializeField]
    float timeToFade = 5.0f;

    private float timer;

    [SerializeField]
    Image black_;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 3.7f;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalPos = cam.transform.position;

        timer = timeToFade;
    }

    public void FadeToBlack(float t)
    {
        StartCoroutine(FadeToBlackC(t));
    }

    public void FadeFromBlack(float t)
    {
        StartCoroutine(FadeFromBlackC(t));
    }

    public IEnumerator FadeToBlackC(float t)
    {
        timeToFade = t;

        while (timeToFade > 0.0f)
        {
            timeToFade -= Time.deltaTime;
            Color c = black_.color;
            c.a = 1 - (timeToFade/timer);
            black_.color = c;
            yield return null;
        }

        yield return null;
    }

    public IEnumerator FadeFromBlackC(float t)
    {
        timeToFade = t;

        while (timeToFade > 0.0f)
        {
            timeToFade -= Time.deltaTime;
            Color c = black_.color;
            c.a = timeToFade / (255.0f * timer);
            black_.color = c;
            yield return null;
        }

        yield return null;
    }

    public void CameraShake(float shakeDuration_)
    {
        StartCoroutine(ScreenShakeC(shakeDuration_));
    }

    public IEnumerator ScreenShakeC(float t)
    {
        shakeDuration = t;

        while (shakeDuration > 0)
        {
            cam.transform.position = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime;

            yield return null;
        }
        
       shakeDuration = 0f;
       cam.transform.position = originalPos;

        yield return null;
    }
}
