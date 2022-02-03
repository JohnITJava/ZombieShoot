using System;
using UnityEngine;
using Random = System.Random;

namespace TestScripts
{
    internal sealed class EnemySpawnerController : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GameObject _spawnPoint;
        [SerializeField] private float _enemyDistStep = 1.0f;
        [SerializeField] private int _enemyCount = 5;
        [SerializeField] private float _spawnTime = 2.0f;
        [SerializeField] private float _timer = 0.0f;
        [SerializeField] private float _spawnScale = 2.0f;

        private Vector3 _randomSpawnPoint;
        private Random rand;

        private bool _isNeedSpawn = true;

        
        // Start is called before the first frame update
        private void Start()
        {
            rand = new Random();
            _randomSpawnPoint = new Vector3();
            _randomSpawnPoint.y = _spawnPoint.transform.position.y;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isNeedSpawn)
            {
                _timer += Time.deltaTime;
                if (_timer >= _spawnTime && _enemyCount > 0)
                {
                    Spawn();
                    _timer = 0.0f;
                    _enemyCount--;

                    if (_enemyCount == 0)
                    {
                        _isNeedSpawn = false;
                        _timer = 0.0f;
                    }
                }
            }
            
            
        }

        private void Spawn()
        {
            var point = GetRandomSpawnPoint();
            var go = Instantiate(_enemyPrefab, point, Quaternion.identity);
            go.transform.Rotate(Vector3.up * 180);
        }

        private Vector3 GetRandomSpawnPoint()
        {
            var mantis = (float) rand.NextDouble();
            float x = rand.Next(
                (int) (_spawnPoint.transform.position.x - _spawnScale),
                (int) (_spawnPoint.transform.position.x + _spawnScale));

            _enemyDistStep *= -1;
            
            x += mantis + _enemyDistStep;

            mantis = (float) rand.NextDouble();
            float z = rand.Next(
                (int) (_spawnPoint.transform.position.z - _spawnScale),
                (int) (_spawnPoint.transform.position.z + _spawnScale));

            _enemyDistStep *= -1;
            
            z += mantis + _enemyDistStep;;

            _randomSpawnPoint.x = x;
            _randomSpawnPoint.z = z;

            return _randomSpawnPoint;
        }
    }
}