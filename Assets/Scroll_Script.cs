using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Script : MonoBehaviour
{
    public GameObject topSong;
    public GameObject bottomSong;
    public List<GameObject> buttons;

    [SerializeField]
    private bool atTop = true;

    [SerializeField]
    private bool atBottom = false;

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
            if (button.transform.localPosition.y == 100)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y + 1500);
            }

            else if (button.transform.localPosition.y == -620)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y + 160);
            }

            else
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y + 80);
            }
        }
    }


    public void moveDown()
    {
        foreach (GameObject button in buttons)
        {
            if (button.transform.localPosition.y == 1600)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, 100);
            }

            else if (button.transform.localPosition.y == -460)
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, -620);
            }

            else
            {
                button.transform.localPosition = new Vector2(button.transform.localPosition.x, button.transform.localPosition.y - 80);
            }
        }
    }

    public void bottomTopCheck()
    {
        if (topSong.transform.localPosition.y <= 100 && !atTop)
        {
            atTop = true;
        }
        else if (topSong.transform.localPosition.y > 100 && atTop)
        {
            atTop = false;
        }

        if (bottomSong.transform.localPosition.y >= -460 && !atBottom)
        {
            atBottom = true;
        }
        else if (bottomSong.transform.localPosition.y < -460 && atBottom)
        {
            atBottom = false;
        }
    }
}
