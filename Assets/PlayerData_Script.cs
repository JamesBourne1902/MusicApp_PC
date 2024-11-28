using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class playlistContents
{
    public List<string> playlist = new List<string>();

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static playlistContents FromJson(string json)
    {
        return JsonUtility.FromJson<playlistContents>(json);
    }
}
