using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Song_Display_Script : MonoBehaviour
{
    private List<GameObject> children = new List<GameObject>();
    public GameObject addSongButton;
    public GameObject returnButton;
    public GameObject playButton;

    // OnEnable is called every time the object is set to Active.
    void OnEnable()
    {
        activeDisplay();
    }

    private void OnDisable()
    {
        foreach (GameObject child in children)
        {
            child.gameObject.SetActive(false);
        }
        children.Clear();
    }

    private void scrollSetter(GameObject theSong)
    {
        Scroll_Script[] scripts = GameObject.FindObjectsOfType<Scroll_Script>();
        foreach (Scroll_Script script in scripts)
        {
            script.topSong = theSong;
        }
    }

    public void activeDisplay()
    {
        int number = PlayerPrefs.GetInt("playlist number");
        string path = Application.persistentDataPath + $"/list{number}.json";
        float yPosition = 80;
        bool set = false;
        addSongButton.SetActive(true);
        returnButton.SetActive(true);
        playButton.SetActive(true);

        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        try
        {
            playlistContents data = playlistContents.FromJson(File.ReadAllText(path));
            List<string> names = new List<string>();

            foreach (string song in data.playlist)
            {
                foreach (GameObject display in children)
                {
                    if (song == display.name.Replace(" PDT", "").ToUpper())
                    {
                        display.SetActive(true);
                        names.Add(display.name);
                    }
                }
            }

            names.Remove("Exit Button");
            names.Remove("Add Song Button");
            names.Remove("Play Button");
            names.Remove("Song Title Text");

            foreach (string songName in names)
            {
                foreach (GameObject song in children)
                {
                    if (song.name == songName)
                    {
                        song.transform.localPosition = new Vector2(song.transform.localPosition.x, yPosition);
                        yPosition -= 180;

                        if (!set)
                        {
                            scrollSetter(song);
                            set = true;
                        }
                    }
                }
            }
            addSongButton.transform.localPosition = new Vector2(addSongButton.transform.localPosition.x, yPosition);
        }
        catch
        {

        }
    }
}
