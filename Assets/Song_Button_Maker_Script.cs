using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class Song_Button_Maker_Script : MonoBehaviour
{
    public Logic_Script logic;
    public Startup_Script git;
    public Song_Scroll_Script scroller;
    public GameObject emptyButton;
    public List<GameObject> buttons = new List<GameObject>();
    public List<string> songs = new List<string>();
    public int index = 0;
    public float transformChange = 0;
    public GameObject firstSong;
    public GameObject lastSong;

    public File_Name_Separator_Script test;

    // Start is called before the first frame update
    void OnEnable()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        git = GameObject.FindGameObjectWithTag("Music Manager").GetComponent<Startup_Script>();
        scroller = gameObject.GetComponent<Song_Scroll_Script>();

        createList();
        createButtons(songs);
    }

    private void changePosition(GameObject child)
    {
        child.transform.position = new Vector2(child.transform.position.x, child.transform.position.y - transformChange);
        index += 1;

        if (index == 4)
        {
            transformChange += 300 * logic.screenScale;
        }
        else
        {
            transformChange += 150 * logic.screenScale;
        }
    }

    private void firstOrLast(GameObject child, List<string> list)
    {
        if (index == 0)
        {
            firstSong = child;
            scroller.topSong = child;
        }
        if (index == list.Count - 1)
        {
            lastSong = child;
            scroller.bottomSong = child;
        }
    }

    private void destroyObjects()
    {
        foreach (GameObject g in buttons)
        {
            Destroy(g);
        }
        buttons.Clear();
    }

    public void createButtons(List<string> songList)
    {
        destroyObjects(); 
        transformChange = 0;
        index = 0;

        foreach (string s in songList)
        {
            GameObject newChildObject = Instantiate(emptyButton, transform);
            newChildObject.GetComponentInChildren<Text>().text = s;
            firstOrLast(newChildObject, songList);
            changePosition(newChildObject);
            buttons.Add(newChildObject);
        }

        scroller.allButtons = new List<GameObject>(buttons);
    }

    private void createList()
    {
        songs.Clear();

        for (int i = 0; i < git.songInfo.GetLength(0); i++)
        {
            songs.Add(git.songInfo[i][0]);
        }
    }
    
}
