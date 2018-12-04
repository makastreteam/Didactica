using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerMap : MonoBehaviour {

    public Transform target;
    [Range(0.1f, 50f)]
    public float speed = 10f;
    float camDistance;


    //Camera Limits:
    float minX = -5.85f;
    float maxX = 5.85f;
    float minY = -15.5f;
    float maxY = 12.5f;
    Camera cam;
	void Start () {
        cam = GetComponent<Camera>();
        camDistance = cam.transform.position.z;
	}
	
	void Update () {
        FollowPlayer();
	}


    void FollowPlayer()
    {
        float target_x, target_y;

        //X € [-4.15, 4]
        //Si has llegado al borde derecho
        //Si has llegado al borde izquierdo
        if (target.position.x >= maxX) target_x = maxX;
        else if (target.position.x <= minX) target_x = minX;
        else target_x = target.position.x;

        //Y € [-15.5, 12.5]
        //Si has llegado al borde superior
        //Si has llegado al borde inferior
        if (target.position.y >= maxY) target_y = maxY;
        else if (target.position.y <= minY) target_y = minY;
        else target_y = target.position.y;

        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(target_x, target_y, camDistance), Time.deltaTime * speed);

    }
}
