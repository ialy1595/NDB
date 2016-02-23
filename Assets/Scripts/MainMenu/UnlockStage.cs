using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnlockStage : MonoBehaviour {
    public int stageNumber;
	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        button.interactable = false;
        if(GameManager.gm.clearedLevel+1 >= stageNumber)
        {
            button.interactable = true;
        }
	}
	
	
}
