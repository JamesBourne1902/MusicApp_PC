using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Song_Assigner_Script : MonoBehaviour
{
    playlistContents playlist;
    public GameObject playlistDisplay;
    public Generate_AudioClip_Script musicMaker;
    public Logic_Script logic;

    void OnEnable()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        musicMaker = GameObject.FindGameObjectWithTag("Music Manager").GetComponent<Generate_AudioClip_Script>();

        logic.currentSong = null;
        logic.nextSong = null;

        try
        {
            playlist = playlistContents.FromJson(File.ReadAllText(logic.path));
            StartCoroutine(musicMaker.Download(playlist.playlist[0]));
        }
        catch
        {

        }
    }
}
