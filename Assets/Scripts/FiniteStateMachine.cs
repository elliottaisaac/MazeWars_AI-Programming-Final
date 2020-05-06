using UnityEngine;
using System.Collections;


public class FiniteStateMachine : FSM 
{
    public enum FSMState
    {
        None,
        Search,
        Attack,
        Dodge,
    }

    public GameObject[] tankBots;
    public float RandomPoint;

    public GameObject endPoint;

    //Current state that the NPC is reaching
    public FSMState curState;

    private float speed;

    //Tank Rotation Speed
    private float rotSpeed;

    //Bullet
    public GameObject Bullet;
    private int loadedBullets = 5;

    // We overwrite the deprecated built-in `rigidbody` variable.
    new private Rigidbody rigidbody;


    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize () 
    {
        curState = FSMState.Search;
        speed = 20.0f;
        rotSpeed = 2.0f;
        elapsedTime = 0.0f;
        shootRate = 3.0f;
        //elapsedRayTime = 0.0f;
        //rayRate = 4.0f;

        RandomPoint = Mathf.Floor(Random.Range(0.0f, 4.9f));
        endPoint = GameObject.FindGameObjectWithTag("End" + this.name);

        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        tankBots = GameObject.FindGameObjectsWithTag("Tank");
        playerTransform = objPlayer.transform;

        // Get the rigidbody
        rigidbody = GetComponent<Rigidbody>();

        if(!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        //Get the turret of the tank
        turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = turret.GetChild(0).transform;

    }

    //Update each frame
    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Search: UpdateSearchState(); break;
           case FSMState.Attack: UpdateAttackState(); break;
           //case FSMState.Dodge: UpdateDodgeState(); break;
        }

        //Update the time
        elapsedTime += Time.deltaTime;
    }

    /// <summary>
    /// Search state
    /// </summary>
    protected void UpdateSearchState()
    {
        switch (RandomPoint)
        {
            case 0: endPoint.transform.position = new Vector3(115, 0, 120); break;
            case 1: endPoint.transform.position = new Vector3(205, 0, 50); break;
            case 2: endPoint.transform.position = new Vector3(205, 0, 200); break;
            case 3: endPoint.transform.position = new Vector3(55, 0, 200); break;
            case 4: endPoint.transform.position = new Vector3(55, 0, 50); break;
            default: endPoint.transform.position = new Vector3(115, 0, 120); break;
        }

        if (Vector3.Distance(transform.position, endPoint.transform.position) < 10)
        {
            RandomPoint++;
            if (RandomPoint > 4) RandomPoint = 0;
        }

            foreach (GameObject tank in tankBots)
        {
            float distance = Vector3.Distance(transform.position, tank.transform.position);
            if (distance > 5 && distance < 150)  //&& elapsedRayTime >= rayRate
            {
                RaycastHit checkForShot;
                Ray ray = new Ray(transform.position, transform.position - tank.transform.position);

                if (Physics.Raycast(ray, out checkForShot, 200))
                {
                    if (checkForShot.collider.gameObject.CompareTag("Tank") && checkForShot.collider.gameObject != this)
                    {
                        endPoint.transform.position = tank.transform.position;

                        //Rotate to the target point
                        Quaternion targetRotation = Quaternion.LookRotation(transform.position - tank.transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);

                        curState = FSMState.Attack;
                    }
                }
               // elapsedRayTime = 0.0f;
            }
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(10);
        loadedBullets = 5;
    }


    /// <summary>
    /// Attack state
    /// </summary>
    protected void UpdateAttackState()
    {
        float distance = Vector3.Distance(transform.position, endPoint.transform.position);
        //Transition to search 
        if (distance > 200.0f) curState = FSMState.Search;

        //Always Turn the turret towards the player
        Quaternion turretRotation = Quaternion.LookRotation(turret.position - endPoint.transform.position);
        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, turretRotation, Time.deltaTime * rotSpeed);
        turret.transform.LookAt(endPoint.transform.position);
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (elapsedTime >= shootRate && loadedBullets > 0)
        {
            Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            elapsedTime = 0.0f;

            loadedBullets--;

            if (loadedBullets == 0) StartCoroutine(ReloadGun());
        }
    }
    

}
