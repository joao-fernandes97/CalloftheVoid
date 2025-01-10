using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Expulsion : MonoBehaviour
{
    [SerializeField] private float _teleportTime = 3f;
    [SerializeField] private Transform _destination;
    [SerializeField] private Volume postProcessingVolume;
    [SerializeField] private AudioSource whisperSource;
    private ColorAdjustments colorAdjustments;
    private YieldInstruction waitInFade;
    
    private Coroutine _expelRot = null;
    private void Awake() {
        if (postProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustmentsComponent))
        {
            colorAdjustments = colorAdjustmentsComponent;
        }
        waitInFade = new WaitForSeconds(1f);
    }

    public void Expel(GameObject target)
    {
        if (_expelRot != null) StopCoroutine(_expelRot);
        _expelRot = StartCoroutine(ExpulsionROT(target,_destination));
    }

    public void Expel(GameObject target,Transform destination)
    {
        if (_expelRot != null) StopCoroutine(_expelRot);
        _expelRot = StartCoroutine(ExpulsionROT(target,destination));
    }

    IEnumerator ExpulsionROT (GameObject target,Transform destination)
    {
        
        //Add visual effect over camera to show teleportation. Perhaps an overbloom fade to white
        //this needs giga work
        //whisper sound fade out
        //and while it is black transform the position
        PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
        
        float startExposure = colorAdjustments.postExposure.value;
        float startVolume = whisperSource.volume;
        float elapsedTime = 0f;

        //fade out
        while (elapsedTime < _teleportTime)
        {
            elapsedTime += Time.deltaTime;
            colorAdjustments.postExposure.value = Mathf.Lerp(startExposure, -15f, elapsedTime / _teleportTime);
            whisperSource.volume = Mathf.Lerp(startVolume,0f,elapsedTime / _teleportTime);
            yield return null;
        }

        
        //teleport while faded
        colorAdjustments.postExposure.value = -15f;
        target.transform.position = destination.position;
        target.transform.rotation = destination.rotation;
        startExposure = colorAdjustments.postExposure.value;
        elapsedTime = 0f;
        yield return waitInFade;

        //fade back in
        while (elapsedTime < _teleportTime)
        {
            elapsedTime += Time.deltaTime;
            colorAdjustments.postExposure.value = Mathf.Lerp(startExposure, 0f, elapsedTime / _teleportTime);
            yield return null;
        }

        playerMovement.enabled = true;

    }
}
