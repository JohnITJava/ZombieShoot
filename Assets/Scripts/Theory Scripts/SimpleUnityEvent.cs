using UnityEngine;
using UnityEngine.Events;

public class SimpleUnityEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent OnStart;
    [SerializeField] private UnityEvent OnUnityTriggerEnter;

    private void Start()
    {
        if (OnStart == null) OnStart = new UnityEvent();
        if (OnUnityTriggerEnter == null) OnUnityTriggerEnter = new UnityEvent();

        OnStart.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OnUnityTriggerEnter.Invoke();
        }
    }
}
