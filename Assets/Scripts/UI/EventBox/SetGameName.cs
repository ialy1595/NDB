using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SetGameName : MonoBehaviour {

    protected void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    public void OnClick()
    {
        string name = GetComponentInChildren<InputField>().text;
        if (name == null || name == "" || name.Length > 8)
            return;
        GameManager.gm.GameName = name;
        SaveLoad.Save();
        SceneManager.LoadScene("ChooseStages");
        Destroy(this);
    }

}
