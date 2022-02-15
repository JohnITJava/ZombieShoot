using UnityEngine;
using UnityEngine.AI;


namespace TestScripts
{
    
    public class WaypointPatrolController : MonoBehaviour
    {
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private Vector3 _nextPoint;
        [SerializeField] private int _currentWaypointIndex;
       
        private NavMeshAgent _agent;
        
        
        
        private void Start()
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
            _nextPoint = waypoints[0].position;
            _agent.SetDestination(_nextPoint);
        }


        private void Update()
        {
            if (IsDestinationAchieved())
            {
                SetNextPoint();
            }
        }

        
        private bool IsDestinationAchieved()
        {
            return _agent.remainingDistance < _agent.stoppingDistance;
        }
        

        private void SetNextPoint()
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            _nextPoint = waypoints[_currentWaypointIndex].position;
            _agent.SetDestination(_nextPoint);
        }

        // private void CheckAndSetNextPoint()
        // {
        //     if (_agent.remainingDistance < _agent.stoppingDistance)
        //     {
        //         _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        //         _nextPoint = waypoints[_currentWaypointIndex].position;
        //     }
        // }
        
        
    }
}