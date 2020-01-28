using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    public Text gun1Text;
    public Text gun1amoText;
    public Text gun2Text;
    public Text gun2amoText;

    public Transform weaponHolder;
    public GameObject startingGun;
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Fists;

    public GameObject GunCrate;

    private bool isGun1Owned;
    private bool isGun2Owned;

    GameObject equippedGun;

    int currentGun;
    int gun1amo;
    int gun2amo;

    private void Start()
    {
        if (startingGun != null)
        {
            EquipGun(startingGun);
            currentGun = 1;
            isGun1Owned = true;
        }
        else
        {
            EquipGun(Fists);
            currentGun = 0;
        }
    }

    private void Update()
    {
        gun1Text.text = Gun1.name;
        gun2Text.text = Gun2.name;
        gun1amoText.text = gun1amo.ToString();
        gun2amoText.text = gun2amo.ToString();

        if (currentGun == 1)
        {
            GameObject gun = equippedGun;
            Gun gunScript = gun.GetComponent<Gun>();
            gun1amo = gunScript.amo;
        }
        else if (currentGun == 2)
        {
            GameObject gun = equippedGun;
            Gun gunScript = gun.GetComponent<Gun>();
            gun2amo = gunScript.amo;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (currentGun == 1)
            {
                if (isGun2Owned)
                {
                    EquipGun(Gun2);
                    currentGun = 2;
                    GameObject gun = equippedGun;
                    Gun gunScript = gun.GetComponent<Gun>();
                    gunScript.amo = gun2amo;
                }
            }
            else if (currentGun == 2)
            {
                if (isGun1Owned)
                {
                    EquipGun(Gun1);
                    currentGun = 1;
                    GameObject gun = equippedGun;
                    Gun gunScript = gun.GetComponent<Gun>();
                    gunScript.amo = gun1amo;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentGun == 1)
            {
                isGun1Owned = false;
                GameObject crate = Instantiate(GunCrate, transform.position, Quaternion.identity);
                GunCrate crateScript = crate.GetComponent<GunCrate>();
                crateScript.gun = Gun1;
                crateScript.gunAmo = gun1amo;
                if (isGun2Owned)
                {
                    EquipGun(Gun2);
                    currentGun = 2;
                }
                else
                {
                    EquipGun(Fists);
                    currentGun = 0;
                }
            }
            else if (currentGun == 2)
            {
                isGun2Owned = false;
                GameObject crate = Instantiate(GunCrate, transform.position, Quaternion.identity);
                GunCrate crateScript = crate.GetComponent<GunCrate>();
                crateScript.gun = Gun2;
                crateScript.gunAmo = gun2amo;
                if (isGun1Owned)
                {
                    EquipGun(Gun1);
                    currentGun = 1;
                }
                else
                {
                    EquipGun(Fists);
                    currentGun = 0;
                }
            }
        }
    }

    public void EquipGun(GameObject gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip, weaponHolder.position, weaponHolder.rotation);
        equippedGun.transform.parent = weaponHolder;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (collision.tag.Equals("Gun Crate"))
            {
                GameObject crate1 = collision.gameObject;
                GunCrate crateScript1 = crate1.GetComponent<GunCrate>();
                if (isGun1Owned == false)
                {
                    Gun1 = crateScript1.gun;
                    isGun1Owned = true;
                    Destroy(collision.gameObject);
                    EquipGun(Gun1);
                    currentGun = 1;
                    GameObject gun = equippedGun;
                    Gun gunScript = gun.GetComponent<Gun>();
                    gunScript.amo = crateScript1.gunAmo;
                }
                else if (isGun2Owned == false)
                {
                    Gun2 = crateScript1.gun;
                    isGun2Owned = true;
                    Destroy(collision.gameObject);
                    EquipGun(Gun2);
                    currentGun = 2;
                    GameObject gun = equippedGun;
                    Gun gunScript = gun.GetComponent<Gun>();
                    gunScript.amo = crateScript1.gunAmo;
                }
                else if (currentGun == 1)
                {
                    GameObject crate2 = Instantiate(GunCrate, transform.position, Quaternion.identity);
                    GunCrate crateScript2 = crate2.GetComponent<GunCrate>();
                    crateScript2.gun = Gun1;
                    Gun1 = crateScript1.gun;
                    isGun1Owned = true;
                    Destroy(collision.gameObject);
                    EquipGun(Gun1);
                    currentGun = 1;
                    GameObject gun = equippedGun;
                    Gun gunScript = gun.GetComponent<Gun>();
                    gunScript.amo = crateScript1.gunAmo;
                }
                else if (currentGun == 2)
                {
                    GameObject crate2 = Instantiate(GunCrate, transform.position, Quaternion.identity);
                    GunCrate crateScript2 = crate2.GetComponent<GunCrate>();
                    crateScript2.gun = Gun2;
                    Gun2 = crateScript1.gun;
                    isGun2Owned = true;
                    Destroy(collision.gameObject);
                    EquipGun(Gun2);
                    currentGun = 2;
                    GameObject gun = equippedGun;
                    Gun gunScript = gun.GetComponent<Gun>();
                    gunScript.amo = crateScript1.gunAmo;
                }
            }
        }
    }
}