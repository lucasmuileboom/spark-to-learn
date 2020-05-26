using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectSpawner : MonoBehaviour
{
    public static void Instantiate(GameObject spawnObject)
    {
        Instantiate<GameObject>(spawnObject);
    }

    //Spawns a object that hovers around the ground as a highlight
    public static IEnumerator HighlightObjectOnRaycastHit(GameObject orientation, GameObject spawnObject, Func<bool> RotateLeft, Func<bool> RotateRight, float rotateSpeed, Func<GameObject,bool> breakCondition, LayerMask mask, Material correctSpawnMaterial, Material incorrectSpawnMaterial)
    {
        GameObject _highlight = null;
        List<MeshRenderer> _currentMat = null;
        ItemDetails _item = null;
        Quaternion _hitRotation;
        float _rotated = 0;
        yield return new WaitForEndOfFrame();

        while (true)
        {
            //Raycast setup
            RaycastHit hit;
            Ray ray = new Ray(orientation.transform.position, orientation.transform.forward);

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                _highlight = (_highlight == null) ? Instantiate<GameObject>(spawnObject) : _highlight;
                _item = (_item == null && _highlight != null) ? _highlight.GetComponent<ItemDetails>() : _item;
                
                //Rotates the object so it alligns with the surface
                _hitRotation = Quaternion.Euler(hit.normal.x, hit.normal.y, hit.normal.z) * hit.collider.transform.rotation;
                
                _highlight.transform.SetPositionAndRotation(hit.point, _hitRotation);

                _highlight.transform.RotateAround(_highlight.transform.position,_highlight.transform.up, _rotated);

                _currentMat = _item.Mesh;

                if (_highlight != null)
                {
                    _highlight.GetComponent<Collider>().isTrigger = true;
                }
                if(_item != null)
                {
                    if (_item.IsTriggerTouching())
                    {
                        foreach(MeshRenderer mesh in _currentMat)
                        {
                            mesh.material = incorrectSpawnMaterial;
                        }
                    }
                    else
                    {
                        foreach (MeshRenderer mesh in _currentMat)
                        {
                            mesh.material = correctSpawnMaterial;
                        }
                    }
                }
                
                //If the left rotation key is pressed and not the right rotation key: rotate left
                if (RotateLeft() && !RotateRight())
                {
                    _rotated -= rotateSpeed;
                }
                //If the right rotation key is pressed and not the left rotation key: rotate right
                if (RotateRight() && !RotateLeft())
                {
                    _rotated += rotateSpeed;
                }
            }
            yield return new WaitForEndOfFrame();

            //If the stop condition is met, then stop coroutine
            if (_currentMat.Exists((MeshRenderer mesh) => { return (mesh.sharedMaterial == correctSpawnMaterial); }))
            {
                if (breakCondition(_highlight))
                {
                    break;
                }
            }
        }
    }

    public static IEnumerator RepositionObject(GameObject orientation, GameObject spawnObject, Func<bool> RotateLeft, Func<bool> RotateRight, float rotateSpeed, Func<GameObject, bool> breakCondition, LayerMask mask, Material correctSpawnMaterial, Material incorrectSpawnMaterial)
    {
        GameObject _highlight = spawnObject;
        List<MeshRenderer> _currentMat = null;
        ItemDetails _item = null;
        Quaternion _hitRotation;
        float _rotated = 0;
        yield return new WaitForEndOfFrame();

        while (true)
        {
            //Raycast setup
            RaycastHit hit;
            Ray ray = new Ray(orientation.transform.position, orientation.transform.forward);

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                _item = (_item == null && _highlight != null) ? _highlight.GetComponent<ItemDetails>() : _item;

                //Rotates the object so it alligns with the surface
                _hitRotation = Quaternion.Euler(hit.normal.x, hit.normal.y, hit.normal.z) * hit.collider.transform.rotation;

                _highlight.transform.SetPositionAndRotation(hit.point, _hitRotation);

                _highlight.transform.RotateAround(_highlight.transform.position, _highlight.transform.up, _rotated);

                _currentMat = _item.Mesh;

                if (_highlight != null)
                {
                    foreach (Collider _col in _item.Colliders)
                    {
                        _col.isTrigger = true;
                    }
                }
                if (_item != null)
                {
                    if (_item.IsTriggerTouching())
                    {
                        foreach (MeshRenderer mesh in _currentMat)
                        {
                            mesh.materials = new Material[0];
                            mesh.material = incorrectSpawnMaterial;
                        }
                    }
                    else
                    {
                        foreach (MeshRenderer mesh in _currentMat)
                        {
                            mesh.materials = new Material[1];
                            mesh.material = correctSpawnMaterial;
                        }
                    }
                }

                //If the left rotation key is pressed and not the right rotation key: rotate left
                if (RotateLeft() && !RotateRight())
                {
                    _rotated -= rotateSpeed;
                }
                //If the right rotation key is pressed and not the left rotation key: rotate right
                if (RotateRight() && !RotateLeft())
                {
                    _rotated += rotateSpeed;
                }
            }
            yield return new WaitForEndOfFrame();

            //If the stop condition is met, then stop coroutine
            if (_currentMat.Exists((MeshRenderer mesh) => { return (mesh.sharedMaterial == correctSpawnMaterial); }))
            {
                if (breakCondition(_highlight))
                {
                    break;
                }
            }
        }
    }

    //Rotates the object instance
    public static IEnumerator RotateObject(GameObject instance, Func<bool> RotateLeft, Func<bool> RotateRight, float rotateSpeed, Func<GameObject,bool> breakCondition)
    {
        while (true)
        {
            //If the left rotation key is pressed and not the right rotation key: rotate left
            if (RotateLeft() && !RotateRight())
            {
                instance.transform.RotateAround(instance.transform.position,instance.transform.up,-rotateSpeed);
            }
            //If the right rotation key is pressed and not the left rotation key: rotate right
            if (RotateRight() && !RotateLeft())
            {
                instance.transform.RotateAround(instance.transform.position, instance.transform.up, rotateSpeed);
            }

            yield return new WaitForEndOfFrame();

            //If the stop condition is met, then stop coroutine
            if (breakCondition(instance))
            {
                break;
            }
        }
        Destroy(instance);
    }
}
