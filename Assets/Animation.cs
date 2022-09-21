using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
     Vector3 direction = new Vector3(5f,0f,45f);
    float speed = 4.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, direction, step);
          if (transform.position.z > 40f)
        {
            transform.position = new Vector3(5f, 0f, 0f);
        }
 
    }
           
    }