using System.Collections;
using UnityEngine;

public class EyeHandler : MonoBehaviour
{
    private EyeFollow _eyeFollowScript;
    private WeightedTransformCopy_v2 _weightedTransformsCopyScript;
    private Animator _eyeAnimator;
    
    void Awake()
    {
        _eyeFollowScript = GetComponentInChildren<EyeFollow>();
        _weightedTransformsCopyScript = GetComponentInChildren<WeightedTransformCopy_v2>();
        _eyeAnimator = GetComponentInChildren<Animator>();
        
        //Eyes off by default

        if (_eyeFollowScript != null) _eyeFollowScript.enabled = false;
        if (_weightedTransformsCopyScript != null) _weightedTransformsCopyScript.enabled = false;
        if (_eyeAnimator != null) _eyeAnimator.Play("Default");
    }   
    public void EnableEye()
    {
        _eyeFollowScript.enabled = true;
        _weightedTransformsCopyScript.enabled = true;
    }
    public void DisableEye()
    {
        
        
        StartCoroutine(CloseEyeAndDisableScript());
        


    }   
    IEnumerator CloseEyeAndDisableScript()
    {
        _eyeFollowScript.ResetEye();
        while (!_eyeFollowScript.CheckIfAtRest())
        {
            Debug.Log("Closing eye...");
            yield return null;
           
        }
        //
        _eyeFollowScript.enabled = false;
        _weightedTransformsCopyScript.enabled = false;
        Debug.Log("Eye closed...");

    }
  



}
