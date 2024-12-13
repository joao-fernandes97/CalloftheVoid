using System.Collections;
using UnityEngine;

public class CreatureSound : MonoBehaviour
{
    private bool canBeep = true;
    private float beepBaseInterval = 0.5f;
    private float beepDistanceInterval = 0.5f;
    private Transform player;
    private AudioSource audioSource;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        DistanceToPlayer();
        Beep();
    }

    private void DistanceToPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 20)
        {
            beepDistanceInterval = beepBaseInterval + 0.15f * distance;
        }
        else
        {
            beepDistanceInterval = 3.5f;
        }
    }

    private void Beep()
    {
        if (canBeep)
        {
            canBeep = false;
            audioSource.Play();
            StartCoroutine(BeepIntervalCoroutine());
        }
    }

    private IEnumerator BeepIntervalCoroutine()
    {
        yield return new WaitForSeconds(beepDistanceInterval);
        canBeep = true;
    }
}
