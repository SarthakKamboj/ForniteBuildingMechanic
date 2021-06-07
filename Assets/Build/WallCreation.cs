using UnityEngine;

namespace Build
{
    public class WallCreation : MonoBehaviour
    {
        [SerializeField] GameObject _wallPrefab;

        public GameObject CreateWall(Vector3 pos, Quaternion quat)
        {
            return Instantiate(_wallPrefab, pos, quat);
        }
    }
}