using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class State : MonoBehaviour {

    public static State state;
    private Text moneyText;
    private Text developerText;
    private static Text upgradeText;
    private Text title;

	// Use this for initialization
	void Start () {
        state = this;
        moneyText = GameObject.Find("MoneyState").GetComponent<Text>();
        developerText = GameObject.Find("DeveloperState").GetComponent<Text>();
        upgradeText = GameObject.Find("UpgradeState").GetComponent<Text>();
        title = GameObject.Find("MainTitle").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        moneyText.text = "" + GameManager.gm.Money();
        //developerText.text = "" + (Developer.dev.salaryCost * (GameManager.gm.basicTime - 10));
        developerText.text = "" + Developer.dev.FindPostByPostID(Developer.dev.FindPostIDByName("Debugging")).DeveloperInPost();
        if (GameManager.gm.isEmergency)
            title.text = "긴급 점검";
        else
            title.text = "정기 점검";
	}
    public void refreshUpgrade(string upgrade)
    {
        if (upgradeText.text == "<없음>")
            upgradeText.text = upgrade;
        else
            upgradeText.text += "\n" + upgrade;
    }
}
