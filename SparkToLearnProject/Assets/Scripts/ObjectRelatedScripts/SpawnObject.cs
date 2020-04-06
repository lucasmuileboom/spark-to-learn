using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpawnObject : MonoBehaviour
{
    public static void SpawnObjectOnRaycastHit(Transform orientation,GameObject spawnObject,LayerMask mask)
    {
        Ray ray = new Ray(orientation.position, orientation.forward);
        RaycastHit hit;
  
        if (Physics.Raycast(ray, out hit,100,mask))
        {
            
            Quaternion hitRotation = Quaternion.Euler(hit.normal.x,hit.normal.y,hit.normal.z) * hit.collider.transform.rotation;
            Instantiate<GameObject>(spawnObject, hit.point, hitRotation);
        }
    }
    public static IEnumerator HighlightObjectOnRaycastHit(GameObject orientation, GameObject spawnObject, Func<bool> breakCondition, LayerMask mask)
    {
        Quaternion hitRotation;
        GameObject highlight = null;

        yield return new WaitForEndOfFrame();

        while (true)
        {
            RaycastHit hit;
            Ray ray = new Ray(orientation.transform.position, orientation.transform.forward);

            if (Physics.Raycast(ray, out hit,100,mask))
            {
                highlight = (highlight == null) ? Instantiate<GameObject>(spawnObject) : highlight;

                hitRotation = Quaternion.Euler(hit.normal.x, hit.normal.y, hit.normal.z) * hit.collider.transform.rotation;

                highlight.transform.SetPositionAndRotation(hit.point, hitRotation);
            }

            yield return new WaitForEndOfFrame();

            if (breakCondition())
            {
                break;
            }
        }

        Destroy(highlight);
    }
}
