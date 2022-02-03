using System;
using UnityEngine;

namespace ZombieQuest
{
    internal sealed class CameraMovementController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _camera;

        private float _xDelta;
        private float _zDelta;
        private float _yDefault;

        private Vector3 _cameraPositionVector;

        private void Start()
        {
            _xDelta = _player.transform.position.x - _camera.transform.position.x;
            _zDelta = _player.transform.position.z - _camera.transform.position.z;
            _yDefault = _camera.transform.position.y;
        }

        private void LateUpdate()
        {
            _cameraPositionVector = _player.transform.position;
            _cameraPositionVector.y = _yDefault;
            _cameraPositionVector.x -= _xDelta;
            _cameraPositionVector.z -= _zDelta;

            _camera.transform.position = _cameraPositionVector;
        }
    }
}