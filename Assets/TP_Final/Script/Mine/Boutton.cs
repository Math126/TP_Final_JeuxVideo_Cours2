using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PressableButton : MonoBehaviour
{
    public XRBaseInteractable interactable;
    

    public Material originMat;
    public Material vert;
    Renderer renderer;
    

    public void Start()
    {
        renderer = GetComponent<Renderer>();
        originMat = renderer.material;
    }

    // Called when the script is enabled
    public void OnEnable()
    {
        // Add the listener for the select enter event
        interactable.onSelectEntered.AddListener(OnSelectEntered);
        this.gameObject.transform.position -= new Vector3(0.0f, 0.05f, 0.0f);
        renderer.material = vert;
    }

    // Called when the script is disabled
    public void OnDisable()
    {
        // Remove the listener for the select enter event
        interactable.onSelectEntered.RemoveListener(OnSelectEntered);
        this.gameObject.transform.position += new Vector3(0.0f, 0.05f, 0.0f);
        renderer.material = originMat;
    }

    // Called when the button is pressed
    private void OnSelectEntered(XRBaseInteractor interactor)
    {
        // Your button press logic here
        Debug.Log("Button Pressed!");
    }
}
