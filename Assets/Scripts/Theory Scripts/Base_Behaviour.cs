using UnityEngine;

public class Base_Behaviour : MonoBehaviour
{
    
    private void Awake() { print("Awake"); }

    private void Start() { print("Start"); }

    private void Update() { print("Update"); }

    private void FixedUpdate() { print("FixedUpdate"); }

    private void LateUpdate() { print("LateUpdate"); }

    private void OnEnable() { print("OnEnable"); }

    private void OnDisable() { print("OnDisable"); }    

    public void Method() { print("Method"); }
}
