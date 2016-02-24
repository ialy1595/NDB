using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitGameNameText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text myGameName = GetComponent<Text>();
        myGameName.text = "내 게임 이름 : " + GameManager.gm.GameName;
	}
	
	
}
