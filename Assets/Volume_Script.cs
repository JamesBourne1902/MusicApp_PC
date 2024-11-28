using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Volume_Script : MonoBehaviour
{
    public Text volumePercent;
    public AudioSource song;
    private int volumePercentValue;

    // Start is called before the first frame update
    void Start()
    {
        song = GetComponentInParent<AudioSource>();
        volumePercent = GameObject.Find("Percentage").GetComponent<Text>();

        volumePercentValue = PlayerPrefs.GetInt("volume", 100);
        writeAndSave(volumePercentValue);
    }

    public void addOne()
    {
        if (volumePercentValue != 100)
        {
            volumePercentValue += 1;
            writeAndSave(volumePercentValue);
        }
    }

    public void addTen()
    {
        if (volumePercentValue <= 90)
        {
            volumePercentValue += 10;
            writeAndSave(volumePercentValue);
        }
        else
        {
            volumePercentValue = 100;
            writeAndSave(volumePercentValue);
        }
    }

    public void removeOne()
    {
        if (volumePercentValue != 0)
        {
            volumePercentValue -= 1;
            writeAndSave(volumePercentValue);
        }
    }

    public void removeTen()
    {
        if (volumePercentValue >= 10)
        {
            volumePercentValue -= 10;
            writeAndSave(volumePercentValue);
        }
        else
        {
            volumePercentValue = 0;
            writeAndSave(volumePercentValue);
        }
    }

    private void writeAndSave(int volume)
    {
        PlayerPrefs.SetInt("volume", volume);
        volumePercent.text = volume.ToString() + "%";
        float test = volume;
        song.volume = test/100;
    }
}
