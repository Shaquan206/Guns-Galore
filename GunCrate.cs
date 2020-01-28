using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCrate : MonoBehaviour
{
    public GameObject[] guns;
    public GameObject gun;
    public int gunAmo;

    private void Start()
    {
        int index = Random.Range(0, guns.Length);
        gun = guns[index];
    }
}
