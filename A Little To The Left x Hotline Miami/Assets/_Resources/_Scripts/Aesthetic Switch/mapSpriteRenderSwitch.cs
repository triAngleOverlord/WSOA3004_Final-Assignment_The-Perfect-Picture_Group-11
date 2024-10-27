using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapSpriteRenderSwitch : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject glitchImage;
    private LayerMask grittyLayer;
    private LayerMask everthingElseLayer;
    void Start()
    {
        glitchImage.SetActive(false);
        grittyLayer = LayerMask.GetMask("Gritty Environment", "UI");
        everthingElseLayer = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "UI", "Obstacles", "Player", "Weapons", "Enemy", "InteractableObject");
    }

    // Update is called once per frame
    public void perfectKill()
    {
        StartCoroutine(perfectKillEffect());
    }

    public IEnumerator perfectKillEffect()
    {
        glitchImage.SetActive(true);
        yield return new WaitForSeconds(2);
        glitchImage.SetActive(false);
    }

    public void perfectLevel()
    {
        playerCamera.cullingMask = grittyLayer;
    }

    public void cameraBackToNormal()
    {
        playerCamera.cullingMask = everthingElseLayer;
    }
}
