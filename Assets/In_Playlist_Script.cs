using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class In_Playlist_Script : MonoBehaviour
{
    public Logic_Script logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        alphaChange();
    }

    public void alphaChange()
    {
        try
        {
            playlistContents lists;
            lists = playlistContents.FromJson(File.ReadAllText(logic.path));

            if (lists.playlist.Contains(gameObject.GetComponentInChildren<Text>().text))
            {
                Color test = gameObject.GetComponent<Image>().color;
                test.a = 0.5f;
                gameObject.GetComponent<Image>().color = test;
            }
            else
            {
                Color test = gameObject.GetComponent<Image>().color;
                test.a = 1f;
                gameObject.GetComponent<Image>().color = test;
            }
        }
        catch
        {

        }
    }
}
