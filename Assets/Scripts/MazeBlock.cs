using UnityEngine;
using System.Collections;


public class MazeBlock : MonoBehaviour
{
    public GameObject mazeWall;
    public Collider coll;

    public void CreateBlock(bool t, bool r, bool b, bool l, int Xpos, int Ypos, int cellNumber)
    {
        if (t)
		{
            for (int i = 0; i < 6; i++)
            {
                AddWall();
                mazeWall.transform.position = new Vector3(Xpos, 0, Ypos + (i * 2) + 8);
                mazeWall.name = "TOP" + " (cell " + cellNumber + ")";
                Instantiate(mazeWall);
            }
        }

        if (r)
        {
            for (int i = 0; i < 6; i++)
            {
                AddWall();
                mazeWall.transform.position = new Vector3(Xpos + (i * 2), 0, Ypos + 8);
                mazeWall.transform.Rotate(0, 90.0f, 0, Space.Self);
                mazeWall.name = "LEFT" + " (cell " + cellNumber + ")";
                Instantiate(mazeWall);
            }
        }

        if (b)
        {
            for (int i = 0; i < 6; i++)
            {
                AddWall();
                mazeWall.transform.position = new Vector3(0 + Xpos, 0, Ypos + (i * 2) + 8);
                mazeWall.transform.Translate(0, 0, 10);
                mazeWall.name = "BOTTOM" + " (cell " + cellNumber + ")";
                Instantiate(mazeWall);
            }
        }

        if (l)
        {
            for (int i = 0; i < 6; i++)
            {
                AddWall();
                mazeWall.transform.position = new Vector3(Xpos + (i * 2), 0,  Ypos + 8);
                mazeWall.transform.Rotate(0, 90.0f, 0, Space.Self);
                mazeWall.name = "RIGHT" + " (cell " + cellNumber + ")";
                Instantiate(mazeWall);
            }
        }
        
    }

    public void AddWall()
    {
        mazeWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mazeWall.transform.localScale += new Vector3(1, 9, 1);
        mazeWall.GetComponent<Renderer>().material.color = Color.white;
        coll = mazeWall.GetComponent<Collider>();
        coll.material.bounciness = 1;
        mazeWall.gameObject.tag = "Obstacle";
    }
}
