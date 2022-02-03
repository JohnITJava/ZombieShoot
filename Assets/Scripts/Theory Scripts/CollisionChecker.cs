using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public GameObject Floor;
    public Collider2D Col;

    private void Start()
    {        
    }

    // Методы OnTrigger и OnCollision не работают если ни на одном из пересекаемых объектов нет RigidBody!
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("OnTriggerEnter2D " + collision.gameObject.name);
        Floor.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("OnTriggerStay2D " + collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("OnTriggerExit2D " + collision.gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("OnCollisionEnter2D " + collision.gameObject.name);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        print("OnCollisionStay2D " + collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        print("OnCollisionExit2D " + collision.gameObject.name);
    }
}
