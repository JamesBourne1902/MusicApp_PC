using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Renaming_Script : MonoBehaviour
{
    public Logic_Script logic;
    private bool inUse = false;
    private Text nameDisplay;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        nameDisplay = GameObject.Find("Name of Playlist").GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (Input.inputString.Length > 0 && inUse)
        {
            keyInput();
        }
    }

    private void OnDisable()
    {
        gameObject.GetComponentInChildren<Text>().text = "RENAME";
        inUse = false;
    }

    public void renamePlaylist()
    {
        if (!inUse)
        {
            allowTyping();
        }
        else
        {
            saveNameChange();
        }
    }

    private void saveNameChange()
    {
        try
        {
            File.Move(logic.path, logic.originPath + nameDisplay.text);
            logic.oldName = logic.playlistName;
            logic.playlistName = nameDisplay.text;
            logic.path = logic.originPath + logic.playlistName;

            inUse = false;
            gameObject.GetComponentInChildren<Text>().text = "RENAME";
        }
        catch
        {
            nameDisplay.text = "INVALID NAME";
        }
    }

    private void allowTyping()
    {
        nameDisplay.text = "TYPE NAME";
        gameObject.GetComponentInChildren<Text>().text = "SAVE";
        inUse = true;
    }

    private void keyInput() // handles the typing for the search bar
    {
        if (Input.GetKey(KeyCode.Backspace) && nameDisplay.text.Length > 0)
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
        if (nameDisplay.text == "TYPE NAME" || nameDisplay.text == "INVALID NAME")
        {
            nameDisplay.text = Input.inputString.ToUpper();
        }
        else
        {
            nameDisplay.text += Input.inputString.ToUpper();
        }
    }

    private void stringRemover()
    {
        if (nameDisplay.text == "TYPE NAME" || nameDisplay.text == "INVALID NAME")
        {
            nameDisplay.text = "TYPE NAME";
        }
        else
        {
            nameDisplay.text = nameDisplay.text.Substring(0, nameDisplay.text.Length - 1);

            if (nameDisplay.text == "")
            {
                nameDisplay.text = "TYPE NAME";
            }
        }
    }
}
