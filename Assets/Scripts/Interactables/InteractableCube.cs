using System.Collections;
using UnityEngine;

public class InteractableCube : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject interactedText;

    public void Interact()
    {
        StartCoroutine(InteractedCoroutine());
    }

    private IEnumerator InteractedCoroutine()
    {
        interactedText.SetActive(true);
        yield return new WaitForSeconds(.5f);
        interactedText.SetActive(false);
    }
}
