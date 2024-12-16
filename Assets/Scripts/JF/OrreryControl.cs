using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class OrreryControl : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField]
    private LayerMask planetsMask;
    [SerializeField]
    private LayerMask outlineMask;

    private int planetsMaskBit;
    private int outlineMaskBit;

    [Header("Transforms")]
    [SerializeField]
    private Transform jupiterTransform;
    [SerializeField]
    private Transform saturnTransform;
    [SerializeField]
    private Transform neptuneTransform;
    //[SerializeField]
    //private Transform[] planetOrbits;
    [SerializeField]
    private List<Transform> planetOrbits;
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

    private InteractablePanel interactablePanel;
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

        planetsMaskBit = (int)Mathf.Log(planetsMask.value, 2);
        outlineMaskBit = (int)Mathf.Log(outlineMask.value, 2);
        interactablePanel = GetComponent<InteractablePanel>();
    }

    private void OnDestroy()
    {
        buttonLeft.onClick.RemoveListener(RotateLeft);
        buttonRight.onClick.RemoveListener(RotateRight);
        buttonOuter.onClick.RemoveListener(SelectOuter);
        buttonInner.onClick.RemoveListener(SelectInner);
        buttonConfirm.onClick.RemoveListener(Confirm);
    }
    
    /// <summary>
    /// Select an outer planet if possible and adds an outline to the selected planet
    /// </summary>
    public void SelectOuter(){
        //activePlanetOrbitIndex = Math.Clamp(activePlanetOrbitIndex+1,0,planetOrbits.Length-1);
        activePlanetOrbitIndex = Math.Clamp(activePlanetOrbitIndex + 1, 0, planetOrbits.Count - 1);
        Debug.Log($"SelectOuter: {activePlanetOrbitIndex}");
        ResetLayers();
        planetOrbits[activePlanetOrbitIndex].transform.GetChild(0).gameObject.layer = outlineMaskBit;
    }

    /// <summary>
    /// Select an inner planet if possible and adds an outline to the selected planet
    /// </summary>
    public void SelectInner(){
        //activePlanetOrbitIndex = Math.Clamp(activePlanetOrbitIndex-1,0,planetOrbits.Length-1);
        activePlanetOrbitIndex = Math.Clamp(activePlanetOrbitIndex - 1, 0, planetOrbits.Count - 1);
        Debug.Log($"SelectInner: {activePlanetOrbitIndex}");
        ResetLayers();
        planetOrbits[activePlanetOrbitIndex].transform.GetChild(0).gameObject.layer = outlineMaskBit;
    }

    /// <summary>
    /// Resets the planets layers to remove the outlines
    /// </summary>
    private void ResetLayers()
    {
        foreach (Transform transform in planetOrbits)
        {
            transform.GetChild(0).gameObject.layer = planetsMaskBit;
        }
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

    /// <summary>
    /// Method that is called when exiting the panel
    /// </summary>
    public void Confirm(){
        ResetLayers();
        interactablePanel.ExitInteraction();
    }


    /// <summary>
    /// Adds the inserted planet to the possible planet selection
    /// Activates the planet's representation on the orrery
    /// </summary>
    /// <param name="planet"></param>
    public void InsertPlanet(PlanetsEnum planet)
    {
        switch (planet)
        {
            case PlanetsEnum.Jupiter:
                jupiterTransform.gameObject.SetActive(true);
                planetOrbits.Add(jupiterTransform);
                return;
            case PlanetsEnum.Saturn:
                saturnTransform.gameObject.SetActive(true);
                planetOrbits.Add(saturnTransform);
                return;
            case PlanetsEnum.Neptune:
                neptuneTransform.gameObject.SetActive(true);
                planetOrbits.Add(neptuneTransform);
                return;
        }
    }

    /// <summary>
    /// Resets all planets' layers to remove the outline
    /// Sets the outline to the currently selected planet
    /// </summary>
    public void StartInteraction()
    {
        ResetLayers();
        planetOrbits[activePlanetOrbitIndex].transform.GetChild(0).gameObject.layer = outlineMaskBit;
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
