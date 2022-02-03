using UnityEngine;

namespace ZombieQuest
{
    public class MineSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _mine; // Наша мина
        [SerializeField] private Transform _mineSpawnPlace; // точка, где создается мина

        private void Update()
        {                     
            // Если нажата кнопка  
            if (Input.GetButtonDown("Fire1"))   
            {                      
                Instantiate(_mine, _mineSpawnPlace.position, _mineSpawnPlace.rotation);   
                // Создаем _mine в точке _mineSpawnPlace
            }
        }

    }
}