using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public float lifeTime;

    private void Start()
    {
        StartCoroutine(Life());
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
