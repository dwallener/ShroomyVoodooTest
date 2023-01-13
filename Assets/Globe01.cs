using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe01 : MonoBehaviour
{

    // for rotation
    public Rigidbody rb;
    public float torque = 100.0f;
    private float rotationSpeed = 10.0f;
    private float lerpSpeed = 1.0f;

    private Vector3 speed = new Vector3();
    private Vector3 avgSpeed = new Vector3();
    private bool dragging = false;
    private Vector3 targetSpeedX = new Vector3();


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && dragging)
        {
            speed = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            avgSpeed = Vector3.Lerp(avgSpeed, speed, Time.deltaTime * 5);
        }
        else
        {
            if (dragging)
            {
                speed = avgSpeed;
                dragging = false;
            }
            var i = Time.deltaTime * lerpSpeed;
            speed = Vector3.Lerp(speed, Vector3.zero, i);
        }

        transform.Rotate(Camera.main.transform.up * speed.x * rotationSpeed, Space.World);
        transform.Rotate(Camera.main.transform.right * speed.y * rotationSpeed, Space.World);

        Vector3 originPosition = new Vector3(0.0f, 0.0f, 0.0f);

        // This is horrible anti-physics hack, but should work
        transform.position = originPosition;

    }


    Vector3 ReturnVector3fromPolar(float r, float s, float t)
    {

        Vector3 returnVector;

        returnVector.x = r * Mathf.Cos(s) + Mathf.Sin(t);
        returnVector.y = r * Mathf.Sin(s) + Mathf.Sin(t);
        returnVector.z = r * Mathf.Cos(t);

        return returnVector;
    }

    private void OnMouseDown()
    {
        dragging = true;
    }


}

