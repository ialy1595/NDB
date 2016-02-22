using UnityEngine;
using System.Collections;
using System;

public class SaveLoad : MonoBehaviour {

    public static void Save()
    {
        int encodedlevel = ~(154321245 + GameManager.gm.clearedLevel * 4203);
        string text = GameManager.gm.GameName + "%" + encodedlevel;
        System.IO.File.WriteAllText(Application.dataPath + "\\SaveFile.txt", text);
    }

    public static bool HasSave()
    {
        return System.IO.File.Exists(Application.dataPath + "\\SaveFile.txt");
    }

    public static void Load()
    {
        string text = System.IO.File.ReadAllText(Application.dataPath + "\\SaveFile.txt");
        GameManager.gm.GameName = text.TrimStart('%');
        int encodedlevel = int.Parse(text.TrimEnd());
        GameManager.gm.clearedLevel = (~encodedlevel - 154321245) / 4203;
    }
}
