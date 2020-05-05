using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDirector : MonoBehaviour
{
    FieldData[,] fieldData = new FieldData[10,10];
    StoneRenderer stoneRenderer;

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
        stoneRenderer = GameObject.Find("Stage").GetComponent<StoneRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fieldData[3, 3].StoneState = 1;
            stoneRenderer.FieldData=fieldData;

        }
    }


    
}
