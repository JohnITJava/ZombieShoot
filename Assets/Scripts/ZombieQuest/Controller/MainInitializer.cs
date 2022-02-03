using UnityEngine;


namespace ZombieQuest
{
    
    internal sealed class MainInitializer : MonoBehaviour
    {
        
        public delegate void StartEventType();
        public event StartEventType OnStartEvent = () => { };

        private float _deltaTime;
        

        private void Start()
        {
            OnStartEvent();
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            // print(_deltaTime);
        }


    }
}