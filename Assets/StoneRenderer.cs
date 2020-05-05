using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRenderer : MonoBehaviour
{
    public GameObject blackStone;
    public GameObject whiteStone;
    public FieldData[,] fieldData = new FieldData[10,10];
    StageLine stageLine;
    Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                fieldData[i, j] = new FieldData();
            }
        }
        fieldData[4, 4].StoneState = 1;
        fieldData[5, 5].StoneState = 1;
        fieldData[5, 4].StoneState = 2;
        fieldData[4, 5].StoneState = 2;
        stageLine = GetComponent<StageLine>();

        
        updateStoneRender();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateStoneRender()
    {
        deleteAllStone();

        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                vector = new Vector3((stageLine.sizex / 2) - ((stageLine.gapy * (j - 1)) + 0.3f), (stageLine.sizey / 2) - ((stageLine.gapy * (i - 1)) + 0.3f));

                if (fieldData[i, j].StoneState == 1)
                {
                    blackStone.transform.position = vector;
                    Instantiate(blackStone, transform);
                }

                if (fieldData[i, j].StoneState == 2)
                {
                    whiteStone.transform.position = vector;
                    Instantiate(whiteStone, transform);
                }

            }
        }
    }

    private void deleteAllStone()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("StoneInstance");

        foreach (GameObject gameObj in gameObjects)
        {
            Destroy(gameObj);
        }
    }
}
