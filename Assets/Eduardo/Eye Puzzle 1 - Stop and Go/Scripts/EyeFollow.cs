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
    private bool active = false;
    private bool _resetDone = false;
   


    //Separate behavior into:
    //Follow
    //Blinking and Opening
    
    public bool _canBlink { get; set; } = true;
    private bool _openAnimationDone = false;

   
    private void Awake()    {
        active = true;
        blinkTracker = blinkInterval;
        animator.Play("Default");
    }
    


    void LateUpdate()
    {
        
        if(active)
        {
            if (!firstBlink) EyeOpen();
            else if ((firstBlink && _openAnimationDone) && _canBlink) BlinkRandom();
            FollowTargetLerped(player.position);

        }
        else
        {
            FollowTargetLerped(restPoint.position);

        }
        
        
    }

    

    private Quaternion LookAtTarget(Vector3 target)
    {
        //Get direction first 
        Vector3 direction = target - Origin.position;
        
        Quaternion rot = Quaternion.LookRotation(direction.normalized,Vector3.up);
        return rot;
    }
    private bool FacingTarget(Vector3 target)
    {
        Vector3 direction = (target - Origin.position).normalized;
        if(Vector3.Dot(Origin.forward,direction)< .7f) return false; 
        else return true;

    }
    public bool CheckIfAtRest() => FacingTarget(restPoint.position);
    private void FollowTargetLerped(Vector3 target)
    {
        Origin.rotation = Quaternion.Lerp(Origin.rotation, LookAtTarget(target), RotationSpeed * Time.fixedDeltaTime);
    }
    
   
    //Blinking behaviour:
    private void EyeOpen()
    {
        animator.Play("Open");
        firstBlink = true;
    } 
    public void ResetEye()
    {
        active = false;
        animator.Play("Blink");
        blinkTracker = blinkInterval;
        firstBlink = false;
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
