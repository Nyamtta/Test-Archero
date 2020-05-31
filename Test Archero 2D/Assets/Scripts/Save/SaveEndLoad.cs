using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveEndLoad
{

    public static void Save(SaveParametrs obj, string name) {

        string path = Application.dataPath + "/Saves/";
        Directory.CreateDirectory(path);

        string data = JsonUtility.ToJson(obj, true);

        File.WriteAllText(path + name + ".txt", data);

    }

    public static SaveParametrs Load(string name) {

        string path = Application.dataPath + "/Saves/";

        string data = File.ReadAllText(path + name + ".txt");

        SaveParametrs Jsdata = JsonUtility.FromJson<SaveParametrs>(data);

        return Jsdata;
    }

    public static void CheckParametrs (ref SaveParametrs data, string name) {

        string path = Application.dataPath + "/Saves/";

        if(File.Exists(path + name + ".txt") == false) {

            Save(data, name);
        }
        else {

            data = Load(name);
        }
    }


}
