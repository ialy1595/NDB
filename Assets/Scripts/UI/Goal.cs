using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Goal : MonoBehaviour {

	private Text goalText;

    void Start () {
        goalText = GetComponent<Text>();
        if (GameManager.gm.currentStageScene == "Stage1")
        {
            goalText.text = "목표 : <color=#ffffff>유저 수 15000 달성</color>";
        }
        else if (GameManager.gm.currentStageScene == "Stage2")
        {
            goalText.text = "목표 : <color=#ffffff>경쟁작보다 먼저\n인기도 50000 달성 </color>";
        }
	}
}
