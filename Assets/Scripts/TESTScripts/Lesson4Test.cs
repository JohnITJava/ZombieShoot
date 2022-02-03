using System;
using UnityEngine;

namespace TestScripts
{
    public class Lesson4Test : MonoBehaviour
    {
        [SerializeField] private Color _color;

        private void Update()
        {
            _color = Mathf.CorrelatedColorTemperatureToRGB(Time.time * 100.0f);
            print(Time.time * 100);    
        }

    }
}