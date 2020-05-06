using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10.0f;
    [SerializeField]
    private float LifeTime = 16.0f;

    Rigidbody rigidBullet;

    void Start()
    {
       Destroy(gameObject, LifeTime);
       this.GetComponent<Renderer>().material.color = Color.black;

       rigidBullet = GetComponent<Rigidbody>();
    }

    void Update()
    {
       // transform.position +=  transform.forward * Speed;
       rigidBullet.velocity = transform.forward * Speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Player" || (collision.gameObject.tag == "Tank"))) 
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.black;
            collision.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;
            Destroy(gameObject);
            Destroy(collision.gameObject, 0.5f);
        }

        else
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Speed))
            {
                Vector3 reflectDirection = Vector3.Reflect(ray.direction.normalized, hit.normal);
                transform.rotation = Quaternion.LookRotation(reflectDirection);
            } 
        }
    }

}