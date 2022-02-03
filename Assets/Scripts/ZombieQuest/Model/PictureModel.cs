using UnityEngine;


namespace ZombieQuest
{
    
    public sealed class PictureModel : MonoBehaviour, IInteractable
    {
        
        [SerializeField] private GameObject _picture;
        
        
        
        public void Interact(GameObject other)
        {
            print("Seems this pictures should be moved in right order");
        }
        
        
    }
}