using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y")*1.5f, Input.GetAxis("Mouse X")*1.5f));
    }
}
