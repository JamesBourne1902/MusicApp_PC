using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Playlist_Script : MonoBehaviour
{
    public Logic_Script logic;
    public Player_Manager_Script player;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic_Script>();
        player = GameObject.FindGameObjectWithTag("playlist manager").GetComponent<Player_Manager_Script>();
    }

    public void onClick()
    {
        string name = $"PLAYLIST";
        logic.playlistName = name;
        logic.path = logic.originPath + logic.playlistName;

        player.number += 1;
        player.yChange -= 160;
        player.makeButton(player.playlistButton, name);
        player.yChange += 160;

        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-160);

        logic.makeNewPlaylist();
    }
}
