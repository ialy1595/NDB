using UnityEngine;
using System.Collections;

public class InstantiateBox : MonoBehaviour {

    public GameObject Box;
    public void OnClick()
    {
        Instantiate(Box);
    }
}
