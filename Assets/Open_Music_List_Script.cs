using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Music_List_Script : MonoBehaviour
{
    public GameObject musicList;
    public GameObject playlistList;
    Logic_Script logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        playlistList = GameObject.FindGameObjectWithTag("Playlist Display");

        musicList = logic.musicList;
    }

    public void openMusicList()
    {
        foreach (GameObject b in logic.buttons)
        {
            Destroy(b);
        }
        logic.buttons.Clear();
        playlistList.GetComponent<Playlist_Display_Script>().transformChange = 0;
        playlistList.GetComponent<Playlist_Display_Script>().index = 0;

        playlistList.SetActive(false);
        musicList.SetActive(true);
    }
}
