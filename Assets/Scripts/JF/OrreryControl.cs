using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class OrreryControl : MonoBehaviour
{
    [SerializeField]
    private Transform[] planetOrbits;
    [SerializeField]
    private CinemachineCamera UICamera;
    [SerializeField]
    private Button buttonLeft;
    [SerializeField]
    private Button buttonRight;
    [SerializeField]
    private Button buttonInner;
    [SerializeField]
    private Button buttonOuter;
    [SerializeField]
    private Button buttonConfirm;

    private int activePlanetOrbitIndex;
    private bool isRotating;
    

    private void Start()
    {
        activePlanetOrbitIndex = 0;
        buttonLeft.onClick.AddListener(RotateLeft);
        buttonRight.onClick.AddListener(RotateRight);
        buttonOuter.onClick.AddListener(SelectOuter);
        buttonInner.onClick.AddListener(SelectInner);
        buttonConfirm.onClick.AddListener(Confirm);
    }

    private void OnDestroy()
    {
        buttonLeft.onClick.RemoveListener(RotateLeft);
        buttonRight.onClick.RemoveListener(RotateRight);
        buttonOuter.onClick.RemoveListener(SelectOuter);
        buttonInner.onClick.RemoveListener(SelectInner);
        buttonConfirm.onClick.RemoveListener(Confirm);
    }
    
    public void SelectOuter(){
        activePlanetOrbitIndex = Math.Clamp(activePlanetOrbitIndex+1,0,planetOrbits.Length-1);
        Debug.Log($"SelectOuter: {activePlanetOrbitIndex}");
    }

    public void SelectInner(){
        activePlanetOrbitIndex = Math.Clamp(activePlanetOrbitIndex-1,0,planetOrbits.Length-1);
        Debug.Log($"SelectInner: {activePlanetOrbitIndex}");
    }

    public void RotateRight(){
        if(!isRotating){
            StartCoroutine(RotateSmooth(planetOrbits[activePlanetOrbitIndex],45.0f,1.0f));
            Debug.Log("RotateRight");
        }
    }

    public void RotateLeft(){
        if(!isRotating){
            StartCoroutine(RotateSmooth(planetOrbits[activePlanetOrbitIndex],-45.0f,1.0f));
            Debug.Log("RotateLeft");
        }
    }

    public void Confirm(){
        UICamera.Priority = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //rotationCoroutine
    private IEnumerator RotateSmooth(Transform target, float angle, float duration)
    {
        isRotating = true;
        Quaternion initialRotation = target.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f,angle,0f);
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            target.rotation = Quaternion.Slerp(initialRotation, targetRotation, timeElapsed/duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        target.rotation = targetRotation;
        isRotating = false;
    }
}
