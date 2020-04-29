using UnityEngine;
using System.Collections;


public class MazeBlock : MonoBehaviour
{
    public GameObject mazeWall;
    public Collider coll;

    //  public void CreateBlock(bool t, bool l, bool b, bool r, Transform goT)
    public void CreateBlock(bool t, bool r, bool b, bool l, int Xpos, int Ypos, int cellNumber)
    {
        if (t)
		{
            AddWall();
            mazeWall.transform.position = new Vector3(Xpos, 0, Ypos);
            mazeWall.name = "TOP" + " (cell " + cellNumber + ")";
            coll = mazeWall.GetComponent<Collider>();
            coll.material.bounciness = 1;
            Instantiate(mazeWall);
        }

        if (r)
        {
            AddWall();
            mazeWall.transform.position = new Vector3(5 + Xpos, 0, 5 + Ypos);
            mazeWall.transform.Rotate(0, 90.0f, 0, Space.Self);
            mazeWall.name = "LEFT" + " (cell " + cellNumber + ")";
            coll = mazeWall.GetComponent<Collider>();

            coll.material.bounciness = 1;
            Instantiate(mazeWall);
        }

        if (b)
        {
            AddWall();
            mazeWall.transform.position = new Vector3(0 + Xpos, 0, 10 + Ypos);
            mazeWall.transform.Translate(0, 0, 10);
            mazeWall.name = "BOTTOM" + " (cell " + cellNumber + ")";
            coll = mazeWall.GetComponent<Collider>();
            coll.material.bounciness = 1;
            Instantiate(mazeWall);
        }

        if (l)
        {
            AddWall();
            mazeWall.transform.position = new Vector3(-5 + Xpos, 0, 5 + Ypos);
            mazeWall.transform.Rotate(0, 90.0f, 0, Space.Self);
            mazeWall.name = "RIGHT" + " (cell " + cellNumber + ")";
            //Rigidbody rigidBodyWall = mazeWall.AddComponent<Rigidbody>();
            //rigidBodyWall.mass = 5;
            coll = mazeWall.GetComponent<Collider>();
            coll.material.bounciness = 1;
            Instantiate(mazeWall);
        }
        
    }

    public void AddWall()
    {
        mazeWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mazeWall.transform.localScale += new Vector3(0, 9, 10);
        mazeWall.GetComponent<Renderer>().material.color = Color.white;
    }
}
