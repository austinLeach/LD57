using UnityEngine;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;

public class DialRandomize : MonoBehaviour
{
    public UI_Knob knobControl; // Your knob script with knobValue

    public float goalValue;
    public bool goalReached = false;

    // These are the only valid knob positions
    private readonly List<float> knobSteps = new List<float>
    {
        0f,
        0.1666667f,
        0.3333333f,
        0.5f,
        0.6666667f,
        0.8333334f,
        1f
    };

    private void Start()
    {

    }

    private void Update()
    {

    }

    public string SetRandomGoal()
    {
        goalReached = false;
        float current = knobControl.knobValue;

        // Tolerance for float comparison
        float epsilon = 0.01f;

        // Create a list of valid options excluding the current (and 1 if current is 0, or 0 if current is 1)
        List<float> validOptions = new List<float>(knobSteps);
        validOptions.RemoveAll(val => Mathf.Abs(val - current) < epsilon || (IsZeroOnePair(val, current, epsilon)));

        // Pick a random goal from the filtered list
        goalValue = validOptions[Random.Range(0, validOptions.Count)];
        int goalIndex = knobSteps.FindIndex(step => Mathf.Abs(step - goalValue) < epsilon);
        int displayNumber = (goalIndex == 0 || goalIndex == 6) ? 1 : goalIndex + 1;

        return ($"{displayNumber}");
    }

    public bool CheckGoalReached()
    {
        float epsilon = 0.01f;

        float currentValue = knobControl.knobValue;
        if (Mathf.Abs(currentValue - 1f) < epsilon)
            currentValue = 0f;

        float goal = goalValue;
        if (Mathf.Abs(goal - 1f) < epsilon)
            goal = 0f;

        if (Mathf.Abs(goal - currentValue) < epsilon)
        {
            goalReached = true;
            Debug.Log("Dial solved");
            return true;
        }

        return false;
    }

    // Helper to check if one value is 0 and the other is 1 (or vice versa)
    private bool IsZeroOnePair(float a, float b, float epsilon)
    {
        return (Mathf.Abs(a) < epsilon && Mathf.Abs(b - 1f) < epsilon)
            || (Mathf.Abs(b) < epsilon && Mathf.Abs(a - 1f) < epsilon);
    }
}
