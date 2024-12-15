using System.Collections;
using UnityEngine;

public class PlanetCollectibles : MonoBehaviour, IInteractable, IUsable
{
    [SerializeField]
    private LayerMask defaultMask;
    [SerializeField]
    private PlanetsEnum planetsEnum;
    [SerializeField]
    private GameObject unusableText;
    [SerializeField]
    private GameObject playerSpot;
    [SerializeField]
    private PlayerInventory playerInventory;
    [SerializeField]
    private int itemNumber = 0;
    [SerializeField]
    private GameObject cameraCenter;

    private float detectionRange = 1.55f;
    private bool usedRecently = false;

    public void Interact()
    {
        transform.parent = playerSpot.transform;

        //reseting the lantern's local position and rotation to make sure it always looks the same
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        //Does the necessary changes to the inventory
        playerInventory.NewItem(itemNumber);

        gameObject.layer = (int)Mathf.Log(defaultMask.value, 2);
    }

    public void Use()
    {
        if (!usedRecently)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraCenter.transform.position, cameraCenter.transform.TransformDirection(Vector3.forward), out hit, detectionRange))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.transform.GetComponent<PlanetInsertion>() != null)
                {
                    hit.transform.GetComponent<PlanetInsertion>().InsertPlanet(planetsEnum);
                    transform.parent.gameObject.SetActive(false);
                    Destroy(gameObject);
                }
                else
                {
                    StartCoroutine(InteractedCoroutine());
                    StartCoroutine(UsedRecentlyCoroutine());
                }
            }
        }
        else
        {
            return;
        }
    }

    private IEnumerator InteractedCoroutine()
    {
        //Activates a text indicating that the cube was interacted with
        //Changes which portal is active
        unusableText.SetActive(true);
        yield return new WaitForSeconds(.5f);
        unusableText.SetActive(false);
    }

    private IEnumerator UsedRecentlyCoroutine()
    {
        usedRecently = true;
        yield return new WaitForSeconds(.5f);
        usedRecently = false;
    }
}
