using UnityEngine;


public class BulletLesson4 : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private Rigidbody _rigidbody;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);

        _rigidbody = GetComponent<Rigidbody>();

        var impulse = transform.up * _rigidbody.mass * _speed;

        _rigidbody.AddForce(impulse, ForceMode.Impulse);
    }
}
