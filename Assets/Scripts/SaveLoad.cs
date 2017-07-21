using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

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

    public static bool Load()
    {
        string text = System.IO.File.ReadAllText(Application.dataPath + "\\SaveFile.txt");
        string pattern = @"^(.*)%(-?\d+)$";
        Match match = Regex.Match(text, pattern);
        if (match.Success)
        {
            GameManager.gm.GameName = match.Groups[1].Value;
            int encodedlevel = int.Parse(match.Groups[2].Value);
            GameManager.gm.clearedLevel = (~encodedlevel - 154321245) / 4203;
            print(GameManager.gm.clearedLevel);
            return true;
        }
        else return false;
    }

    public static void DeleteSave()
    {
        string file = Application.dataPath + "\\SaveFile.txt";
        if (System.IO.File.Exists(file))
        {
            System.IO.File.Delete(file);
        }
        else return;
    }
}
