using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Remove_Song_Script : MonoBehaviour
{
    public Logic_Script logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
    }

    public void removeSong()
    {
        string name = gameObject.GetComponentInChildren<Text>().text;

        playlistContents songList = playlistContents.FromJson(File.ReadAllText(logic.path));
        songList.playlist.Remove(name);
        string json = songList.ToJson();
        File.WriteAllText(logic.path, json);
        logic.refreshSongs();
    }
}
