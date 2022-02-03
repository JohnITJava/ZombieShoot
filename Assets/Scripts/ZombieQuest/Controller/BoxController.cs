using System;
using System.Collections.Generic;
using UnityEngine;


namespace ZombieQuest
{
    internal sealed class BoxController : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<BoxModel> _boxes;

        [SerializeField] private bool _isInteractKeyPressed;


        public void Start()
        {
            foreach (var box in _boxes)
            {
                box.Item?.Disable();
            }
        }


        public void Interact(GameObject other)
        {
            foreach (var box in _boxes)
            {
                box.Interact(other);
            }
        }

        
        public BoxModel FindBoxByItem(IItemable item)
        {
            BoxModel foundBox = null;
            foreach (var box in _boxes)
            {
                if (!(box.Item is null))
                {
                    if (box.Item.Equals(item))
                    {
                        foundBox = box;
                        break;
                    }
                }
            }
            return foundBox;
        }
        
    }
}