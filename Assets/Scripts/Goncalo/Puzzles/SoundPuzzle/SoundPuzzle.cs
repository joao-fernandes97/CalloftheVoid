using UnityEngine;

public class SoundPuzzle : MonoBehaviour
{
    [SerializeField]
    private int startingSwitch = 0;
    [SerializeField]
    private GameManager gameManager;

    //Remove this after TP2
    /*[SerializeField]
    private GameObject endPanel;*/

    private int numOfSwitches = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.GetChild(startingSwitch).GetComponent<LightSwitches>().StartBeeping();
        numOfSwitches = transform.childCount - 1;
    }

    public void SwitchActivated(int switchNumber, bool finalSwitch)
    {
        int nextSwitch = switchNumber + 1;
        if (finalSwitch || nextSwitch > numOfSwitches)
        {
            PuzzleComplete();
        }
        else
        {
            transform.GetChild(nextSwitch).GetComponent<LightSwitches>().StartBeeping();
        }
    }

    private void PuzzleComplete()
    {
        Debug.Log("You did it!");
        RenderSettings.fog = false;
        //Remove this after TP2
        /*endPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;*/
        gameManager.SoundPuzzleFinished();
    }
}
