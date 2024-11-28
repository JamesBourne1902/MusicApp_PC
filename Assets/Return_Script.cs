using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Return_Script : MonoBehaviour
{
    public Logic_Script logic;
    public Text nameDisplay;
    public GameObject renameButton;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        nameDisplay = GameObject.Find("Name of Playlist").GetComponentInChildren<Text>();
    }

    public void goBack()
    {
        if (logic.playlistName == "PLAYLIST" && renameButton.activeSelf == true)
        {
            nameDisplay.text = "PLEASE RENAME";
        }
        else
        {
            logic.returnHome();
        }
    }
}
