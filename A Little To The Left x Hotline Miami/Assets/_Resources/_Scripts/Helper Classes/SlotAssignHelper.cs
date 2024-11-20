using UnityEngine;

public class SlotAssignHelper : MonoBehaviour
{
    public static SlotAssignHelper Instance;

    [Header("Player Helper")]
    public AudioSource Sfx;

    public AudioSource loadSFx;
    public Transform meleeAttackRange;

    //[Header("Enemy Helper")]


    [Header("Sound Clips")]
    public AudioClip whoosh;
    public AudioClip slash;
    public AudioClip chainsaw;
    public AudioClip shotgun;
    public AudioClip warArms;
    public AudioClip handguns;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}