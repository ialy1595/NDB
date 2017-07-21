using UnityEngine;
using System.Collections;

public class MoneyPanel : MonoBehaviour {

    private static GameObject _this;

    public static void Hide(bool hide)
    {
        _this.SetActive(!hide);
    }

    void Start()
    {
        _this = gameObject;
        Hide(true);
    }

}
