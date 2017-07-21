using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BugGMBar : MonoBehaviour {

    private Text bugGMText;

	void Start () {
        bugGMText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        bugGMText.text = Developer.dev.useableDeveloperCount[0] + "/" + Developer.dev.developerCount[0];
	}
}
