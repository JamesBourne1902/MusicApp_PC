using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Generate_AudioClip_Script : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public Playing_Songs_Script playing;
    public Logic_Script logic;
    public Startup_Script startup;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        startup = gameObject.GetComponent<Startup_Script>();
    }

    public IEnumerator Download(string songName)
    {
        string dropboxPublicLink = "https://raw.githubusercontent.com/Beast-Bourne/Music_Storage/main/";
        string properName = nameCorrector(songName);
        string link = dropboxPublicLink + properName + ".mp3";

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(link, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            audioClip = DownloadHandlerAudioClip.GetContent(www);
            
            if (logic.currentSong == null)
            {
                logic.currentSong = audioClip;
            }
            else
            {
                logic.nextSong = audioClip;
            }
        }
    }
    public IEnumerator downloadAndPlay(string songName)
    {
        playing = GameObject.FindGameObjectWithTag("Player").GetComponent<Playing_Songs_Script>();
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        
        string dropboxPublicLink = "https://raw.githubusercontent.com/Beast-Bourne/Music_Storage/main/";
        string properName = nameCorrector(songName);
        string link = dropboxPublicLink + properName + ".mp3";

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(link, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            audioClip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = audioClip;
            logic.currentSong = audioClip;
            audioSource.Play();
            playing.variablePrep();
        }
    }

    private string nameCorrector(string title)
    {
        string fullName = "";
        int index = rowFinder(title);

        for (int i = 0; i < startup.songInfo[index].Length; i++)
        {
            fullName += startup.songInfo[index][i] + ",";
        }

        return fullName;
    }

    private int rowFinder(string name)
    {
        int row = 0;
        while (true)
        {
            if (Array.Exists(startup.songInfo[row], element => element == name))
            {
                return row;
            }
            else
            {
                row += 1;
                continue;
            }
        }
    }

}
