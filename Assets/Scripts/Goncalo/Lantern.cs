using UnityEngine;

public class Lantern : MonoBehaviour, IUsable
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
    [SerializeField]
    private GameObject particles;

    private Collider energyCollider;
    public bool emitingEnergy = false;

    public bool OnPlayer {  get; set; } = false;

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

    private void Start()
    {
        energyCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            emitingEnergy = false;
            particles.SetActive(false);
        }
    }

    /// <summary>
    /// Changes the lantern's appearance and particles depending on the energy
    /// </summary>
    /// <param name="planet"></param>
    private void SetEnergy(PlanetsEnum planet)
    {
        Renderer renderer = GetComponent<Renderer>();
        ParticleSystem.MainModule main = particles.GetComponent<ParticleSystem>().main;
        switch (planet)
        {
            case PlanetsEnum.None:
                renderer.material = defaultMat;
                main.startColor = defaultMat.color;
                break;
            case PlanetsEnum.Mars:
                renderer.material = mars;
                main.startColor = mars.color;
                break;
            case PlanetsEnum.Jupiter:
                renderer.material = jupiter;
                main.startColor = jupiter.color;
                break;
            case PlanetsEnum.Saturn:
                renderer.material = saturn;
                main.startColor = saturn.color;
                break;
            case PlanetsEnum.Eclipse:
                renderer.material = eclipse;
                main.startColor = eclipse.color;
                break;
            case PlanetsEnum.Neptune:
                renderer.material = neptune;
                main.startColor = neptune.color;
                break;
            default:
                break;
        }
    }

    public void Use()
    {
        emitingEnergy = true;
        particles.SetActive(true);
    }
}
