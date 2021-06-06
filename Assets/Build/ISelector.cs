using UnityEngine;

interface ISelector
{
    void CheckRay(Ray ray);
    Transform GetSelection();
    Vector3 GetAimPoint();
}
