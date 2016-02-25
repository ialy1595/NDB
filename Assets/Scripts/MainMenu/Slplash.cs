using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Slplash : MonoBehaviour {
    private Image image;
    private AudioSource audio;
    private int currentNum = 1;
    private int lastNum = 8;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        audio = GetComponent<AudioSource>();
        Debug.Log(image.sprite);
        image.sprite = Resources.Load<Sprite>("Opening/1 CUT") as Sprite;

    }
	
    public void OnClick()
    {
        currentNum++;
        if(currentNum > lastNum)
        {
            SceneManager.LoadScene("MainMenu");
            audio.Play();
        }
        else
        {
            image.sprite = Resources.Load<Sprite>("Opening/" + currentNum.ToString() + " CUT") as Sprite;
        }
    }
    
}
