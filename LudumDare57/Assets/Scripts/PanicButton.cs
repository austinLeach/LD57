using UnityEngine;
using UnityEngine.UI;

public class PanicButton : MonoBehaviour
{
    public Button myButton; // Assign this in the Inspector
    private bool goalAchieved = false;

    void Start()
    {
        // Set up the listener for the button press
        myButton.onClick.AddListener(PANIC);

        // Create the goal (initial state)
        goalAchieved = false;
        Debug.Log("Panicing Waiting for button press...");
    }

    void PANIC()
    {
        // Update the goal status
        goalAchieved = true;
        Debug.Log("Panic resolved");

        // You can add any additional logic here
    }

    void Update()
    {
        // You could do checks here if needed
        if (goalAchieved)
        {
           
        }
    }
}
