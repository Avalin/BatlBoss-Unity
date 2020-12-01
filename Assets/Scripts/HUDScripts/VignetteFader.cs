using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteFader : MonoBehaviour
{
    public CanvasGroup uiElement;

    public void FadeIn(float time)
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, time));
    }

    public void FadeOut(float time)
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, time));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }
    }
}
