using UnityEngine;

public class GhostRay : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    private Transform _player;

    private float _startOffset = 0.5f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        //_mask = 1 << 8; // Слой Player
        //RaycastHit hit;

        //var rayCast = Physics.Raycast(transform.position, transform.forward, out hit, 1, _mask);

        //if (rayCast)
        //{
        //    print(hit.collider.gameObject.tag);
        //}

        //Debug.DrawRay(transform.position * 1, transform.forward, Color.red);

        var color = Color.red;
        RaycastHit hit;

        var startRaycastPosition = CalculateOffset(transform.position);
        var playerPosition = CalculateOffset(_player.position - startRaycastPosition);

        //var rayCast = Physics.Raycast(startRaycastPosition, playerPosition, out hit, Mathf.Infinity, _mask);
        var rayCast = Physics.Raycast(startRaycastPosition, playerPosition, out hit, playerPosition.magnitude, _mask);

        if (rayCast)
        {
            print(hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                color = Color.green;
                print(hit.collider.gameObject.tag);
            }
        }

        Debug.DrawRay(startRaycastPosition, playerPosition, color);
        
    }

    private Vector3 CalculateOffset(Vector3 position)
    {
        position.y += _startOffset;
        return position;
    }
}
