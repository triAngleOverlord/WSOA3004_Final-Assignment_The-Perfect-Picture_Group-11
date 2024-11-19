using UnityEngine;

public class PulsatingUI : UIAnimator
{
    private RectTransform rectTransform;
    private float pulseTime = 1;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        Animate(rectTransform, pulseTime);
    }
}
