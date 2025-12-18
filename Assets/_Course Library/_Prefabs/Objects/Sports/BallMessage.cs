using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class BallMessageUI : MonoBehaviour
{
    public XRGrabInteractable grabObject;
    public TextMeshProUGUI messageText;

    private void OnEnable()
    {
        grabObject.selectEntered.AddListener(OnGrab);
        grabObject.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabObject.selectEntered.RemoveListener(OnGrab);
        grabObject.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        messageText.text = "Tennis Ball Picked Up";
    }

    void OnRelease(SelectExitEventArgs args)
    {
        messageText.text = "Tennis Ball Released";
    }
}
