using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FleshWall : MonoBehaviour
{
    [SerializeField]
    private PlanetsEnum portalPlanet;
    [SerializeField]
    private Portal portal;

    /// <summary>
    /// This method will get called through an event from the Portal class
    /// </summary>
    /// <param name="planet"></param>
    public void ChangePortalActive(PlanetsEnum activePortalPlanet)
    {
        if (portalPlanet == activePortalPlanet)
        {
            Destroy(gameObject);
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
