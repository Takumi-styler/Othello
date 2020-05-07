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
    const int stone = 1, opponentStone=2;

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
        placeStone();
    }

    public void placeStone()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスをクリックした座標
            this.clickposition = Input.mousePosition;
            Vector2Int calculated = new Vector2Int();
            calculated.x = calculatex(clickposition.x);
            calculated.y = calculatey(clickposition.y);
            Debug.Log(calculated.x + " " + calculated.y);
            fieldData = gameDirector.FieldData;
            fieldData[calculated.x, calculated.y].StoneState = 1;
            flip(calculated);
            gameDirector.FieldData = fieldData;
        }
    }

    private void flip(Vector2Int pos)
    {
        Vector2Int result = new Vector2Int(0,0) , nextblock = new Vector2Int(1,1);
        if (within1to9(pos))
        {
            for (int i = pos.y - 1; i <= pos.y + 1; i++)
            {
                for (int j = pos.x - 1; j <= pos.x + 1; j++)
                {
                    if (pos.x == j && pos.y == i) { continue; }
                    if (fieldData[j, i].StoneState == opponentStone)
                    {
                        Debug.Log(j + " opponent stone detect " + i);

                        result.Set(j - pos.x, i - pos.y);
                        Debug.Log(result.x + " direction confirmed " + result.y);


                        // strange from here
                        while (within1to9(nextblock))
                        {
                            nextblock.Set(pos.x + result.x, pos.y + result.y);
                            Debug.Log(nextblock.x + " next block confirmed " + nextblock.y);
                            if (fieldData[nextblock.x, nextblock.y].StoneState == stone)
                            {
                                for (Vector2Int k = nextblock; k != pos; k -= result)
                                {
                                    Debug.Log(k.x + "looping " + k.y);
                                    fieldData[k.x, k.y].StoneState = stone;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private bool within1to9(Vector2Int next)
    {
        return next.x > 0 && next.x < 9 && next.y > 0 && next.y < 9;
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
