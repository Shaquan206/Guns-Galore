using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public int damage;
    public int health;
    public float moveSpeed;
    public float sightRange;

    public float timeBeforeStart;

    public bool isAlive;

    private GameObject player;

    private void Start()
    {
        StartCoroutine(StartLife());
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (sightRange > dist)
            {
                rb.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            GameObject bullet = collision.gameObject;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            health -= bulletScript.damage;
            CheckDead();
        }
    }

    private void CheckDead()
    {
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator StartLife()
    {
        yield return new WaitForSeconds(timeBeforeStart);
        isAlive = true;
    }
}
