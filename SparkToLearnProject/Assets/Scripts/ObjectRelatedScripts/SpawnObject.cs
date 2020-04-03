using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public static void SpawnObjectOnRaycastHit(Transform orientation,GameObject spawnObject)
    {
        Ray ray = new Ray(orientation.position, orientation.forward);
        RaycastHit hit;
  
        Debug.Log("scanning");
        if (Physics.Raycast(ray, out hit))
        {
            Quaternion hitRotation = Quaternion.Euler(hit.normal.x,hit.normal.y,hit.normal.z)*hit.collider.transform.rotation;
            Instantiate<GameObject>(spawnObject, hit.point, hitRotation);
        }
    }
}
