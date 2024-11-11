using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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

    [SerializeField]
    private int itemNumber;
    private PlayerInventory playerInventory;

    private PlanetsEnum planet;
    public PlanetsEnum Planet
    {
        get
        {
            return planet;
        }
        set
        {
            planet = value;
            if (hasLantern)
            {
                //Immediately changes the lanter's energy if the lantern is on the pedestal
                lantern.GetComponent<Lantern>().Planet = value;
            }
        }
    }

    private bool hasLantern;

    private void Start()
    {
        hasLantern = true;
        playerInventory = FindAnyObjectByType<PlayerInventory>();
    }

    public void Interact()
    {
        //If the pedestal has the lantern, it moves to the player
        if (hasLantern)
        {
            lantern.transform.parent = playerSpot.transform;

            //reseting the lantern's local position and rotation to make sure it always looks the same
            lantern.transform.localPosition = Vector3.zero;
            lantern.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

            lantern.GetComponent<Lantern>().OnPlayer = true;

            //Does the necessary changes to the inventory
            playerInventory.NewItem(itemNumber);
            hasLantern = false;
        }
        //If the player has the lantern, it moves to the pedestal
        else if (!hasLantern && lantern.activeInHierarchy)
        {
            lantern.transform.parent = pedestalSpot.transform;

            //reseting the lantern's local position and rotation to make sure it always looks the same
            lantern.transform.localPosition = Vector3.zero;
            lantern.transform.localRotation = Quaternion.Euler(0f,0f,0f);
            lantern.GetComponent<Lantern>().OnPlayer = false;

            //Deactivates the player's lantern slot
            playerSpot.SetActive(false);
            hasLantern = true;

            //sets the lantern's energy when the player puts it on the pedestal
            lantern.GetComponent<Lantern>().Planet = Planet;
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
