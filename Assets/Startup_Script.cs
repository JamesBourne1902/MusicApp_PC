using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup_Script : MonoBehaviour
{
    public File_Name_Separator_Script separator;
    public GitHubFileList git;

    public string[][] songInfo;

    void Start()
    {
        separator = gameObject.GetComponent<File_Name_Separator_Script>();
        git = gameObject.GetComponent<GitHubFileList>();

    }

    public void listSplitter()
    {
        songInfo = new string[git.fileList.Count][];

        for (int i = 0; i < git.fileList.Count; i++)
        {
            List<string> temp = separator.nameSeparator(git.fileList[i]);
            songInfo[i] = new string[temp.Count];

            for (int j = 0; j < temp.Count; j++)
            {
                songInfo[i][j] = temp[j];
            }
        }
    }
}
