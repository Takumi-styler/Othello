using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameDirector : MonoBehaviour
{
    FieldData[,] fieldData = new FieldData[10,10];
    StoneRenderer stoneRenderer;
    GameObject turnText;
    GameObject Win;
    Player[] player;
    bool turn = true;
    int opponentStone, stone;
    int placeableCount = 1;
    bool inGame = true;
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
        turnText = GameObject.Find("ColorText");
        Win = GameObject.Find("Win");
        Win.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            int playerNo = turn ? 0 : 1;
            opponentStone = turn ? 2 : 1;
            stone = turn ? 1 : 2;

            if (checkPlaceable() == 0)
            {
                inGame = false;
            }

            if (player[playerNo].placeStone(ref fieldData) || placeableCount == 0)
            {
                stoneRenderer.FieldData = fieldData;
                turn = !turn;
            }

            if (stone == 1)
            {
                turnText.GetComponent<Text>().text = "BLACK";
                turnText.GetComponent<Text>().color = new Color(0, 0, 0);
            }
            if (stone == 2)
            {
                turnText.GetComponent<Text>().text = "WHITE";
                turnText.GetComponent<Text>().color = new Color(255, 255, 255);
            }
        }
        else
        {
            if (countStone())
            {
                Win.GetComponent<Text>().text = "Black Wins";
                Win.GetComponent<Text>().color = new Color(0, 0, 0);
            }
            else
            {
                Win.GetComponent<Text>().text = "White Wins";
                Win.GetComponent<Text>().color = new Color(255, 255, 255);
            }
            Win.GetComponent<Text>().enabled = true;

            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    private bool countStone()
    {
        int count = 0;
        foreach (FieldData i in fieldData)
        {
            if (i.StoneState==1)
            {
                count++;
            }
        }
        return count > 32;
    }

    private int checkPlaceable()
    {
        int empty = 0;
        placeableCount = 0;
        Vector2Int vector2Int = new Vector2Int();
        for(vector2Int.x = 1; vector2Int.x < 9; vector2Int.x++)
        {
            for(vector2Int.y = 1; vector2Int.y < 9; vector2Int.y++)
            {
                fieldData[vector2Int.x, vector2Int.y].Placeable = false;
                if (fieldData[vector2Int.x, vector2Int.y].StoneState != 0) { continue; }
                placeable(vector2Int);
                empty++;
            }
        }
        return empty;
    }

    private void placeable(Vector2Int pos)
    {
        Vector2Int result = new Vector2Int(0, 0), nextblock = new Vector2Int(1, 1);

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
                                fieldData[pos.x, pos.y].Placeable = true;
                            placeableCount++;
                            }
                            if (fieldData[nextblock.x, nextblock.y].StoneState == 0) { break; }
                        }
                    }

                }
        }
    }
    private bool within1to9(Vector2Int next)
    {
        return next.x > 0 && next.x < 9 && next.y > 0 && next.y < 9;
    }
}
