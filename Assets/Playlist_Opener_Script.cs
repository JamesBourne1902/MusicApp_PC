using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playlist_Opener_Script : MonoBehaviour
{
    public Logic_Script logic;

    void Start()
    {
        logic= GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
    }

    private void OnEnable()
    {
        try
        {
            Text name = gameObject.GetComponentInChildren<Text>();
            if (name.text == logic.oldName)
            {
                name.text = logic.playlistName;
            }
        }
        catch
        {

        }
    }

    public void buttonTest() // REWRITE, needs to pass 
    {
        string text = gameObject.GetComponentInChildren<Text>().text;
        logic.playlistName = text;

        logic.path = logic.originPath + logic.playlistName; // defines the path of the playlist
        logic.openSongList();
    }
}
