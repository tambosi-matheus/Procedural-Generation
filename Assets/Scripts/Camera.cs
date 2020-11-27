using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private bool autoMove = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) autoMove = !autoMove;
        if (Input.GetKeyDown(KeyCode.Z)) 
            transform.position += new Vector3(0, speed, 0);
        if (Input.GetKeyDown(KeyCode.C)) 
            transform.position -= new Vector3(0, speed, 0);
        Vector3 vel;
        if(autoMove)
            vel = new Vector3(speed, 0, 0);
        else
            vel = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        transform.position += vel * Time.deltaTime;
    }
}
