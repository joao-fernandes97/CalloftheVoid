using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float selfDestructTime = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SelfDestructCoroutine());
    }

    private IEnumerator SelfDestructCoroutine()
    {
        yield return new WaitForSeconds(selfDestructTime);
    }
}
