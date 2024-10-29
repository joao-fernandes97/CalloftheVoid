using System.Collections;
using UnityEngine;

public class Pedestal : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject interactedText;
    [SerializeField]
    private GameObject lantern;
    [SerializeField]
    private GameObject pedestalSpot;
    [SerializeField]
    private GameObject playerSpot;

    private bool hasLantern;

    private void Start()
    {
        hasLantern = true;
    }

    private void FixedUpdate()
    {
        if (hasLantern)
        {
            //insert code to transfer energy to lantern
        }
    }

    public void Interact()
    {
        if (hasLantern)
        {
            lantern.transform.parent = playerSpot.transform;
            lantern.transform.localPosition = Vector3.zero;
            hasLantern = false;
        }
        else if (!hasLantern)
        {
            lantern.transform.parent = pedestalSpot.transform;
            lantern.transform.localPosition = Vector3.zero;
            hasLantern = true;
        }

        StartCoroutine(InteractedCoroutine());
    }

    private IEnumerator InteractedCoroutine()
    {
        //Activates a text indicating that the cube was interacted with
        //Changes which portal is active
        interactedText.SetActive(true);
        yield return new WaitForSeconds(.5f);
        interactedText.SetActive(false);
    }
}
