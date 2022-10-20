using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    Camera cam;
    Vector3 direction = new Vector3(5f,0f,45f);
    float speed = 6.0f;

    void Start () {
      cam = Camera.main;
	}
	
	void Update () {

        float step = speed * Time.deltaTime;
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, direction, step);
          if (cam.transform.position.z > 40f)
        {
            cam.transform.position = new Vector3(5f, 0f, 0f);
        }
 
    }
           
    }