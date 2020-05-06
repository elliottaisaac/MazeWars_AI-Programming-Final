using UnityEngine;
using System.Collections;

public class EnemyTank : MonoBehaviour
{
    public GameObject Bullet;
    private Transform Turret;
    private Transform bulletSpawnPoint;
    private float speed, rotSpeed;
    private int loadedBullets = 5;

    public Node startNode { get; set; }
    public Node goalNode { get; set; }
    public ArrayList pathArray;
    public float pathRadius = 2.2f;
     public bool isLooping = true;

    private int curPathIndex;
    private float pathLength;
    private Node targetPoint;


    void Start()
    {
        rotSpeed = 30.0f;
        speed = 20.0f;
        curPathIndex = 0;

        Turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = Turret.GetChild(0).transform;
    }


    void Update()
    {
        startNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(this.transform.position)));
        goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(GameObject.FindGameObjectWithTag("End" + this.name).transform.position)));
        pathArray = AStar.FindPath(startNode, goalNode);
        DrawPath();

        targetPoint = (Node)pathArray[curPathIndex + 1];

        //If reach the radius within the path then move to next point in the path
        if (Vector3.Distance(transform.position, targetPoint.position) < pathRadius)
        {
            //Don't move the vehicle if path is finished 
            if (curPathIndex < pathLength - 1)
                curPathIndex++;
            else if (isLooping)
                curPathIndex = 0;
            else return;
        }

        this.transform.rotation = Quaternion.LookRotation(targetPoint.position);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPoint.position), Time.deltaTime * rotSpeed);
        this.transform.LookAt(targetPoint.position);
        this.transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, Time.deltaTime * speed);
    }


    void DrawPath()
    {
        if (pathArray == null)
            return;

        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach (Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node nextNode = (Node)pathArray[index];
                    Debug.DrawLine(node.position, nextNode.position, Color.red);
                    index++;
                }
            };
        }
    }
}


/* Node[] SetPath(Node startNode, Node goalNode)
{
  pathArray = AStar.FindPath(startNode, goalNode);

  pathLength = pathArray.Count;
  path = new Node[pathArray.Count];

  int i = 0;
  foreach (Node node in pathArray)
  {
      Node pathNode = (Node)pathArray[i];
      path[i] = pathNode;
      i++;
  }

  for (int i = 0; i < pathArray.Count; i++)
  {
      path[i] = (Vector3)pathArray[i].transform.position;
  }

  return path;
}*/


