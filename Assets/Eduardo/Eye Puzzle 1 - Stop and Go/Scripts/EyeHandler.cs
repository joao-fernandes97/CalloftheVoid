using System.Collections;
using UnityEngine;

public class EyeHandler : MonoBehaviour
{
    [SerializeField] private bool _blinks = false;
    private EyeFollow _eyeFollowScript;
    private WeightedTransformCopy_v2 _weightedTransformsCopyScript;
    private Animator _eyeAnimator;
    private float _resetDelay = 1f;
    private Coroutine closingRoutine = null;

     
    private void FixedUpdate() 
    {
        if (_eyeFollowScript.canBlink != _blinks)
        _eyeFollowScript.canBlink = _blinks;

    }
    
    void Awake()
    {
        _eyeFollowScript = GetComponentInChildren<EyeFollow>(true);
        _weightedTransformsCopyScript = GetComponentInChildren<WeightedTransformCopy_v2>(true);
        _eyeAnimator = GetComponentInChildren<Animator>(true);

        
        //Eyes off by default

        if (_eyeFollowScript != null) _eyeFollowScript.enabled = false;
        if (_weightedTransformsCopyScript != null) _weightedTransformsCopyScript.enabled = false;
        if (_eyeAnimator != null) _eyeAnimator.Play("Default");
    }   
    public void EnableEye()
    {
        Debug.Log($"EyeHandler: {gameObject.name} EnableEyes() triggered");
        if (closingRoutine != null)
        {
            StopCoroutine(closingRoutine);
            _eyeFollowScript.enabled = false;
            _weightedTransformsCopyScript.enabled = false;
        }
        _eyeFollowScript.enabled = true;
        _weightedTransformsCopyScript.enabled = true;
        
    }
    public void DisableEye()
    {
        Debug.Log($"EyeHandler: {gameObject.name} DisableEye() triggered");
        closingRoutine = StartCoroutine(CloseEyeAndDisableScript(_resetDelay));
        
    }   
    IEnumerator CloseEyeAndDisableScript(float delay)
    {

        _eyeFollowScript.CloseEye();
        yield return new WaitForSeconds(delay);       
        _eyeFollowScript.enabled = false;
        _weightedTransformsCopyScript.enabled = false;
       

    }
    
  



}
