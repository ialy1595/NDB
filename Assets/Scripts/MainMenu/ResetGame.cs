using UnityEngine;
using System.Collections;

public class ResetGame : MonoBehaviour {

	
    public void Reset()
    {
        SaveLoad.DeleteSave();
    }
}
