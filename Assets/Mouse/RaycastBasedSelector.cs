using UnityEngine;

public class RaycastBasedSelector : MonoBehaviour, ISelector
{

    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _maxDistance = 10f;
    [SerializeField] RaycastHit _hit;

    public void CheckRay(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistance, _layerMask))
        {
            _hit = hit;
        }
    }

    public Vector3 GetAimPoint()
    {
        return _hit.point;
    }

    public Transform GetSelection()
    {
        return _hit.transform;
    }
}