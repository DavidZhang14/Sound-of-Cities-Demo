using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public static class SaveSystem {
    public static void SaveCity(string saveName) {
        XmlSerializer serializer = new(typeof(CityData));
        string path = Application.persistentDataPath + "/save/" + saveName + ".city";
        FileStream fs = new(path, FileMode.Create);
        Vector3 characterPosition = GameObject.Find("Character").transform.position;
        CityData data = new(PlacementManager.instance.GetStructureDictionary(), characterPosition);
        serializer.Serialize(fs, data);
        fs.Close();
        Debug.Log("Save Path: " + path);
    }

    public static CityData LoadCity(string saveName) {
        string path = Application.persistentDataPath + "/save/" + saveName + ".city";
        if (File.Exists(path)) {
            XmlSerializer serializer = new(typeof(CityData));
            FileStream fs = new(path, FileMode.Open);
            CityData data = serializer.Deserialize(fs) as CityData;
            fs.Close();
            return data;
        }
        else {
            Debug.LogError("File not found: " + path);
            return null;
        }
    }

    public static void DeleteCity(string saveName) {
        string path = Application.persistentDataPath + "/save/" + saveName + ".city";
        if (File.Exists(path)) File.Delete(path);
    }
}