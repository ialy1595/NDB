using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundStartScene : MonoBehaviour {

    public GameObject Description;
    void Start()
    {
        string str = Description.GetComponent<Text>().text;
        str = str.Replace("%title%", GameManager.gm.GameName);
        Description.GetComponent<Text>().text = str;
        GetComponent<EventBox>().DisableHotkey = true;
    }

    public void OnClick()
    {
        GameManager.gm.DoRoundStartEvent();
    }
}
