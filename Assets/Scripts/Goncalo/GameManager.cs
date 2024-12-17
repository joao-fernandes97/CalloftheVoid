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

    public void SoundPuzzleFinished()
    {
        controlPanel.SetActive(true);
        orrery.SetActive(true);
        pedestal.SetActive(true);
        planetInsertionObject.SetActive(true);
    }
}
