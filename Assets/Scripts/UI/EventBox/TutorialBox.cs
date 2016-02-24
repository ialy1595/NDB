using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialBox : MonoBehaviour {

    public List<GameObject> tutorials;
    public int tutorialStep;

    private int cnt;

    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;

        for (int i = 0; i < tutorials.Count; i++)
        {
            tutorials[i].GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        }

        tutorials[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        cnt = 0;

        GameManager.gm.SetSE((int)SE.SEType.Dingaling);
        GameManager.gm.Pause(true);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            tutorials[cnt].GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            cnt++;

            if (cnt == tutorials.Count)
            {
                GameManager.gm.Pause(false);
                GameManager.gm.isTutorialCleared[tutorialStep] = true;
                Destroy(gameObject);
            }

            GameManager.gm.SetSE((int)SE.SEType.Dingaling);
            tutorials[cnt].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
}
