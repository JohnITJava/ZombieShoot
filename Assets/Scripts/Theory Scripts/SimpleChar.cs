using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleChar : MonoBehaviour
{

    public float Speed;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed,0, 0);

        /*
        if (Input.GetButton("Horizontal"))
            print("Horrozontal was pressed");

        
        if (Input.GetKey(KeyCode.A))
        {
            print("A was pressed");
            transform.position -= new Vector3(Speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
            print("S was pressed");

        if (Input.GetKey(KeyCode.D))
        {
            print("D was pressed");
            transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
        }
        */
    }
}
