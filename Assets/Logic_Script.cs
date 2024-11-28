using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Logic_Script : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject playlists;
    public GameObject musicList;
    public GameObject playlist;
    public GameObject playingUI;

    public List<GameObject> buttons = new List<GameObject>();

    public Generate_AudioClip_Script songMaker;
    public AudioClip currentSong;
    public AudioClip nextSong;

    public bool atTop = true;
    public bool atBottom = false;

    public string path;
    public string originPath;
    public string playlistName;
    public string oldName;

    public float screenScale;

    // Start is called before the first frame update
    void Start()
    {
        songMaker = GameObject.FindGameObjectWithTag("Music Manager").GetComponent<Generate_AudioClip_Script>();
        originPath = Application.persistentDataPath + "/Playlists/";
        screenScale = GameObject.Find("UI Canvas").GetComponent<RectTransform>().lossyScale.x;
    }

    public void openPlaylists()
    {
        homeScreen.SetActive(false);
        playlists.SetActive(true);
    }

    public void openSongList()
    {
        playlists.SetActive(false);
        playlist.SetActive(true);
    }

    public void returnHome()
    {
        if (playlists.activeSelf)
        {
            playlists.SetActive(false);
            homeScreen.SetActive(true);
        }
        else if (playlist.activeSelf)
        {
            playlist.SetActive(false);
            playlists.SetActive(true);
        }
        else if (playingUI.activeSelf)
        {
            playingUI.SetActive(false);
            playlist.SetActive(true);
        }
        else
        {
            musicList.SetActive(false);
            playlist.SetActive(true);
        }
    }

    public void openArtistList() // opens the list of available songs
    {
        playlist.SetActive(false);
        musicList.SetActive(true);
    }

    public void makeNewPlaylist()
    {
        PlayerPrefs.SetInt("playlist number", PlayerPrefs.GetInt("playlist_Total")+1);
        playlists.SetActive(false);
        playlist.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void refreshSongs()
    {
        destroyButtons();

        playlist.SetActive(false);
        playlist.SetActive(true);
    }

    public void playTheList()
    {
        playingUI.SetActive(true);
        playlist.SetActive(false);
    }

    public void destroyButtons() // clears the song buttons in the list of all songs
    {
        foreach (GameObject b in buttons)
        {
            Destroy(b);
        }
        buttons.Clear();
        playlist.GetComponent<Playlist_Display_Script>().transformChange = 0;
        playlist.GetComponent<Playlist_Display_Script>().index = 0;
    }

}
