using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Player
{
    public Player1(int mine, int opponent)
    {
        stone = mine;
        opponentStone = opponent;
    }

    override public bool placeStone(ref FieldData[,] fieldDatas)
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスをクリックした座標
            Vector3 temp;
            temp = Input.mousePosition;
            this.clickposition = Camera.main.ScreenToWorldPoint(temp);
            Debug.Log(clickposition);
            Vector2Int calculated = new Vector2Int();
            calculated.x = calculatex(clickposition.x);
            calculated.y = calculatey(clickposition.y);
            if (fieldDatas[calculated.x,calculated.y].Placeable) {
                fieldDatas = calculateFlip(fieldDatas, calculated);
                return true;
            }
            return false;
        }
        return false;
    }
}
