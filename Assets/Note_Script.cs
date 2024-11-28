using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Script : MonoBehaviour
{
    float xCoords;
    float yCoords;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1);
        resetNote();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > 9.5 || gameObject.transform.position.y > 5.6)
        {
            resetNote();
        }
    }

    void randomXValue() // changes the x spawn coordinate
    {
        xCoords = gameObject.transform.position.x + Random.Range(3, 15);
        yCoords = -6;
    }

    void randomYValue() // changes the y spawn coordinate
    {
        xCoords = -9.5f;
        yCoords = gameObject.transform.position.y + Random.Range(0, 9);
    }

    void resetNote()
    {
        randomiser();
        gameObject.transform.position = new Vector2(xCoords, yCoords);
    }

    void randomiser()
    {
        int number = Random.Range(1, 3);

        if (number == 1)
        {
            randomXValue();
        }
        else
        {
            randomYValue();
        }
    }
}
