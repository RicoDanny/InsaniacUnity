using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyThingy : MonoBehaviour
{
    public float rotateSpeed;
    public Vector3 rotationDirection = new Vector3();

    void Update()
    {
        transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);
    }
}
