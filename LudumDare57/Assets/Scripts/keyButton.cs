using UnityEngine;
using UnityEngine.UI;

public class KeyToggle : MonoBehaviour
{
    public Toggle myToggle;
    private bool goalAchieved = false;

    void Start()
    {
        // Ensure it's off at the beginning
        myToggle.isOn = false;

        // Listen for toggle changes
        myToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn && !goalAchieved)
        {
            goalAchieved = true;
            Debug.Log("✅ Goal achieved: Toggle is ON!");
            // You can trigger more game logic here
        }
    }
}
