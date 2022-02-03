using TestScripts;
using UnityEngine;

//
// namespace ZombieQuest
// {
//     internal class WeaponModel : MonoBehaviour, IInteractable, IItemable
//     {
//         [SerializeField] private GameObject _weapon;
//         [SerializeField] private bool _isEnabled;
//         [SerializeField] private bool _isEquiped;
//         [SerializeField] private bool _isInInventory;
//
//
//         public void StartProcess()
//         {
//             if (!_weapon.Equals(null))
//             {
//                 _weapon.SetActive(false);
//             }
//         }
//
//         public void TakeInInventory()
//         {
//             if (!_isInInventory)
//             {
//                 Disable();
//                 Destroy(_weapon);
//                 _isInInventory = true;
//             }
//         }
//
//         public void Equip(GameObject hand)
//         {
//             Enable();
//             Instantiate(_weapon, hand.transform.position, hand.transform.rotation);
//             _isEquiped = true;
//         }
//
//         public void UseEquiped()
//         {
//         }
//
//
//         public void Interact(GameObject other)
//         {
//         }
//
//
//         public void Enable()
//         {
//             if (!_weapon.Equals(null))
//             {
//                 _weapon.SetActive(true);
//                 _isEnabled = true;
//             }
//         }
//
//         public void Disable()
//         {
//             if (!_weapon.Equals(null))
//             {
//                 _weapon.SetActive(false);
//                 _isEnabled = false;
//             }
//         }
//
//         public override bool Equals(object other)
//         {
//             WeaponModel otherWeaponModel = (WeaponModel) other;
//
//             return otherWeaponModel.Equals(_weapon);
//         }
//     }
// }