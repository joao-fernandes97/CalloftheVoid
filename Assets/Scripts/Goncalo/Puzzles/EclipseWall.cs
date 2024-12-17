using UnityEngine;

public class EclipseWall : MonoBehaviour
{
    [SerializeField]
    private GameObject wallModel;
    [SerializeField]
    private GameObject jupiterSymbol;
    [SerializeField]
    private GameObject saturnSymbol;
    [SerializeField]
    private Portal portal;
    [SerializeField]
    private PlanetsEnum portalPlanet;
    private PlanetsEnum portalPlanetActive;

    [SerializeField]
    private float timeToDisappear = 1f;
    private bool disappearing = false;

    private Color materialColor;
    private Color jupiterColor;
    private Color saturnColor;
    private float alpha = 1f;

    private void Start()
    {
        materialColor = wallModel.GetComponent<Renderer>().material.color;
        jupiterColor = jupiterSymbol.GetComponent<Renderer>().material.color;
        saturnColor = saturnSymbol.GetComponent<Renderer>().material.color;
    }

    private void FixedUpdate()
    {
        if (disappearing)
        {
            alpha -= Time.fixedDeltaTime/ timeToDisappear;
            if (alpha <= 0f)
            {
                alpha = 0f;
                wallModel.SetActive(false);
            }
        }
        else
        {
            alpha += Time.fixedDeltaTime / timeToDisappear;
            if (alpha > 1f)
            {
                alpha = 1f;
            }
        }

        if (alpha > 0f)
        {
            wallModel.SetActive(true);
        }

        materialColor = new Color(materialColor.r, materialColor.g, materialColor.b, alpha);
        jupiterColor = new Color(jupiterColor.r, jupiterColor.g, jupiterColor.b, alpha);
        saturnColor = new Color(saturnColor.r, saturnColor.g, saturnColor.b, alpha);
        wallModel.GetComponent<Renderer>().material.color = materialColor;
        jupiterSymbol.GetComponent<Renderer>().material.color = materialColor;
        saturnSymbol.GetComponent<Renderer>().material.color = materialColor;
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
                //wallModel.SetActive(false);
                disappearing = true;
            }
            else if (lantern.Planet == portalPlanet && !lantern.emitingEnergy && portalPlanetActive != portalPlanet)
            {
                //wallModel.SetActive(true);
                disappearing = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Lantern>() != null && portalPlanetActive != portalPlanet)
        {
            wallModel.SetActive(true);
            disappearing = false;
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
            //wallModel.SetActive(false);
            disappearing = true;
        }
        else
        {
            //wallModel.SetActive(true);
            disappearing = false;
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
