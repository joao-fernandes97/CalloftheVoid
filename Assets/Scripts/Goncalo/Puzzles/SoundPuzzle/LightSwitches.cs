using System.Collections;
using UnityEngine;

public class LightSwitches : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int switchNumber = 0;
    [SerializeField]
    private bool finalSwitch = false;

    private bool activated = false;
    private bool beeping = false;
    private bool canBeep = true;
    private float beepBaseInterval = 0.5f;
    private float beepDistanceInterval = 0.5f;
    private Transform player;
    private AudioSource audioSource;
    private SoundPuzzle soundPuzzle;


    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        audioSource = GetComponent<AudioSource>();
        soundPuzzle = GetComponentInParent<SoundPuzzle>();
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
        if (beeping && canBeep)
        {
            canBeep = false;
            audioSource.Play();
            StartCoroutine(BeepIntervalCoroutine());
        }
    }

    public void Interact()
    {
        if (beeping && !activated)
        {
            activated = true;
            beeping = false;
            audioSource.Stop();
            soundPuzzle.SwitchActivated(switchNumber, finalSwitch);
        }
    }

    public void StartBeeping()
    {
        beeping = true;
    }

    private IEnumerator BeepIntervalCoroutine()
    {
        yield return new WaitForSeconds(beepDistanceInterval);
        canBeep = true;
    }
}
