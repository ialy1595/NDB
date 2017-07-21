using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserCountText : MonoBehaviour {
    private Text userCountText;
	// Use this for initialization
	void Start () {
        userCountText = GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {
        userCountText.text = "<color=#ffffff>" + GameManager.gm.UserAllCount();
        userCountText.text += " / " + Unlockables.GetInt("UserLimit") + "</color>" + " 명";
        //userCountText.text += "\n<color=#ff0000>목표 유저 수\n15000</color>";
	}
}
