using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Controller : MonoBehaviour
{
    protected abstract IEnumerator Apply(Environment environment);

    public void NewEnvironment(Environment env)
    {
        this.StopAllCoroutines();
        this.StartCoroutine(Apply(env));
    }

    protected IEnumerator Interpolate(float targetTime, float startValue, float endValue, UnityAction<float> action)
    {
        float lerpTime = 0.0f;

        while (lerpTime < targetTime)
        {
            lerpTime += Time.deltaTime;

            float percemtage = lerpTime / targetTime;

            float finalValue = Mathf.Lerp(startValue, endValue, percemtage);

            if (action != null)
                action.Invoke(finalValue);

            yield return null;
        }
    }
}
