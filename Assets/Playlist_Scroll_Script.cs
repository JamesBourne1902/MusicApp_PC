using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlist_Scroll_Script : MonoBehaviour
{
    public GameObject topSong;
    public GameObject bottomSong;
    public List<GameObject> buttons;
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
            bottomTopCheck();
        }
        catch
        {

        }
    }

    public void moveUp()
    {
        foreach (GameObject button in buttons)
        {
            if (button.transform.localPosition.y == 300)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y + 1500);
            }

            else
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y + 160);
            }
        }
    }


    public void moveDown()
    {
        foreach (GameObject button in buttons)
        {
            if (button.transform.localPosition.y == 1800)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, 300);
            }

            else
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y - 160);
            }
        }
    }

    public void bottomTopCheck()
    {
        if (topSong.transform.localPosition.y == 300 && !atTop)
        {
            atTop = true;
        }
        else if (topSong.transform.localPosition.y != 300 && atTop)
        {
            atTop = false;
        }

        if (bottomSong.transform.localPosition.y >= -500 && !atBottom)
        {
            atBottom = true;
        }
        else if (bottomSong.transform.localPosition.y < -500 && atBottom)
        {
            atBottom = false;
        }
    }
}
