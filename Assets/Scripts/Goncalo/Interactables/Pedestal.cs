using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Pedestal : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject lantern;
    [SerializeField]
    private GameObject pedestalSpot;
    [SerializeField]
    private GameObject playerSpot;
    [SerializeField]
    private Portal portal;
    [SerializeField]
    private int itemNumber;
    private PlayerInventory playerInventory;



    private bool hasLantern;
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
    }

    /// <summary>
    /// This method will get called through an event from the Portal class
    /// </summary>
    /// <param name="planet"></param>
    private void PedestalPlanetChange(PlanetsEnum planet)
    {
        Planet = planet;
    }

    private void OnEnable()
    {
        portal.PlanetChanged += PedestalPlanetChange;
    }

    private void OnDisable()
    {
        portal.PlanetChanged -= PedestalPlanetChange;
    }
}
