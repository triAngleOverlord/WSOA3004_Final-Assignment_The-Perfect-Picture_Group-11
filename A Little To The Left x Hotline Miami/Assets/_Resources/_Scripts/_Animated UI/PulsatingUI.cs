using UnityEngine;

public class PulsatingUI : UIAnimator
{
    private RectTransform rectTransform;
    public float speed, minSize, maxSize;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        Animate(rectTransform, speed, minSize, maxSize);
    }
}
