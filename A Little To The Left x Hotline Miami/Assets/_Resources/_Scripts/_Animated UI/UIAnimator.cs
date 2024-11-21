using UnityEngine;

public abstract class UIAnimator : MonoBehaviour
{
    protected virtual void Animate(RectTransform ui, float speed, float minSize, float maxSize, float rotationRange)
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        t = Mathf.SmoothStep(0f, 1f, t);

        float value = Mathf.Lerp(minSize, maxSize, t);
        ui.localScale = new Vector3(value, value, value);

        float randomRotation = Mathf.PerlinNoise(Time.time * speed, 0f) * rotationRange - (rotationRange / 2f);
        ui.localRotation = Quaternion.Euler(0f, 0f, randomRotation);
    }
}
