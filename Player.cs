using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    public Text healthText;

    private void Update()
    {
        healthText.text = "" + health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript.isAlive)
            {
                health -= enemyScript.damage;
                CheckDead();
            }
        }
    }

    private void CheckDead()
    {
        if (health < 1)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
