using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapSpriteRenderSwitch : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject glitchPanel;
    private LayerMask grittyLayer;
    private LayerMask everthingElseLayer;
    void Start()
    {
        glitchPanel.SetActive(false);
        grittyLayer = LayerMask.GetMask("Gritty Environment", "UI");
        everthingElseLayer = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "UI", "Obstacles", "Player", "Weapons","Enemy", "InteractableObject");
    }

    // Update is called once per frame
    public void switchMap()
    {
        StartCoroutine(mapSwitch(playerCamera));
    }

    public IEnumerator mapSwitch(Camera cam)
    {
        cam.cullingMask = (grittyLayer);
        glitchPanel.SetActive(true);
        Debug.Log("switched");
        yield return new WaitForSeconds(2);
        cam.cullingMask = (everthingElseLayer);
        glitchPanel.SetActive(false);
    }
}
