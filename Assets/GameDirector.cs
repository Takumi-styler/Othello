using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    FieldData[,] fieldData = new FieldData[10,10];
    StoneRenderer stoneRenderer;
    Player[] player;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                fieldData[i, j] = new FieldData();
            }
        }
        fieldData[4, 4].StoneState = 1;
        fieldData[5, 5].StoneState = 1;
        fieldData[4, 5].StoneState = 2;
        fieldData[5, 4].StoneState = 2;
        stoneRenderer = GameObject.Find("Stage").GetComponent<StoneRenderer>();
        stoneRenderer.FieldData = fieldData;
        player = new Player1[]
        {
            new Player1(1,2),
            new Player1(2,1)
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (player[1].placeStone(ref fieldData))
        {
            stoneRenderer.FieldData = fieldData;
        }
    }
}
