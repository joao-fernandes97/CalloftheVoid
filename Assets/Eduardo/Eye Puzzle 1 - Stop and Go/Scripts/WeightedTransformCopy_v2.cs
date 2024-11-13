using UnityEngine;

public class WeightedTransformCopy_v2 : MonoBehaviour
{
    [SerializeField] private Transform topLid;
    Vector3 topEuler;
    [SerializeField] private Transform bottomLid;
    Vector3 bottomEuler;

    Vector3 thisEuler;
    Vector3 deltaEuler;

    [SerializeField] private float factor = 0.3f;
    void Start()
    {
        topEuler = topLid.transform.eulerAngles;
        bottomEuler = bottomLid.transform.eulerAngles;
        thisEuler = this.transform.eulerAngles;
       
    }
    private void GetDelta()
    {
        deltaEuler = this.transform.eulerAngles - thisEuler;
    }
    // Update is called once per frame
    void Update()
    {
        GetDelta();
        //The main thing to do is to change the method from ovewritting to blending
        //get the CURRENT >>> after animation has been applied >>> add this transformation on top
        // if possible we can do a lerp between the CURRENT and the CURRENT + THIS:
        //Focus on making these transforms additive to the CURRENT euler values
        //AN EASYER SOLUTION IS TO JUST ADD AN EMPTY ON TOP TO TAKE CARE OF THE SCRIPTED TRANSFORMS AND THE THE ANIMATIONS HAPPEN ON THE BONES UNDERNEATH
        //WHEN IN DOUBT, SEPARATE THE BEHAVIOURS
        //WRITE DOWN  YOUR FINDINGS LATER
        topLid.transform.eulerAngles = CorrectlyLerpedEuler(topEuler,topEuler + deltaEuler,factor);

        bottomLid.transform.eulerAngles = CorrectlyLerpedEuler(bottomEuler,bottomEuler + deltaEuler,factor);

        topEuler = topLid.transform.eulerAngles;
        bottomEuler = bottomLid.transform.eulerAngles;
        thisEuler = this.transform.eulerAngles;


    }
    
    private Vector3 CorrectlyLerpedEuler(Vector3 min, Vector3 max, float factor)
    {
        float x, y, z;
        x = Mathf.LerpAngle(min.x, max.x, factor);
        y = Mathf.LerpAngle(min.y, max.y, factor);
        z = Mathf.LerpAngle(min.z, max.z, factor);
        return new Vector3(x, y, z);
    }
}
