using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieQuest
{
    public class MyEnemy : MonoBehaviour
    {
        [SerializeField] private int _health;

        public void Hurt(int damage)
        {
            print("Ouch: " + damage);

            _health -= damage;

            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}