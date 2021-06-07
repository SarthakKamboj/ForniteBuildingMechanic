using UnityEngine;
using Utils;

namespace Build
{
    public class BuildManager : MonoBehaviour
    {

        [SerializeField] GameObject _wallPrefab;
        [SerializeField] LayerMask _buildLayerMask;
        [SerializeField] float _maxDistance = 10f;

        Vector3 _wallExtents;
        IRayProvider _rayProvider;
        IValidBuildPosProvider _validBuildPosProvider;
        IBuildResponse[] _buildResponses;
        IBaseBuildResponse _baseBuildResponse;
        ISelector _selector;

        Vector3 _aimPoint;

        void Start()
        {

            _rayProvider = GetComponent<IRayProvider>();
            _validBuildPosProvider = GetComponent<IValidBuildPosProvider>();
            _buildResponses = GetComponents<IBuildResponse>();
            _baseBuildResponse = GetComponent<IBaseBuildResponse>();
            _selector = GetComponent<ISelector>();

            _wallExtents = GetWallExtents();
        }

        private Vector3 GetWallExtents()
        {
            GameObject _tempWall = Instantiate(_wallPrefab);
            Vector3 wallExtents = _tempWall.GetComponent<Renderer>().bounds.extents;
            Destroy(_tempWall);
            return wallExtents;
        }

        void Update()
        {
            Ray ray = _rayProvider.GetRay();

            _selector.CheckRay(ray);
            Transform selectionTransform = _selector.GetSelection();

            if (selectionTransform != null)
            {
                Vector3 aimPoint = _selector.GetAimPoint();
                _aimPoint = aimPoint;

                if (selectionTransform.CompareTag("Wall"))
                {
                    Vector3 relativeSelectionPos = VectorUtils.GetRelativePosition(selectionTransform.position, _wallExtents, aimPoint);

                    Vector3 buildDir = GetBuildDirection(selectionTransform, relativeSelectionPos);

                    int maxPriority = -1;
                    IBuildResponse finalBuildResponse = null;
                    foreach (IBuildResponse buildResponse in _buildResponses)
                    {
                        if (buildResponse.GetBuildPriority() > maxPriority && buildResponse.ShouldBuild(selectionTransform.position, buildDir))
                        {
                            maxPriority = buildResponse.GetBuildPriority();
                            finalBuildResponse = buildResponse;
                        }
                    }

                    if (maxPriority != -1)
                    {
                        finalBuildResponse.Build(selectionTransform, buildDir);
                    }
                }
                else
                {
                    if (_baseBuildResponse.ShouldBuild(aimPoint))
                    {
                        _baseBuildResponse.Build(aimPoint);
                    }
                }

            }
        }

        public Vector3 GetBuildDirection(Transform t, Vector3 relativeSelectionPos)
        {
            Vector3 buildDir = Vector3.zero;
            if (Mathf.Abs(relativeSelectionPos.x) >= 0.5f)
            {
                buildDir = t.right * Mathf.Sign(relativeSelectionPos.x);
            }

            if (Mathf.Abs(relativeSelectionPos.z) >= 0.5f)
            {
                buildDir = t.forward * Mathf.Sign(relativeSelectionPos.z);
            }

            return buildDir;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_aimPoint, 0.3f);
        }

    }
}
