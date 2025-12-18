using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZoneTracker : MonoBehaviour
{
    private XRSocketInteractor socket;
    public string currentLetter = "";

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    void Update()
    {
        // Check if a block is placed in this snap zone
        if (socket.hasSelection)
        {
            // Remember the name of the block placed in it
            currentLetter = socket.selectTarget.name;
        }
        else
        {
            currentLetter = "";
        }
    }
}
