using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name_Display_Script : MonoBehaviour
{
    public Text nameText;
    public Logic_Script logic;

    private void OnEnable()
    {
        nameText = gameObject.GetComponentInChildren<Text>();
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();

        nameText.text = logic.playlistName.ToUpper();
    }
}
