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

    private Coroutine fadeEffectCoroutine;

    public delegate void Callback();

    void Start()
    {
        cam = GetComponent<Camera>();
        originalPos = cam.transform.position;

        timer = timeToFade;
    }

    public void FadeToBlack(float t, Callback callback)
    {
        if (black_ == null)
        {
            if (callback != null) callback();
            return;
        }
        if(fadeEffectCoroutine != null)
        {
            StopCoroutine(fadeEffectCoroutine);
            fadeEffectCoroutine = null;
        }
        fadeEffectCoroutine = StartCoroutine(FadeToBlackC(t, callback));
    }

    public void FadeFromBlack(float t, Callback callback)
    {
        if (black_ == null)
        {
            if(callback!=null) callback();

            return;
        }
        if (fadeEffectCoroutine != null)
        {
            StopCoroutine(fadeEffectCoroutine);
            fadeEffectCoroutine = null;
        }
        fadeEffectCoroutine = StartCoroutine(FadeFromBlackC(t, callback));
    }

    public IEnumerator FadeToBlackC(float t, Callback callback)
    {
        timeToFade = t;
        timer = t;

        while (timeToFade > 0.0f)
        {
            timeToFade -= Time.deltaTime;
            Color c = black_.color;
            c.a = 1f - (timeToFade/timer);
            black_.color = c;
            yield return null;
        }
        if (callback != null) callback();
        yield return null;
    }

    public IEnumerator FadeFromBlackC(float t, Callback callback)
    {
        timeToFade = t;
        timer = t;
        while (timeToFade > 0.0f)
        {
            timeToFade -= Time.deltaTime;
            Color c = black_.color;
            c.a = timeToFade / timer;
            black_.color = c;
            yield return null;
        }
        if(callback!=null) callback();
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
