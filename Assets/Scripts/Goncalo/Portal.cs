using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private PlanetsEnum planet;
    [SerializeField]
    private GameObject eclipseWall;
    [SerializeField]
    private GameObject marsWall;

    public event Action<PlanetsEnum> PlanetChanged;

    /// <summary>
    /// A method to iterate through the planes of each planet, activating only the intended portal
    /// Also sets the pedestal and the walls to the right planet using an EVENT
    /// Disables all other portals
    /// </summary>
    /// <param name="planet"></param>
    public void ChangePlanet(PlanetsEnum planet)
    {
        this.planet = planet;
        OnPlanetsChanged(planet);

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.GetComponent<Planet>().PlanetName == planet)
            {
                child.SetActive(true);
            }
            else
            {
                child.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Check if there are listeners before originating the event
    /// </summary>
    /// <param name="planet"></param>
    protected virtual void OnPlanetsChanged(PlanetsEnum planet)
    {
        if (PlanetChanged != null)
        {
            PlanetChanged(planet);
        }
    }
}
