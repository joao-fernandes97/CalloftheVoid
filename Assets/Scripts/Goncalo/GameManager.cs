using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject planetInsertionObject;
    [SerializeField]
    private GameObject orrery;
    [SerializeField]
    private GameObject pedestal;
    [SerializeField]
    private List<LightSwitches> lightSwitches;

    public void SoundPuzzleFinished()
    {
        controlPanel.SetActive(true);
        orrery.SetActive(true);
        pedestal.SetActive(true);
        planetInsertionObject.SetActive(true);
    }

    public void ChangeVisualCues(bool state)
    {
        foreach (LightSwitches lightSwitch in lightSwitches)
        {
            lightSwitch.ChangeVisualCues(state);
        }
    }
}
