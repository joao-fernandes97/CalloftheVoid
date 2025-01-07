using System.Collections;
using UnityEngine;

public class LightSwitches : MonoBehaviour, IInteractable
{
    [SerializeField]
    private LayerMask defaultMask;
    [SerializeField]
    private int switchNumber = 0;
    [SerializeField]
    private bool finalSwitch = false;
    [SerializeField]
    private AudioClip wrongSwitchSound;
    [SerializeField]
    private GameObject sonarParticles;
    [SerializeField]
    private bool sonarActive = false;

    private bool activated = false;
    private bool beeping = false;
    private bool canBeep = true;
    private float beepBaseInterval = 0.5f;
    private float beepDistanceInterval = 0.5f;
    private Transform player;
    private AudioSource audioSource;
    private SoundPuzzle soundPuzzle;
    private bool wrongSoundPlaying = false;


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
            if (sonarActive)
            {
                GameObject sonarObject = Instantiate(sonarParticles, transform.position, Quaternion.identity);
                float timeToDie = sonarObject.GetComponentInChildren<ParticleSystem>().main.startLifetime.constant;
                Destroy(sonarObject, timeToDie);
            }
        }
    }

    public void Interact()
    {
        //If the switch is emiting sound the switch becomes activated, stopping the sound and progressing the puzzle
        if (beeping && !activated)
        {
            activated = true;
            beeping = false;
            audioSource.Stop();
            soundPuzzle.SwitchActivated(switchNumber, finalSwitch);
            gameObject.layer = (int)Mathf.Log(defaultMask.value, 2);
        }
        //if its not the correct switch and its not playing the wrong switch sound, plays it
        else if (!wrongSoundPlaying)
        {
            audioSource.PlayOneShot(wrongSwitchSound);
            StartCoroutine(WrongSoundPlayingCoroutine());
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

    /// <summary>
    /// Coroutine to prevent the player from spamming the wrong switch sound
    /// </summary>
    /// <returns></returns>
    private IEnumerator WrongSoundPlayingCoroutine()
    {
        wrongSoundPlaying = true;
        yield return new WaitForSeconds(wrongSwitchSound.length);
        wrongSoundPlaying = false;
    }

    public void ChangeVisualCues(bool state)
    {
        sonarActive = state;
    }
}
