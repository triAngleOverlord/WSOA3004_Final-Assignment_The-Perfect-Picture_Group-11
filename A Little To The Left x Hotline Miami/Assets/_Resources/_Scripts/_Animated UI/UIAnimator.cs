using UnityEngine;

public abstract class UIAnimator : MonoBehaviour
{
    protected virtual void Animate(RectTransform ui, float time)
    {
        float value = Mathf.Clamp(Mathf.PingPong(Time.time, time), 0.7f, 1f);
        ui.localScale = new Vector3(value, value, value);
    }
}
