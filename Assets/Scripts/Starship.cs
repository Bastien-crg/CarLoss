using Kryz.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starship : MonoBehaviour
{
    public delegate float EasingFuncDelegate(float t);
    public float translationSpeed;

    void Start()
    {
        StartCoroutine(WaitBeforeLiftOff());
    }

    IEnumerator WaitBeforeLiftOff()
    {
        yield return new WaitForSeconds(10);
        Vector3 endPos = transform.position;
        endPos.y = 10000;
        yield return StartCoroutine(TranslationCoroutine(transform, transform.position, endPos, translationSpeed, EasingFunctions.InQuad));

    }

    public static IEnumerator TranslationCoroutine(Transform transform,
        Vector3 startPos, Vector3 endPos, float translationSpeed,
        EasingFuncDelegate easingFunc = null)
    {
        if (translationSpeed <= 0) yield break;

        float elapsedTime = 0;
        float duration = Vector3.Distance(startPos, endPos) / translationSpeed;

        while (elapsedTime < duration)
        {
            float k = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPos, endPos, easingFunc != null ? easingFunc(k) : k);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
