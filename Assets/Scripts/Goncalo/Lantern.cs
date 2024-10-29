using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField]
    private Material defaultMat;
    [SerializeField]
    private Material mars;
    [SerializeField]
    private Material jupiter;
    [SerializeField]
    private Material saturn;
    [SerializeField]
    private Material eclipse;
    [SerializeField]
    private Material neptune;

    private PlanetsEnum planet;
    public PlanetsEnum Planet
    {   get
        {
            return planet;
        }
        set
        {
            //Changes the lantern's energy
            planet = value;
            SetEnergy(planet);
        }
    }

    /// <summary>
    /// Changes the lantern's appearance depending on the energy
    /// </summary>
    /// <param name="planet"></param>
    private void SetEnergy(PlanetsEnum planet)
    {
        Renderer renderer = GetComponent<Renderer>();
        switch (planet)
        {
            case PlanetsEnum.None:
                renderer.material = defaultMat;
                break;
            case PlanetsEnum.Mars:
                renderer.material = mars;
                break;
            case PlanetsEnum.Jupiter:
                renderer.material = jupiter;
                break;
            case PlanetsEnum.Saturn:
                renderer.material = saturn;
                break;
            case PlanetsEnum.Eclipse:
                renderer.material = eclipse;
                break;
            case PlanetsEnum.Neptune:
                renderer.material = neptune;
                break;
            default:
                break;
        }
    }
}
