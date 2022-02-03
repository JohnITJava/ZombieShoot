using System.Collections.Generic;
using UnityEngine;


namespace ZombieQuest
{

    internal sealed class EmissionController : MonoBehaviour
    {

        [SerializeField] private List<GameObject> _highlightedObjects;
        
        
        public delegate void MethodContainer();
        public event MethodContainer onCount;
        
        
        private bool[] _isActivatedArray;
        private bool[] _isNeedEnableEmission;

        private Color _currentColor = new Color(0.0f, 0.0f, 0.0f);
        private Color _lightColor = new Color(0.0f, 0.05f, 0.05f);

        
        
        private void Start()
        {
            _isActivatedArray = new bool[_highlightedObjects.Count];
            _isNeedEnableEmission = new bool[_highlightedObjects.Count];
            for (int i = 0; i < _isNeedEnableEmission.Length; i++)
            {
                _isNeedEnableEmission[i] = true;
            }
        }


        private void Update()
        {
            HighlightLogicProceed(_highlightedObjects);
            
            // Highlight(_highlitedObjects[0], 0);
        }


        private void EnableEmission(GameObject lightObj)
        {
            // Enables emission for the material, and make the material use
            // realtime emission.
            var material = lightObj.GetComponent<Renderer>().material;
            material.EnableKeyword("_EMISSION");
            material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        }


        private void DisableEmission(GameObject lightObj)
        {
            var renderer = lightObj.GetComponent<Renderer>();
            var material = renderer.material;
            
            material.DisableKeyword("_EMISSION");
            material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;

            material.SetColor("_EmissionColor", Color.black);
            RendererExtensions.UpdateGIMaterials(renderer);

            DynamicGI.SetEmissive(renderer, Color.black);
            DynamicGI.UpdateEnvironment();
        }

        
        private void UpdateRecalcGISystem(Renderer renderer, Color newColor)
        {
            // Makes the renderer update the emission and albedo maps of our material.
            RendererExtensions.UpdateGIMaterials(renderer);

            // Inform Unity's GI system to recalculate GI based on the new emission map.
            DynamicGI.SetEmissive(renderer, newColor);
            DynamicGI.UpdateEnvironment();
        }


        private void HighlightLogicProceed(List<GameObject> lightObjs)
        {
            for (int i = 0; i < lightObjs.Count; i++)
            {
                var lightObj = lightObjs[i];

                if (!_isActivatedArray[i] && _isNeedEnableEmission[i])
                {
                    EnableEmission(lightObj);
                    _isActivatedArray[i] = true;
                }

                Renderer renderer = lightObj.GetComponent<Renderer>();
                Material material = renderer.material;
                var t = Time.time;
                var newValue = Mathf.PingPong(t * 0.1f, _lightColor.g);

                _currentColor.r = 0.0f;
                _currentColor.g = newValue;
                _currentColor.b = newValue;

                material.SetColor("_EmissionColor", _currentColor);

                UpdateRecalcGISystem(renderer, _currentColor);

                if (!_isNeedEnableEmission[i])
                {
                    DisableEmission(lightObj);
                }
            }
        }
        
        
        
    }
}