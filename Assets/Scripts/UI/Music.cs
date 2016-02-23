using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Music : MonoBehaviour {

    public enum MusicType //buildSetting에 맞춰서
    {
        MainMenu = 0,
        InterRound = 3,
        InGame = 4,
    }

    public List<AudioClip> bgms = new List<AudioClip>();

    private int previndex = -1;

    private AudioSource audioSource;

	void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
        previndex = -1;
	}

    public void setAudio(int index)
    {
        if (previndex == index)
        {
            Debug.Log("music not change");
            return;
        }
        else
        {
            audioSource.clip = bgms[index];
            audioSource.Play();
            previndex = index;
        }
    }

}
