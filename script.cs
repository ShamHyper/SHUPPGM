using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace SHU
{

    public class SHU
    {
        public static void Main()
        {
            CategoryBuilder.Create("SH's Utils", "", ModAPI.LoadSprite("category.png"));
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"), //item to derive from
                    NameToOrderByOverride = "A",
                    NameOverride = "Human Afterlife -SHU", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "Human. The same person who tolerates the bullying of the players. But this is his reborn version, with visible past defects. And not naked xd",
                    CategoryOverride = ModAPI.FindCategory("SH's Utils"), //new item category
                    ThumbnailOverride = ModAPI.LoadSprite("HumanAfterlife/thumb.png"), //new item thumbnail
                    AfterSpawn = (Instance) =>
                    {
                        var person = Instance.GetComponent<PersonBehaviour>();

                        person.SetBruiseColor(145, 30, 30);
                        person.SetSecondBruiseColor(60, 0, 0);
                        person.SetThirdBruiseColor(30, 0, 0);
                        person.SetRottenColour(202, 199, 104);
                        person.SetBloodColour(108, 0, 4);

                        foreach (var limb in person.Limbs)
                        {
                            var HumanSprites = limb.gameObject.GetOrAddComponent<RandomHumanTextureBehaviour>();
                            HumanSprites.person = person;
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin.png"));
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin2.png"));
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin3.png"));
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin4.png"));
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin5.png"));
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin6.png"));
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin7.png"));
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin8.png"));
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin9.png"));
                            HumanSprites.Textures.Add(ModAPI.LoadTexture("HumanAfterlife/HA_SHU_Skin10.png"));
                        }


                    }
                }
            );
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Soap"), //item to derive from
                    NameToOrderByOverride = "B",
                    NameOverride = "Duffle Bag -SHU", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "Bag. Bag and in Africa Bag (Russian Proverb).", //new item description
                    CategoryOverride = ModAPI.FindCategory("SH's Utils"), //new item category
                    ThumbnailOverride = ModAPI.LoadSprite("DuffleBag/thumb.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("DuffleBag/DuffleBag.png");
                        foreach (var c in Instance.GetComponents<Collider2D>())
                        {
                            GameObject.Destroy(c);
                        }
                        Instance.FixColliders();

                    }
                }
            );
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Sword"), //item to derive from
                    NameToOrderByOverride = "C",
                    NameOverride = "Old Machete -SHU", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "Machete, very sharp!.", //new item description
                    CategoryOverride = ModAPI.FindCategory("SH's Utils"), //new item category
                    ThumbnailOverride = ModAPI.LoadSprite("MacheteSHU/thumb.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("MacheteSHU/MacheteSHU.png");
                        foreach (var c in Instance.GetComponents<Collider2D>())
                        {
                            GameObject.Destroy(c);
                        }
                        Instance.FixColliders();

                    }
                }
            );
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Magnum Revolver"), //item to derive from
                    NameToOrderByOverride = "D",
                    NameOverride = "Old Magnum -SHU", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "Survival ver. of Magnum. Some smart guy managed to remake a revolver for a 50 BMG caliber! AND IT WORKS?!", //new item description
                    CategoryOverride = ModAPI.FindCategory("SH's Utils"), //new item category
                    ThumbnailOverride = ModAPI.LoadSprite("RevolverSHU/thumb.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("RevolverSHU/RevSHU.png");
                        var firearm = Instance.GetComponent<FirearmBehaviour>();
                        Cartridge customCartridge = ModAPI.FindCartridge("38 Special"); //load a copy of the 9mm cartridge
                        customCartridge.name = ".50 BMG"; //set a name
                        customCartridge.Damage *= 2.5f; //change the damage however you like
                        customCartridge.StartSpeed *= 1.75f; //change the bullet velocity
                        customCartridge.PenetrationRandomAngleMultiplier *= 2.5f; //change the accuracy when the bullet travels through an object
                        customCartridge.Recoil *= 2.3f; //change the recoil
                        customCartridge.ImpactForce *= 1.8f; //change how much the bullet pushes the target

                        //set the cartridge to the FirearmBehaviour
                        firearm.Cartridge = customCartridge;

                        //set the new gun sounds. this is an array of AudioClips that is picked from at random when shot
                        firearm.ShotSounds = new AudioClip[]
                        {
                ModAPI.LoadSound("RevolverSHU/50bmgshu.wav")
                        };

                        // set the collision box to the new sprite shape
                        // this is the easiest way to fix your collision shape, but it also the slowest.
                        Instance.FixColliders();
                    }
                }
            );
            ModAPI.Register(
                            new Modification()
                            {
                                OriginalItem = ModAPI.FindSpawnable("Shotgun"), //item to derive from
                                NameToOrderByOverride = "E",
                                NameOverride = "Panic Attack [TF2] -SHU", //new item name with a suffix to assure it is globally unique
                                DescriptionOverride = "Team Fortress 2 Panic Attack. The sprite was copied directly from the image of the weapon in the game, so it 100% conveys the spirit of TF2", //new item description
                                CategoryOverride = ModAPI.FindCategory("SH's Utils"), //new item category
                                ThumbnailOverride = ModAPI.LoadSprite("PanicAttack/thumb.png"), //new item thumbnail (relative path)
                                AfterSpawn = (Instance) =>
                                {
                                    Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("PanicAttack/PanicAttaaack.png");
                                    var firearm = Instance.GetComponent<FirearmBehaviour>();
                                    Cartridge customCartridge = ModAPI.FindCartridge("30-06"); //load a copy of the 9mm cartridge
                                    customCartridge.name = "12 Gauge"; //set a name
                                    customCartridge.Damage *= 1.3f; //change the damage however you like
                                    customCartridge.StartSpeed *= 1.15f; //change the bullet velocity
                                    customCartridge.PenetrationRandomAngleMultiplier *= 3.0f; //change the accuracy when the bullet travels through an object
                                    customCartridge.Recoil *= 3.0f; //change the recoil
                                    customCartridge.ImpactForce *= 1.2f; //change how much the bullet pushes the target
                                    firearm.Cartridge = customCartridge;
                                    firearm.BulletsPerShot = 4;
                                    firearm.ShotSounds = new AudioClip[]
                                    {
                ModAPI.LoadSound("PanicAttack/tf2_backshot_shotty.wav")
                                    };

                                    // set the collision box to the new sprite shape
                                    // this is the easiest way to fix your collision shape, but it also the slowest.
                                    Instance.FixColliders();
                                }
                            }
                        );
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Bowling Pin"),
                    NameOverride = "Flamboyant Flamenco [TF2] -SHU",
                    NameToOrderByOverride = "E - 1",
                    DescriptionOverride = "Hat of Pyro from TF2.",
                    CategoryOverride = ModAPI.FindCategory("SH's Utils"),
                    ThumbnailOverride = ModAPI.LoadSprite("Flamboyant/thumb.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Flamboyant/Flamboyant.png", 1.15f);
						Instance.GetComponent<SpriteRenderer>().sortingOrder = 15;
                        ArmorBehaviour Armor = Instance.AddComponent<ArmorBehaviour>();

                        Armor.stabResistance = 15f;
                        Armor.armorPiece = "Head";
                        Armor.armorTier = 0;

                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 0.01f;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass = 0.01f;
                        Instance.FixColliders();
				    }
                }
            );
        }

    }
    public class RandomHumanTextureBehaviour : MonoBehaviour
    {
        public List<Texture2D> Textures = new List<Texture2D>();
        int chosenIndex = 0;
        public PersonBehaviour person;
        ContextMenuButton menuButton;
        public void Start()
        {
            chosenIndex = UnityEngine.Random.Range(0, Textures.Count - -1);
            ChangeTexture();
            GetComponent<PhysicalBehaviour>().ContextMenuOptions.Buttons.Add(menuButton = new ContextMenuButton("ChangeTexture", "Change Texture", "Switches to the change human skin", new UnityAction[1]
                 {
                (UnityAction) (() =>
                {
                    ChangeTexture();
                    foreach (var humanSkin in person.Limbs)
                    {
                        if (humanSkin.GetComponent<RandomHumanTextureBehaviour>())
                        {
                           humanSkin.GetComponent<RandomHumanTextureBehaviour>().chosenIndex = chosenIndex;
                        }
                    }

                })
                 }));
        }
        public void ChangeTexture()
        {
            chosenIndex += 1;
            if (chosenIndex > Textures.Count - 1)
                chosenIndex = 0;
            person.SetBodyTextures(Textures[chosenIndex]);
        }
    }
}
