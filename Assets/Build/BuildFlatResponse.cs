using UnityEngine;
using Utils;

namespace Build
{
    class BuildFlatResponse : MonoBehaviour, IBaseBuildResponse, IBuildResponse
    {
        [SerializeField] GameObject _wallPrefab;
        [SerializeField] Transform _ground;
        [SerializeField] int _priority = 0;

        IValidBuildPosProvider _validBuildPosProvider;
        Quaternion _initialRot;
        Vector3 _wallExtents;
        WallCreation _wallCreation;
        float _topGroundY;
        Vector3 _newPos;

        Vector3 _vec;

        void Awake()
        {
            _validBuildPosProvider = GetComponent<IValidBuildPosProvider>();
            _wallCreation = GetComponent<WallCreation>();

            GameObject _tempWall = Instantiate(_wallPrefab);
            _initialRot = _tempWall.transform.rotation;
            _wallExtents = _tempWall.GetComponent<Renderer>().bounds.extents;
            Destroy(_tempWall);

            _topGroundY = _ground.position.y + _ground.GetComponent<Renderer>().bounds.extents.y;
            _vec = transform.forward;
        }

        bool IBaseBuildResponse.ShouldBuild(Vector3 aimPosition)
        {
            return Input.GetMouseButton(0) && (aimPosition.y - _topGroundY) <= 0.01f;
        }

        GameObject IBaseBuildResponse.Build(Vector3 aimPosition)
        {
            Vector3 spawnPoint = aimPosition + new Vector3(0f, _wallExtents.y, 0f);
            return _wallCreation.CreateWall(spawnPoint, _initialRot);
        }

        public bool ShouldBuild(Vector3 baseWallPosition, Vector3 buildDir)
        {
            _newPos = GetNewPos(baseWallPosition, buildDir);
            return _validBuildPosProvider.IsValidPos(_newPos) && IsBuildingStraight();
        }

        public GameObject Build(Transform baseWall, Vector3 buildDir)
        {
            return _wallCreation.CreateWall(_newPos, _initialRot);
        }

        Vector3 GetNewPos(Vector3 baseWallPosition, Vector3 buildDir)
        {
            Vector3 buildDirForward = VectorUtils.ProjectVectorOntoPlane(buildDir, Vector3.up);
            float angle = VectorUtils.GetVerticalAngle(buildDirForward, buildDir);

            Vector3 yChange = new Vector3(0f, Mathf.Sin(angle * Mathf.Deg2Rad) * _wallExtents.z, 0f);
            Vector3 xChange = (Mathf.Cos(angle * Mathf.Deg2Rad) * _wallExtents.z + _wallExtents.z) * buildDirForward;

            return baseWallPosition + xChange + yChange;
        }

        int IBuildResponse.GetBuildPriority()
        {
            return _priority;
        }

        bool IsBuildingStraight()
        {
            return Input.GetKey(KeyCode.Tab);
        }

    }
}