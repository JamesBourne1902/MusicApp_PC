using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using UnityEngine.UIElements;

public class Player_Manager_Script : MonoBehaviour
{
    public Logic_Script logic;
    public Playlist_Scroll_Script scroller;

    public GameObject playlistButton;
    public GameObject makeNewButton;

    public float yChange = 0;
    public int number = 0;

    public List<GameObject> buttonList = new List<GameObject>();

    // Start is called before the first frame update
    private void OnEnable()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        scroller = gameObject.GetComponent<Playlist_Scroll_Script>();
        PlayerPrefs.SetInt("playlist_Total", 0);

        DestroyAllButtons();
        readAllLists();
        scroller.buttons = new List<GameObject>(buttonList);
    }

    // increments through the already created playlists and generates a button to access them
    // when there are no more playlists it creates the 'make new' button
    private void readAllLists()
    {
        while (true)
        {
            try
            {
                string[] filenames = Directory.GetFiles(logic.originPath);
                string name = Path.GetFileName(filenames[number]);
                logic.path = logic.originPath + name;

                playlistContents list = playlistContents.FromJson(File.ReadAllText(logic.path));
                number += 1;
                makeButton(playlistButton, name.ToUpper());
                continue;
            }
            catch
            {
                PlayerPrefs.SetInt("playlist_Total", number);
                makeButton(makeNewButton, "MAKE NEW");
                break;
            }
        }
    }

    public void makeButton(GameObject g, string text) // generates a prefab gameObject and changes its text
    {
        GameObject newChildObject = Instantiate(g, transform);
        newChildObject.GetComponentInChildren<Text>().text = text;
        changePosition(newChildObject);
        firstOrLast(newChildObject);
        buttonList.Add(newChildObject);
    }

    private void changePosition(GameObject child) // sets the position of the gameObject
    {
        child.transform.position = new Vector2(child.transform.position.x, child.transform.position.y  - yChange);
        yChange += 160 * logic.screenScale;
    }

    private void DestroyAllButtons() // removes all the buttons so it refreshes each time its opened
    {
        foreach (GameObject button in buttonList)
        {
            Destroy(button);
        }
        buttonList.Clear();

        yChange = 0;
        number = 0;
    }

    private void firstOrLast(GameObject child)
    {
        if (number == 1)
        {
            scroller.topSong = child;
        }

        if (child.GetComponentInChildren<Text>().text == "MAKE NEW")
        {
            scroller.bottomSong = child;
        }
    }
}
