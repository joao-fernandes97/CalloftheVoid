using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("Footsteps")]
    [SerializeField] private Transform mainCameraTransform;
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip[] footstepSounds;
    
    private float lastStepTime;
    private float stepInterval = 0.5f;
    private PlayerMovement playerMovement;

    float lastCameraY;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastCameraY = mainCameraTransform.position.y;
        lastStepTime = Time.time;
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentCameraY = mainCameraTransform.position.y;
        if (currentCameraY < lastCameraY)
        {
            if (Time.time - lastStepTime >= stepInterval)
            {
                if(playerMovement.IsMoving())
                {
                    PlayFootstepSound();
                    lastStepTime = Time.time;
                    Debug.Log("Step");
                }
            }
        }
        lastCameraY = currentCameraY;
    }

    void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            AudioClip clip = footstepSounds[randomIndex];

            footstepSource.clip = clip;
            footstepSource.volume = Random.Range(0.1f, 0.15f);
            footstepSource.pitch = Random.Range(0.8f,1.2f);
            footstepSource.Play();
        }
    }
}
