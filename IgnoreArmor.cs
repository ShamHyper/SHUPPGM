using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace SHU
{
    public class IgnoreArmor : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            ArmorBehaviour armor = col.gameObject.GetComponent<ArmorBehaviour>();
            if (armor)
            {
                armor.Nocollide(gameObject);
            }
        }
    }
}