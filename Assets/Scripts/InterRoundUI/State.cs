using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class State : MonoBehaviour {

    private Text moneyText;
    private Text developerText;
    private Text title;

	// Use this for initialization
	void Start () {
        moneyText = GameObject.Find("MoneyState").GetComponent<Text>();
        developerText = GameObject.Find("DeveloperState").GetComponent<Text>();
        title = GameObject.Find("MainTitle").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        moneyText.text = "" + GameManager.gm.Money();
        developerText.text = "" + (Developer.dev.salaryCost * (GameManager.gm.basicTime - 10));
        if (GameManager.gm.isEmergency)
            title.text = "긴급 점검";
        else
            title.text = "정기 점검";
	}
}
