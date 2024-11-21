using NaughtyAttributes;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    [SerializeField] private Transform Origin;
    [SerializeField] private Transform player;
    [SerializeField] private Transform restPoint;
    [SerializeField] private float minDistance;
    [SerializeField] private float RotationSpeed = .0001f;
    [SerializeField] private Animator animator;    
    [SerializeField] private float blinkInterval = 1f;
    private float blinkTracker;
    private bool firstBlink = false;   
    
    public bool canBlink { get; set; } = true;
    private bool _openAnimationDone = false;
    private bool _openEyes = false;
    //What is called everytime the eyes are opened from outside
    private void Awake()
    {
        animator.Play("Default");
    }
    private void OnEnable() 
    {
        firstBlink = false;        
        blinkTracker = blinkInterval;
        OpenEye();
        
    }
    

    void LateUpdate()
    {
        
        if(_openEyes)
        {
            //Open eyes and follow target
            if (!firstBlink) EyeOpen();
            else if ((firstBlink && _openAnimationDone) && canBlink) BlinkRandom();
            FollowTargetLerped(player.position);

        }
        else
        {
            //top following eye and reset
            ResetEye();
        }
        
    }
    
    [Button]
    public void OpenEye() => _openEyes = true;
    [Button]
    public void CloseEye() => _openEyes = false;
    
    private Quaternion LookAtTarget(Vector3 target)
    {
        //Get direction first 
        Vector3 direction = target - Origin.position;
        
        Quaternion rot = Quaternion.LookRotation(direction.normalized,Vector3.up);
        return rot;
    }
    
    private void FollowTargetLerped(Vector3 target)
    {
        //Debug.Log($"Position is being tracked {target}.");
        Origin.rotation = Quaternion.Lerp(Origin.rotation, LookAtTarget(target), RotationSpeed * Time.fixedDeltaTime);
    }
    
   
    //Blinking behaviour:
    private void EyeOpen()
    {
        animator.Play("Open");
        firstBlink = true;
    }
    

    private void ResetEye()
    { 
        animator.Play("Blink");
        blinkTracker = blinkInterval;
        firstBlink = false;
        FollowTargetLerped(restPoint.position);
       
    }
    private void BlinkRandom()
    {
        blinkTracker -= Time.fixedDeltaTime;
        //Debug.Log($"blinktracker = {blinkTracker}");
        if (blinkTracker <= 0f )
        {
            //Debug.Log("Should had blinked");
            animator.Play("Blink and Open");
            animator.CrossFade("Inbetween Blinks", blinkInterval);
            blinkTracker = Random.Range(blinkInterval - .2f, blinkInterval + .2f);
            
        }       


    }

    public void EyeIsOpening() => _openAnimationDone = false;
    
    public void EyeDoneOpening() => _openAnimationDone = true;
    




}
