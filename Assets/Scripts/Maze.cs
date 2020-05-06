using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Maze : MonoBehaviour
{
    public int MazeSizeX = 50;
    public int MazeSizeY = 50;
    public GameObject topEdge;
    public GameObject rightEdge;
    public GameObject botEdge;
    public GameObject leftEdge;


    void Awake()
    {
        BuildMaze(this.MazeSizeX, this.MazeSizeY);
        SetFloorSize();
    }

    void SetFloorSize()
    {
        this.transform.localScale = new Vector3(26.0f, 1.0f, 27.5f);
        this.transform.position = new Vector3(120.0f, -5.0f, 132.0f);
        this.GetComponent<Renderer>().material.color = new Color(0.625f, 0.625f, 0.625f, 1.0f);
    }

    void BuildMaze(int MazeSizeX, int MazeSizeY)
	{
        AddMazeEdges();

        MazeBlock[] cells = new MazeBlock[MazeSizeX * MazeSizeY];
        for (int i = 1; i < (MazeSizeX * MazeSizeY); i++)
		{
            bool t;
            if (Random.Range(0.0f, 10.0f) > 9.33f) t = true; else t = false;
            bool r;
            if (Random.Range(0.0f, 10.0f) > 9.33f) r = true; else r = false;
            bool b;
            if (Random.Range(0.0f, 10.0f) > 9.33f) b = true; else b = false;
            bool l;
            if (Random.Range(0.0f, 10.0f) > 9.33f) l = true; else l = false;

            cells[i] = NewBlock(i, t, r, b, l, (i % MazeSizeY) * 10, (i / MazeSizeX) * 10);
            cells[i].name = "Cell" + i;
        }
	}


    MazeBlock NewBlock(int cellNum, bool top, bool left, bool bottom, bool right, int positionX, int positionY)
	{
        MazeBlock MB = gameObject.AddComponent(typeof(MazeBlock)) as MazeBlock;
        MB.CreateBlock(top, left, bottom, right, positionX, positionY, cellNum);
        return MB;
    }


    public Collider coll;
    void AddMazeEdges()
    {
        topEdge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topEdge.transform.localScale += new Vector3(1, 9, 260);
        topEdge.transform.position = new Vector3(120.0f, 0.0f, 269.0f);
        topEdge.transform.Rotate(0, 90.0f, 0, Space.Self);
        topEdge.GetComponent<Renderer>().material.color = Color.white;
        coll = topEdge.GetComponent<Collider>();
        coll.material.bounciness = 1;
        topEdge.name = "Top Edge";
        Instantiate(topEdge);

        rightEdge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightEdge.transform.localScale += new Vector3(1, 9, 273);
        rightEdge.transform.position = new Vector3(250.0f, 0.0f, 132.0f);
        rightEdge.GetComponent<Renderer>().material.color = Color.white;
        coll = rightEdge.GetComponent<Collider>();
        coll.material.bounciness = 1;
        rightEdge.name = "Right Edge";
        Instantiate(rightEdge);

        botEdge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        botEdge.transform.localScale += new Vector3(1, 9, 260);
        botEdge.transform.position = new Vector3(120.0f, 0.0f, -5.0f);
        botEdge.transform.Rotate(0, 90.0f, 0, Space.Self);
        botEdge.GetComponent<Renderer>().material.color = Color.white;
        coll = botEdge.GetComponent<Collider>();
        coll.material.bounciness = 1;
        botEdge.name = "Bottom Edge";
        Instantiate(botEdge);

        leftEdge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftEdge.transform.localScale += new Vector3(1, 9, 273);
        leftEdge.transform.position = new Vector3(-10.0f, 0.0f, 132.0f);
        leftEdge.GetComponent<Renderer>().material.color = Color.white;
        coll = leftEdge.GetComponent<Collider>();
        coll.material.bounciness = 1;
        leftEdge.name = "Left Edge";
        Instantiate(leftEdge);
    }
}
