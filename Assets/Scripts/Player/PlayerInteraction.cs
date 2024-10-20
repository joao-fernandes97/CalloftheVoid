using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableLayer;
    [SerializeField]
    private float interactableDetectionRange = 1f;
    [SerializeField]
    private GameObject interactableText;

    private bool interactableDetected = false;
    private GameObject interactable;

    private void Update()
    {
        //Checks if there's a detected interable and if the player wants to interact
        if (interactableDetected && Input.GetKeyDown(KeyCode.E))
        {
            //Gets a component that is interactable and interacts independently of the object or what it does
            //All interactable classes need to implement IInteractable
            interactable.GetComponent<IInteractable>().Interact();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Checking if there is any interactable in range while the player is looking at it
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactableDetectionRange, interactableLayer.value))
        {
            //Enables the Interact text, enables interaction and assigns the hit objects as the interactable object
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            interactable = hit.transform.gameObject;
            interactableText.SetActive(true);
            interactableDetected = true;
        }
        else
        {
            //Disables the Interact text and disables interaction
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * interactableDetectionRange, Color.white);
            interactableText.SetActive(false);
            interactableDetected = false;
        }
    }
}
