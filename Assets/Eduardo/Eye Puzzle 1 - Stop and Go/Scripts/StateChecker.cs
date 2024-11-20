using UnityEngine;

public class StateChecker : MonoBehaviour
{
    private Material mat;
    private void Awake() {
        mat = GetComponent<Renderer>().material;
        mat.color = Color.black;
    }
    public void SwapColour(Color colour) => mat.color = colour;
}
