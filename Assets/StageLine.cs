using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLine : MonoBehaviour
{
    public GameObject line;
    GameObject backgroundStage;
    Vector3[] pos = new Vector3[2];
    float sizex,sizey,gapx,gapy;

    // Start is called before the first frame update
    void Start()
    {
        backgroundStage = GameObject.Find("OthelloBackground");
        sizex = backgroundStage.transform.localScale.x;
        sizey = backgroundStage.transform.localScale.y;
        gapx = sizex / 8;
        gapy = sizey / 8;
        for (int i = 0; i < 9; i++)
        {
            //horizontal
            line.GetComponent<LineRenderer>().positionCount = 2;
            pos[0] = new Vector3((sizex / 2), (sizey / 2) - (gapy * i));
            pos[1] = new Vector3(-(sizex / 2), (sizey / 2) - (gapy * i));
            line.GetComponent<LineRenderer>().SetPosition(0, pos[0]);
            line.GetComponent<LineRenderer>().SetPosition(1, pos[1]);
            Instantiate(line);
            //vertical
            line.GetComponent<LineRenderer>().positionCount = 2;
            pos[0] = new Vector3((sizex / 2) - (gapx * i), (sizey / 2));
            pos[1] = new Vector3((sizex / 2) - (gapx * i), -(sizey / 2));
            line.GetComponent<LineRenderer>().SetPosition(0, pos[0]);
            line.GetComponent<LineRenderer>().SetPosition(1, pos[1]);
            Instantiate(line);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
