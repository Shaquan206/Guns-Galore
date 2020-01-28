using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //public GameObject gunShot;
    //public GameObject gunReload;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float startDelay;
    public int damage;
    public float spread;
    public int magSize;
    public float shootDelay;
    public float reloadTime;
    public float bulletMinSpeed = 20f;
    public float bulletMaxSpeed = 20f;
    public float bulletLifeTime;
    public int amountOfShots = 1;
    public bool isAutomatic;
    public int amo;

    private bool isReloading;
    private bool isShooting;
    private bool started;

    private void Start()
    {
        if (amo == 0)
        {
            amo = magSize;
        }
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        started = true;
    }

    private void Update()
    {
        if (started == true)
        {
            if (isReloading == false)
            {
                if (isAutomatic)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        if (isShooting == false)
                        {
                            StartCoroutine(Shoot());
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (isShooting == false)
                        {
                            StartCoroutine(Shoot());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(Reload());
                }
            }
        }
    }

    IEnumerator Shoot()
    {
        if (amo > 0)
        {
            isShooting = true;
            amo--;
            //Instantiate(gunShot, transform.position, Quaternion.identity);
            for (int x = 0; x < amountOfShots; x++)
            {
                float randomSpread = Random.Range(-spread, spread);
                float randomSpeed = Random.Range(bulletMinSpeed, bulletMaxSpeed);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bulletCs = bullet.GetComponent<Bullet>();
                bullet.transform.Rotate(0, 0, randomSpread);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                bulletCs.damage = damage;
                bulletCs.lifeTime = bulletLifeTime;
                rb.AddForce(bullet.transform.up * randomSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(shootDelay);
            isShooting = false;
        }
        else
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        //Instantiate(gunReload, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(reloadTime);
        amo = magSize;
        isReloading = false;
    }
}
