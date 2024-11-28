using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Song_Scroll_Script : MonoBehaviour
{
    public List<GameObject> allButtons;
    public GameObject topSong;
    public GameObject bottomSong;
    bool atTop = true;
    bool atBottom = false;

    void Update()
    {

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel != 0f)
        {
            if (scrollWheel > 0f && !atBottom)
            {
                moveUp();
            }
            else if (scrollWheel < 0f && !atTop)
            {
                moveDown();
            }
        }

        try
        {
            bottomTopChecker();
        }
        catch { }
    }

    public void moveUp()
    {
        foreach (GameObject button in allButtons)
        {
            if (button.transform.localPosition.y == 100)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y + 1500);
            }

            else if (button.transform.localPosition.y == -650)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y + 300);
            }

            else
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y + 150);
            }
        }
    }


    public void moveDown()
    {
        foreach (GameObject button in allButtons)
        {
            if (button.transform.localPosition.y == 1600)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, 100);
            }

            else if (button.transform.localPosition.y == -350)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, -650);
            }

            else
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y - 150);
            }
        }
    }

    private void bottomTopChecker()
    {
        if (topSong.transform.localPosition.y == 100 && !atTop)
        {
            atTop = true;
        }
        else if (topSong.transform.localPosition.y != 100 && atTop)
        {
            atTop = false;
        }

        if (bottomSong.transform.localPosition.y >= -350 && !atBottom)
        {
            atBottom = true;
        }
        else if (bottomSong.transform.localPosition.y < -350 && atBottom)
        {
            atBottom = false;
        }
    }
}
