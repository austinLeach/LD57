using UnityEngine;
using UnityEngine.UI;

public class PanicButton : MonoBehaviour
{
    public Button myButton; // Assign this in the Inspector
    private bool goalAchieved = false;
    private bool isPanicing = false;

    void Start()
    {
        // Set up the listener for the button press
        myButton.onClick.AddListener(PANIC);
    }

    public void PANIC()
    {
        if (isPanicing)
        {
            goalAchieved = true;
            isPanicing = false;
            //Debug.Log("Panic resolved");
        }
    }

    void Update()
    {

    }
    
    public void SetPanicState()
    {
        Debug.Log("Panicing Waiting for button press...");
        goalAchieved =false;
        isPanicing = true;
    }

    public bool PanicWin()
    {
        return goalAchieved;
    }
}
