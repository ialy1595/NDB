using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SE : MonoBehaviour
{
    public enum SEType
    {
        Perchase = 0,
        Pop = 1,
        Click_Cute = 2,
        Click_Retro = 3,
        Click_Reject = 4,
        Click_Clean = 5,
        Dingaling = 6,
        Notification = 7,
        ItemUse = 8,
        Bug_Appear = 9,
    };

    //public static SE se;

    public List<AudioClip> SEs = new List<AudioClip>();

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetSE(int index)
    {
        audioSource.clip = SEs[index];
        audioSource.Play();
    }
}
