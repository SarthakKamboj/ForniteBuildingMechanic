using UnityEngine;

public class RaycastBasedBuildSelector : MonoBehaviour, ISelector
{

    [SerializeField] LayerMask _buildLayerMask;
    [SerializeField] float _maxDistance = 10f;
    [SerializeField] RaycastHit _hit;

    public void CheckRay(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistance, _buildLayerMask))
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