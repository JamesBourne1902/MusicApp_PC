using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Song_Clicked_Script : MonoBehaviour
{
    public Logic_Script logic;
    playlistContents list1;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
    }

    public void onSongClicked()
    {
        string tag = gameObject.GetComponentInChildren<Text>().text;

        try // Fetches the playlist file if it exists
        {
            list1 = playlistContents.FromJson(File.ReadAllText(logic.path));
            if (!list1.playlist.Contains(tag))
            {
                list1.playlist.Add(tag);
                string json = list1.ToJson();
                File.WriteAllText(logic.path, json);
                gameObject.GetComponent<In_Playlist_Script>().alphaChange();
            }
        }
        catch // if the file doesnt exist it makes one
        {
            list1 = new playlistContents(); // creates new instance of the playlistContents class
            list1.playlist.Add(tag); // adds the song name ('tag') to the playlist list in the class
            string json = list1.ToJson(); // returns a jsonUtility that can be written to a json file
            makeEmptyFolder();
            File.WriteAllText(logic.path, json); // creates the json file of the instance list1 in the 'path'
            gameObject.GetComponent<In_Playlist_Script>().alphaChange();
        }
    }

    private void makeEmptyFolder()
    {
        string folderName = "Playlists";
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }
}
