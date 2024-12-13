using System;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CreatureBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField]
    private LayerMask defaultMask;
    [SerializeField]
    private LayerMask InteractableMask;
    [SerializeField]
    private int creatureNumber;
    [SerializeField]
    private PlanetsEnum planetEnergy;
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private GameObject creatureDeathSoundObject;
    [SerializeField]
    private GameObject deathParticles;

    private int defaultMaskBit;
    private int interactableMaskBit;

    private void Start()
    {
        defaultMaskBit = (int)Mathf.Log(defaultMask.value, 2);
        interactableMaskBit = (int)Mathf.Log(InteractableMask.value, 2);
    }

    public void Interact()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Instantiate(creatureDeathSoundObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        //Checks for the lantern
        if (other.GetComponent<Lantern>() != null)
        {
            Lantern lantern = other.GetComponent<Lantern>();
            //If the lantern has Neptune's energy, the eye dies
            if (lantern.Planet == planetEnergy && lantern.emitingEnergy && lantern.Planet == planetEnergy)
            {
                model.SetActive(true);
                gameObject.layer = interactableMaskBit;
            }
            else
            {
                model.SetActive(false);
                gameObject.layer = defaultMaskBit;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Lantern>() != null)
        {
            model.SetActive(false);
            gameObject.layer = defaultMaskBit;
        }
    }
}
