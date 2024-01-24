using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponRunner
{
    public class Gate : MonoBehaviour
    {
        [SerializeField, Required] private WeaponData weaponData;

        public WeaponData WeaponData => weaponData;
    }
}