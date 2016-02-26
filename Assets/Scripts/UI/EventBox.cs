using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventBox : MonoBehaviour {

    public GameObject Description = null;
    public bool DisableHotkey = false;

    protected void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;

        if (Description != null)
        {
            string str = Description.GetComponent<Text>().text;
            str = str.Replace("%title%", GameManager.gm.GameName);
            Description.GetComponent<Text>().text = str;
        }
        GameManager.gm.SetSE((int)SE.SEType.Dingaling);
        GameManager.gm.Pause(true);
    }

    void Update()
    {
        if (!GameManager.gm.isPaused)
        {
            GameManager.gm.Pause(true);
        }
        if (Input.GetKeyDown(KeyCode.Return) == true && DisableHotkey == false)
            OnClick();
    }

    public void OnClick()
    {
        GameManager.gm.Pause(false);
        Destroy(gameObject);
    }
}
