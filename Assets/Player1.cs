using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    FieldData[,] fieldData = new FieldData[10, 10];
    GameDirector gameDirector;
    public Vector3 clickposition;
    public int gap;
    public float startx = 50, starty;

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
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスをクリックした座標
            this.clickposition = Input.mousePosition;
            Debug.Log(calculatex(clickposition.x) + " " + calculatey(clickposition.y));
            fieldData = gameDirector.FieldData;
            fieldData[calculatey(clickposition.y), calculatex(clickposition.x)].StoneState = 1;
            gameDirector.FieldData = fieldData;
        }
    }

    private int calculatex(float posx)
    {
        int x = 0;
        for (int i = 1; i < 9; i++)
        {
            if (startx + (gap * (i-1)) < posx && startx + (gap * i) > posx)
            {
                x = i;
                break;
            }
        }
        return x;
    }
    private int calculatey(float posy)
    {
        int y = 0;
        for (int i = 1; i < 9; i++)
        {
            if (starty - (gap * (i - 1)) > posy && starty - (gap * i) < posy)
            {
                y = i;
                break;
            }
        }
        return y;
    }
}
