using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float Axis = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        float speed = 0.1f;
        Vector3 Vec = new Vector3(Axis, 0, Vertical);
        transform.position += Vec * speed;
	}
}
