using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Delete_Playlist_Script : MonoBehaviour
{
    public Logic_Script logic;
    public GameObject mainButton;
    public GameObject yesButton;
    public GameObject noButton;
    public Text theText;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        theText = mainButton.GetComponentInChildren<Text>();
    }

    private void OnDisable()
    {
        no();
    }

    public void deletePlaylist()
    {
        if (theText.text == "ARE YOU SURE?")
        {

        }
        else
        {
            theText.text = "ARE YOU SURE?";
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }
    }

    public void yes()
    {
        File.Delete(logic.path);
        logic.returnHome();
    }

    public void no()
    {
        theText.text = "DELETE PLAYLIST";
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }
}
