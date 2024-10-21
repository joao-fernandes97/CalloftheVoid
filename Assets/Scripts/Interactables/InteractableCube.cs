using System.Collections;
using UnityEngine;

public class InteractableCube : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject interactedText;

    private Portal portal;

    private void Start()
    {
        portal = FindAnyObjectByType<Portal>();
    }

    public void Interact()
    {
        StartCoroutine(InteractedCoroutine());
    }

    private IEnumerator InteractedCoroutine()
    {
        //Activates a text indicating that the cube was interacted with
        //Changes which portal is active
        interactedText.SetActive(true);
        portal.ChangePlanet(GetComponent<Planet>().PlanetName);
        yield return new WaitForSeconds(.5f);
        interactedText.SetActive(false);
    }
}
