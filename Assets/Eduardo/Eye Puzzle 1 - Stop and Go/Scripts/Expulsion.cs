using System.Collections;
using UnityEngine;

public class Expulsion : MonoBehaviour
{
    [SerializeField] private float _teleportTime = 2f;
    [SerializeField] private Transform _destination;
    private YieldInstruction wait;
    private Coroutine _expelRot = null;
    private void Awake() {
        wait = new WaitForSeconds(_teleportTime);
    }
    public void Expel(GameObject target)
    {
        if (_expelRot != null) StopCoroutine(_expelRot);
        _expelRot = StartCoroutine(ExpulsionROT(target,_destination));
    }
    public void Expel(GameObject target,Transform destination)
    {
        if (_expelRot != null) StopCoroutine(_expelRot);
        _expelRot = StartCoroutine(ExpulsionROT(target,destination));
    }
    IEnumerator ExpulsionROT (GameObject target,Transform destination)
    {
        
        //Add visual effect over camera to show teleportation. Perhaps an overbloom fade to white
        yield return wait;
        CharacterController cc = target.GetComponent<CharacterController>();
        cc.enabled = false;
        target.transform.position = destination.position;
        target.transform.rotation = destination.rotation;
        cc.enabled = true;

    }
}
