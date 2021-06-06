using UnityEngine;
using Utils;

namespace Build
{
    public class BuildDiagonalResponse : MonoBehaviour, IBuildResponse
    {

        [SerializeField] int _priority = 2;
        [SerializeField] GameObject _wallPrefab;
        [SerializeField] Transform _player;
        [SerializeField] float _steepnessDegree;

        IValidBuildPosProvider _validBuildPosProvider;
        WallCreation _wallCreation;
        Vector3 _newBuildPos;
        Vector3 _wallExtents;
        int _verticalMultiplier = 1;

        void Awake()
        {
            _validBuildPosProvider = GetComponent<IValidBuildPosProvider>();
            _wallCreation = GetComponent<WallCreation>();
            _wallExtents = GetWallExtents();
        }

        Vector3 GetWallExtents()
        {
            GameObject tempWall = Instantiate(_wallPrefab);
            Vector3 wallExtents = tempWall.GetComponent<Renderer>().bounds.extents;
            Destroy(tempWall);
            return wallExtents;
        }

        Vector3 GetPlayerRoundedForward()
        {
            float yRot = _player.eulerAngles.y;
            return Quaternion.Euler(new Vector3(0f, AngleUtils.RoundToNearestRightAngle(yRot, 45f), 0f)) * Vector3.forward;
        }

        public int GetBuildPriority()
        {
            return _priority;
        }

        public bool ShouldBuild(Vector3 baseWallPosition, Vector3 buildDir)
        {

            if (Input.GetKey(KeyCode.LeftControl))
            {
                _verticalMultiplier = 1;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                _verticalMultiplier = -1;
            }
            else
            {
                _verticalMultiplier = 0;
            }

            _newBuildPos = GetBuildPos(baseWallPosition, buildDir);
            bool validPos = _validBuildPosProvider.IsValidPos(_newBuildPos);

            if (buildDir == Vector3.zero)
            {
                return false;
            }

            Quaternion buildDirForwardQuat = Quaternion.LookRotation(buildDir);
            float yRot = buildDirForwardQuat.eulerAngles.y;
            float roundedYRot = AngleUtils.RoundToNearestRightAngle(yRot, 45f);

            if (roundedYRot == -1f)
            {
                return false;
            }

            Vector3 buildDirRoundedForward = Quaternion.Euler(0f, roundedYRot, 0f) * Vector3.forward;

            Vector3 playerRoundedForward = GetPlayerRoundedForward();

            float playerBuildDirForwardAngle = Vector3.Dot(buildDirRoundedForward, playerRoundedForward);
            bool isBuildingInFront = playerBuildDirForwardAngle >= Mathf.Cos(45f * Mathf.Deg2Rad);

            return _verticalMultiplier != 0 && Input.GetMouseButton(0) && validPos && isBuildingInFront;
        }

        public GameObject Build(Transform baseWall, Vector3 buildDir)
        {
            GameObject wall = _wallCreation.CreateWall(_newBuildPos, Quaternion.Euler(Vector3.zero));
            Transform wallTransform = wall.transform;

            Vector3 axis = Vector3.Cross(buildDir, Vector3.up);
            Quaternion rot = Quaternion.AngleAxis(_verticalMultiplier * _steepnessDegree, axis);

            wallTransform.rotation = wallTransform.rotation * rot;

            return wall;
        }

        Vector3 GetBuildPos(Vector3 baseWallPosition, Vector3 buildDir)
        {
            Vector3 buildDirForward = VectorUtils.ProjectVectorOntoPlane(buildDir, Vector3.up);
            float angle = VectorUtils.GetVerticalAngle(buildDirForward, buildDir);

            Vector3 yChange = new Vector3(0f, Mathf.Sin(angle * Mathf.Deg2Rad) * _wallExtents.z + _wallExtents.z * Mathf.Sin(_verticalMultiplier * _steepnessDegree * Mathf.Deg2Rad), 0f);
            Vector3 xChange = Mathf.Cos(angle * Mathf.Deg2Rad) * _wallExtents.z * buildDirForward + _wallExtents.z * buildDirForward * Mathf.Cos(_steepnessDegree * Mathf.Deg2Rad);

            return baseWallPosition + yChange + xChange;
        }

    }
}