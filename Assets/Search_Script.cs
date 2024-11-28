using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Search_Script : MonoBehaviour
{
    public Text searchBar;
    public GameObject filterButton;
    public Text filterText;
    public Startup_Script git;
    private List<string> fullSongList;
    private List<string> filteredSongList = new List<string>();
    public Song_Button_Maker_Script songMaker;

    void OnEnable()
    {
        searchBar = gameObject.GetComponent<Text>();
        filterText = filterButton.GetComponentInChildren<Text>();
        git = GameObject.FindGameObjectWithTag("Music Manager").GetComponent<Startup_Script>();
        songMaker = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Song_Button_Maker_Script>();

        searchBar.text = "TYPE TO SEARCH";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.inputString.Length > 0)
        {
            keyInput();
            songSearch();
        }
    }

    private void keyInput() // handles the typing for the search bar
    {
        if (Input.GetKey(KeyCode.Backspace) && searchBar.text.Length > 0)
        {
            stringRemover();

        }
        else if (!Input.GetKeyDown(KeyCode.Backspace))
        {
            stringWriter();
        }
    }

    private void stringWriter()
    {
        if (searchBar.text == "TYPE TO SEARCH")
        {
            searchBar.text = Input.inputString.ToUpper();
        }
        else
        {
            searchBar.text += Input.inputString.ToUpper();
        }
    }

    private void stringRemover()
    {
        if (searchBar.text == "TYPE TO SEARCH")
        {
            searchBar.text = "TYPE TO SEARCH";
        }
        else
        {
            searchBar.text = searchBar.text.Substring(0, searchBar.text.Length - 1);

            if (searchBar.text == "")
            {
                searchBar.text = "TYPE TO SEARCH";
            }
        }
    }

    private void songSearch() // makes a list of all the song names that start with the same characters as in the search bar
    {
        SearchList();
        songMaker.createButtons(filteredSongList);
    }

    private void SearchList() // generates the list of songs that match the search
    {
        filteredSongList.Clear();

        if (searchBar.text != "TYPE TO SEARCH")
        {
            if (filterText.text == "NAME")
            {
                nameSearch();
            }
            else if (filterText.text == "GENRE")
            {
                genreSearch();
            }
            else if (filterText.text == "ARTIST")
            {
                artistSearch();
            }
        }
        else
        {
            filteredSongList = new List<string>(songMaker.songs);
        }
    }

    public void filterChanger()
    {
        if (filterText.text == "NAME")
        {
            filterText.text = "GENRE";
        }
        else if (filterText.text == "GENRE")
        {
            filterText.text = "ARTIST";
        }
        else
        {
            filterText.text = "NAME";
        }
        songSearch();
    }

    private void nameSearch()
    {
        for (int i = 0; i < git.songInfo.GetLength(0); i++)
        {
            if (searchBar.text.Length <= git.songInfo[i][0].Length)
            {
                string temp = git.songInfo[i][0].Substring(0, searchBar.text.Length); // 0 is the starting character, searchBar.text.Length is the number of characters to use
                if (temp == searchBar.text)
                {
                    filteredSongList.Add(git.songInfo[i][0]);
                }
            }
        }
    }

    private void genreSearch()
    {
        for (int i = 0; i < git.songInfo.GetLength(0); i++)
        {
            if (searchBar.text.Length <= git.songInfo[i][1].Length)
            {
                string temp = git.songInfo[i][1].Substring(0, searchBar.text.Length);
                if (temp == searchBar.text)
                {
                    filteredSongList.Add(git.songInfo[i][0]);
                }
            }
        }
    }

    private void artistSearch()
    {
        for (int i = 0; i < git.songInfo.GetLength(0); i++)
        {
            for (int j = 0; j < git.songInfo[i].Length-2; j++)
            {
                if (searchBar.text.Length <= git.songInfo[i][j+2].Length)
                {
                    string temp = git.songInfo[i][j+2].Substring(0, searchBar.text.Length);
                    if (temp == searchBar.text)
                    {
                        filteredSongList.Add(git.songInfo[i][0]);
                    }
                }
            }
        }
    }
}
