using UnityEngine;
using System.Collections;

public class bugtext : MonoBehaviour {

    private MeshRenderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<MeshRenderer>();
        rend.sortingLayerName = "Default";
        rend.sortingOrder = 11;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
