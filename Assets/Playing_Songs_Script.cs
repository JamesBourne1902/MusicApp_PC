using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.ComponentModel;
using UnityEngine.UI;

public class Playing_Songs_Script : MonoBehaviour
{
    int songIndex;
    string currentSong;
    public bool songPlaying;
    public float songLength;
    public float counter;
    bool shuffle = false;
    bool shuffleChangeAvailable = true;

    playlistContents lists;
    public Generate_AudioClip_Script music;
    public Logic_Script logic;
    public AudioSource songTrack;
    List<string> songList = new List<string>();

    public GameObject songTitle;
    public Sprite playing;
    public Sprite paused;
    public GameObject pauseButton;
    public GameObject shuffleText;
    public GameObject shuffleButton;
    public GameObject previousSongButton;

    void OnEnable()
    {
        songLength = 1000;
        music = GameObject.FindGameObjectWithTag("Music Manager").GetComponent<Generate_AudioClip_Script>();
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        songIndex = 0;
        counter = 0;

        
        lists = playlistContents.FromJson(File.ReadAllText(logic.path));

        songList = new List<string>(lists.playlist);
        Application.runInBackground = true;
        playSong();
    }

    void OnDisable()
    {
        songTrack.Stop();
        Application.runInBackground = false;
    }

    void Update() //keeps track of the timer through the song, auto plays the next when the current on is done
    {
        if (counter < songLength && songPlaying)
        {
            counter += Time.deltaTime;
        }
        else if (counter >= songLength)
        {
            counter = 0;
            nextSong();
        }

        if (logic.currentSong == null && shuffleChangeAvailable)
        {
            shuffleButton.SetActive(false);
            shuffleChangeAvailable = false;
        }
        else if (!shuffleChangeAvailable && logic.currentSong != null)
        {
            shuffleButton.SetActive(true);
            shuffleChangeAvailable = true;
        }
    }

    public void playSong() // plays the song stored in logic.currentSong
    {
        currentSong = songList[songIndex];
        setSongText(currentSong);

        if (logic.currentSong != null)
        {
            songTrack.Stop();
            songTrack.clip = logic.currentSong;
            songTrack.Play();
            incrementIndex();
            variablePrep();
        }
        else
        {
            songTrack.Stop();
            incrementIndex();
            StartCoroutine(music.downloadAndPlay(currentSong));
        }
    }

    public void variablePrep() // run when a song is about to play. sets certain variables to correct values
    {
        counter = 0;
        songLength = logic.currentSong.length;
        songPlaying = true;
        prepNextSong();
    }

    public void prepNextSong() // downloads and stores the next song to be played
    {
        StartCoroutine(music.Download(songList[songIndex]));
    }

    public void incrementIndex() // changes the song index ready for the next song to be downloaded.
    {
        if (!shuffle)
        {
            songIndex += 1;

            if (songIndex == songList.Count)
            {
                songIndex = 0;
            }
        }
        else
        {
            songList.Remove(currentSong);

            if (songList.Count == 0)
            {
                songList = new List<string>(lists.playlist);
                songList.Remove(currentSong);
            }

            songIndex = Random.Range(0, songList.Count);
        }
    }

    public void shuffleChange() // This runs when the shuffle button is clicked
    {
        StopAllCoroutines();

        if (shuffle)
        {
            shuffle = false;
            shuffleText.GetComponent<Text>().text = "SHUFFLE OFF";
            songIndex = lists.playlist.IndexOf(currentSong);
            songList = new List<string>(lists.playlist);
            previousSongButton.SetActive(true);
            prepNextSong();
        }
        else
        {
            shuffle = true;
            shuffleText.GetComponent<Text>().text = "SHUFFLE ON";
            int index = songList.IndexOf(currentSong);
            previousSongButton.SetActive(false);

            for (int i = 0; i < index; i++)
            {
                songList.Remove(songList[0]);
            }
            incrementIndex();
            prepNextSong();
        }
    }

    public void nextSong() // the function that will play the next song in the list
    {
        StopAllCoroutines();
        resetPauseButton();

        if (logic.nextSong != null)
        {
            logic.currentSong = logic.nextSong;
            logic.nextSong = null;
            playSong();
        }
        else
        {
            logic.currentSong = null;
            playSong();
        }
    }

    public void setSongText(string name) // Sets the display to the current song name
    {
        string textStuff = "Currently playing\n\n" + $"{name}";
        songTitle.GetComponent<Text>().text = textStuff.ToUpper();
    }

    public void playAndPause() // pauses and plays the song when the pause button is clicked
    {
        if (songTrack.isPlaying)
        {
            songTrack.Pause();
            songPlaying = false;
            pauseButton.GetComponent<Image>().sprite = paused;
        }
        else
        {
            songTrack.UnPause();
            songPlaying = true;
            resetPauseButton();
        }
    }

    private void resetPauseButton() // changes the sprite of the pause button. needed in case of skip song being clicked while the current song is paused.
    {
        pauseButton.GetComponent<Image>().sprite = playing;
    }

    public void prevSong() // plays the previous song in the list
    {
        StopAllCoroutines();
        resetPauseButton();
        songLength = 1000;

        if (songIndex == 1)
        {
            songIndex = songList.Count - 1;
        }
        else if (songIndex == 0)
        {
            songIndex = songList.Count - 2;
        }
        else
        {
            songIndex -= 2;
        }

        logic.currentSong = null;
        playSong();
    }
}
