using UnityEngine;

public abstract class UIAnimator : MonoBehaviour
{
    protected virtual void Animate(RectTransform ui, float speed, float minSize, float maxSize)
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        t = Mathf.SmoothStep(0f, 1f, t);

        float value = Mathf.Lerp(minSize, maxSize, t);
        ui.localScale = new Vector3(value, value, value);
    }
}
