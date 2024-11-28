using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class File_Name_Separator_Script : MonoBehaviour
{
    public List<string> nameSeparator(string fileName)
    {
        int index = -1;
        int startPoint = 0;

        List<int> list = new List<int>();
        List<string> names = new List<string>();

        do // the 'do' causes the code to execute at least once even if the while statement is false
        {
            index = fileName.IndexOf(',', index + 1); // starts the search from the letter after each "T"

            if (index != -1)
            {
                list.Add(index);
            }

        } while (index != -1); // .IndexOf returns -1 if nothing is found which breaks out of the loop

        foreach (int i in list)
        {
            int change = i - startPoint;
            string temp = fileName.Substring(startPoint, change);
            startPoint = i + 1;

            names.Add(temp);
        }

        return names;
    }
}
