using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Playlist_Display_Script : MonoBehaviour
{
    public GameObject emptyButton;
    public GameObject addSongButton;
    public List<GameObject> buttons = new List<GameObject>();
    private List<string> songs;
    public int index = 0;
    public float transformChange = 0;
    public GameObject firstSong;
    public GameObject lastSong;
    public Logic_Script logic;
    public Scroll_Script scroll;

    public GameObject deleteButton;
    public GameObject renameButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        scroll = gameObject.GetComponent<Scroll_Script>();
        activateDisplay();
    }

    private void OnDisable()
    {
        logic.destroyButtons();
    }

    private void changePosition(GameObject child)
    {
        child.transform.position = new Vector2(child.transform.position.x-(550*logic.screenScale), child.transform.position.y - transformChange);
        index += 1;

        if (child.transform.localPosition.y < -460)
        {
            child.transform.localPosition = new Vector2(child.transform.localPosition.x, child.transform.localPosition.y - 80);
        }


        transformChange += 80 * logic.screenScale;
    }

    private void firstOrLast(GameObject child)
    {
        if (index == 0)
        {
            scroll.topSong = child;
        }

        if (child.GetComponentInChildren<Text>().text == "+ NEW SONG")
        {
            scroll.bottomSong = child;
        }
    }

    private void activateDisplay()
    {
        buttons.Clear();
        try
        {
            playlistContents data = playlistContents.FromJson(File.ReadAllText(logic.path));
            songs = new List<string>(data.playlist);

            foreach (string s in songs)
            {
                generateButton(emptyButton, songs[index]);
            }


        }
        catch
        {

        }

        generateButton(addSongButton, "+ NEW SONG");
        scroll.buttons = new List<GameObject>(buttons);
        logic.buttons = new List<GameObject>(buttons);

        renameButton.SetActive(File.Exists(logic.path));
        deleteButton.SetActive(File.Exists(logic.path));
    }

    private void generateButton(GameObject g, string title)
    {
        GameObject newChildObject = Instantiate(g, transform);
        newChildObject.GetComponentInChildren<Text>().text = title;
        firstOrLast(newChildObject);
        changePosition(newChildObject);
        buttons.Add(newChildObject);
    }
}
