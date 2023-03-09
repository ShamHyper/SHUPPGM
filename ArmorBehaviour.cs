using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace SHU
{
    public class ArmorBehaviour : MonoBehaviour
    {
        private bool equipped;
        public string armorPiece;
        public int armorTier;
        public float stabResistance;
        private bool blockingStab;

        // dont do this
        public string otherSprite;
        public string otherName;
        public string otherPiece;
        public float otherResist;
        public float mass;
        public int otherTier;
        // honestly this is fucking terrible
        // not good code, messy
        public bool threepieces;
        public string thirdsprite;
        public string thirdpart;
        public Vector3 offset;
        public Vector3 scaleOffset = new Vector3(1, 1, 1);
        public bool decorative;
        [SerializeField]
        public LimbBehaviour attachedLimb;

        void Start()
        {
            GetComponent<PhysicalBehaviour>().HoldingPositions = new Vector3[0];
            switch (armorTier)
            {
                case 0:
                    GetComponent<PhysicalProperties>().Softness = 1;
                    GetComponent<PhysicalProperties>().Brittleness = 1;
                    break;
                case 1:
                    GetComponent<PhysicalProperties>().Softness = .15f;
                    GetComponent<PhysicalProperties>().Brittleness = 0f;
                    GetComponent<PhysicalProperties>().BulletSpeedAbsorptionPower = .35f;
                    break;
                case 2:
                    GetComponent<PhysicalProperties>().Softness = 0f;
                    GetComponent<PhysicalProperties>().Brittleness = 0f;
                    GetComponent<PhysicalProperties>().BulletSpeedAbsorptionPower = 2f;
                    break;
            }
        }
        public void SpawnOtherParts()
        {
            GameObject lower = Instantiate(ModAPI.FindSpawnable(otherName).Prefab, transform.position, transform.rotation);
            lower.name = ModAPI.FindSpawnable(otherName).name;
            lower.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite(otherSprite);
            if (GetComponent<PhysicalBehaviour>().IsWeightless)
            {
                lower.GetComponent<PhysicalBehaviour>().MakeWeightless();
            }
            ArmorBehaviour armor1 = lower.AddComponent<ArmorBehaviour>();
            armor1.stabResistance = otherResist;
            armor1.armorPiece = otherPiece;
            armor1.armorTier = otherTier;
            lower.FixColliders();
            if (threepieces)
            {
                GameObject lower1 = Instantiate(ModAPI.FindSpawnable(otherName).Prefab, lower.transform.position, transform.rotation);
                lower1.name = ModAPI.FindSpawnable(otherName).name;
                lower1.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite(thirdsprite);
                if (GetComponent<PhysicalBehaviour>().IsWeightless)
                {
                    lower1.GetComponent<PhysicalBehaviour>().MakeWeightless();
                }
                ArmorBehaviour armor11 = lower1.AddComponent<ArmorBehaviour>();
                armor11.stabResistance = otherResist;
                armor11.armorPiece = thirdpart;
                armor11.armorTier = otherTier;
                lower1.FixColliders();
            }
        }
        void Update()
        {
            if (equipped && GetComponent<FixedJoint2D>().connectedBody.gameObject.GetComponent<GripBehaviour>() && GetComponent<FixedJoint2D>().connectedBody.gameObject.GetComponent<GripBehaviour>().CurrentlyHolding)
            {
                GripBehaviour grip = GetComponent<FixedJoint2D>().connectedBody.gameObject.GetComponent<GripBehaviour>();
                Nocollide(grip.CurrentlyHolding.gameObject);
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!decorative)
            {
                LimbBehaviour limb = collision.gameObject.GetComponent<LimbBehaviour>();
                ArmorBehaviour arm = collision.gameObject.GetComponent<ArmorBehaviour>();
                if (arm)
                {
                    Nocollide(arm.gameObject);
                }
                if (limb)
                {
                    Nocollide(limb.gameObject);
                    if (!equipped && limb.gameObject.name == armorPiece)
                    {
                        
                        Debug.Log("attach");
                        Attach(limb);
                        
                    }
                }
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            }
        }
        public void Nocollide(GameObject col)
        {
            NoCollide noCol = gameObject.AddComponent<NoCollide>();
            noCol.NoCollideSetA = GetComponents<Collider2D>();
            noCol.NoCollideSetB = col.GetComponents<Collider2D>();
        }
        public void Attach(LimbBehaviour limb)
        {
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sortingOrder = limb.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            sr.sortingLayerName = limb.gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
            equipped = true;
            GetComponent<Rigidbody2D>().isKinematic = true;
            transform.parent = limb.transform;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.localPosition = offset;
            transform.localScale = scaleOffset;
            transform.parent = null;
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.dampingRatio = 1;
            joint.frequency = 0;
            joint.connectedBody = limb.GetComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().isKinematic = false;
            attachedLimb = limb;
        }
        public void Detach(LimbBehaviour limb)
        {
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            equipped = false;
            foreach (FixedJoint2D joint in GetComponents<FixedJoint2D>())
            {
                if (joint.connectedBody = limb.gameObject.GetComponent<Rigidbody2D>())
                    Destroy(joint);
            }
            attachedLimb = null;
        }
    }
}