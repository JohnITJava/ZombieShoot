using System.Collections;
using UnityEngine;

public class Methods : MonoBehaviour
{
    [SerializeField] private float _waitTime;

    private Coroutine _cooldown = null;

    private float _counterTime = 10.0f; // -= time.delt ... if(_counterTime) < 0) _counterTime == 10.0f;
    private int _count = 0;
    private bool MyCooldown = false;

    // Use this for initialization
    private void Start ()
    {
        //Invoke("Timer", _waitTime); // Отложенный вызов метода.
        //InvokeRepeating("Counter", 4, 2); // Переодический вызов отложенного метода  string (метод), float (задержка), float (частота)        
        //yield break;
        //StartCoroutine(Spawn());
    }

    // Update is called once per frame
    private void Update()
    {
        //StartCoroutine(Spawn());

        //if (_cooldown == null)
        //    _cooldown = StartCoroutine(Spawn());

        //StopCoroutine(Spawn());
        // StopAllCoroutines();
        // Cooldown = null;
    }

    private void Timer()
    {
        print("Я ожил");
    }

    private void Counter()
    {
        _count ++;
        print("Посчитали: " + _count);

        if (_count == 5)
            CancelInvoke("Counter");
    }

    private IEnumerator Spawn()
    {
        print(_waitTime);
        yield return new WaitForSeconds(_waitTime); // WaitTime = 0.007f;
       // yield return new WaitForEndOfFrame();
       // yield return new WaitForFixedUpdate();

        _waitTime -= 1;        

        _cooldown = null;
        //while(true)
        //{
        //    print("Hello Unity!");
        //}
        // yield break;
        //yield return new WaitForFixedUpdate(); 
    }
}
