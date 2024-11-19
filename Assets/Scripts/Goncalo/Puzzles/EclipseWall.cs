using UnityEngine;

public class EclipseWall : MonoBehaviour
{
    [SerializeField]
    private GameObject wallModel;

    [SerializeField]
    private PlanetsEnum portalPlanet;
    private PlanetsEnum portalPlanetActive;

    private PlanetsEnum lanternPlanetActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
}
