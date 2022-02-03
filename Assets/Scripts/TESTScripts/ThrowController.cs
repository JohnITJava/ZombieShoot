using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TestScripts
{
    public class ThrowController : MonoBehaviour
    {
        [SerializeField] private GameObject _throwableSpawner;
        [SerializeField] private GameObject _throwablePrefab;
        [SerializeField] private GameObject _throwablesPool;

        [SerializeField] private float _force = 10.0f;
        [SerializeField] private float _destroyThowableTime = 10.0f;

        [SerializeField] private List<GameObject> _allThrowables;

        [SerializeField] private bool _throwKey;

        [SerializeField] private int _RightMouseButton = 1;

        [SerializeField] private Vector3 _forceDirection = new Vector3(0.0f, 1.0f, 1.0f);

        private void Start()
        {
        }

        private void Update()
        {
            _throwKey = Input.GetMouseButtonDown(_RightMouseButton);

            if (_throwKey)
            {
                Throw();
            }
        }

        private void Throw()
        {
            var go = Instantiate(_throwablePrefab, _throwableSpawner.transform);
            go.transform.SetParent(_throwablesPool.transform);
            _allThrowables.Add(go);
            go.GetComponent<Rigidbody>().AddForce(_forceDirection * _force, ForceMode.Force);
            Destroy(go, _destroyThowableTime);
        }
    }
}