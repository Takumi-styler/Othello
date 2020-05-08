using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    FieldData[,] fieldData = new FieldData[10, 10];
    public Vector3 clickposition;
    public int gap = 77;
    public float startx = 50, starty=950;
    // read only 
    protected int stone, opponentStone;

    public Player(int mine,int opponent)
    {
        stone = mine;
        opponentStone = opponent;
    }

    public Player() { }



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
    }

    // Update is called once per frame
    void Update()
    {
    }

    virtual public bool placeStone(ref FieldData[,] fieldDatas)
    {
        return false;
    }

    protected FieldData[,] calculateFlip(FieldData[,] fieldDatas, Vector2Int calculated)
    {
        fieldData = fieldDatas;
        fieldData[calculated.x, calculated.y].StoneState = stone;
        flip(calculated);
        fieldDatas = fieldData;
        return fieldDatas;
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

                        result.Set(j - pos.x, i - pos.y);
                        nextblock.Set(pos.x + result.x, pos.y + result.y);

                        while (within1to9(nextblock))
                        {
                            nextblock.Set(nextblock.x + result.x, nextblock.y + result.y);
                            if (fieldData[nextblock.x, nextblock.y].StoneState == stone)
                            {
                                for (Vector2Int k = nextblock; k != pos; k -= result)
                                {
                                    fieldData[k.x, k.y].StoneState = stone;
                                }
                                break;
                            }
                            if (fieldData[nextblock.x, nextblock.y].StoneState == 0) { break; }
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

    protected int calculatex(float posx)
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
    protected int calculatey(float posy)
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
