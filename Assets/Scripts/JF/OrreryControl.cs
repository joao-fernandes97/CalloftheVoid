using System;
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
        planetOrbits[activePlanetOrbitIndex].Rotate(0.0f,45.0f,0.0f);
        Debug.Log("RotateRight");
    }

    public void RotateLeft(){
        planetOrbits[activePlanetOrbitIndex].Rotate(0.0f,-45.0f,0.0f);
        Debug.Log("RotateLeft");
    }

    public void Confirm(){
        UICamera.Priority = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
