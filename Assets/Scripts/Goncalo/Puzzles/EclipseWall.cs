using UnityEngine;

public class EclipseWall : MonoBehaviour
{
    [SerializeField]
    private GameObject wallModel;
    [SerializeField]
    private Portal portal;
    [SerializeField]
    private PlanetsEnum portalPlanet;
    private PlanetsEnum portalPlanetActive;


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("detected something");
        //Checks for the lantern
        if (other.GetComponent<Lantern>() != null)
        {
            Lantern lantern = other.GetComponent<Lantern>();
            Debug.Log("detected lantern");
            //If the lantern has Neptune's energy, the eye dies
            if (lantern.Planet == portalPlanet && lantern.emitingEnergy)
            {
                Debug.Log("detected neptune");
                wallModel.SetActive(false);
            }
            else if (lantern.Planet == portalPlanet && !lantern.emitingEnergy)
            {
                wallModel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Lantern>() != null && portalPlanetActive != portalPlanet)
        {
            wallModel.SetActive(true);
        }
    }

    /// <summary>
    /// This method will get called through an event from the Portal class
    /// </summary>
    /// <param name="planet"></param>
    public void ChangePortalActive(PlanetsEnum activePortalPlanet)
    {
        portalPlanetActive = activePortalPlanet;

        if (portalPlanetActive == portalPlanet)
        {
            wallModel.SetActive(false);
        }
        else
        {
            wallModel.SetActive(true);
        }
    }

    private void OnEnable()
    {
        portal.PlanetChanged += ChangePortalActive;
    }

    private void OnDisable()
    {
        portal.PlanetChanged -= ChangePortalActive;
    }
}
