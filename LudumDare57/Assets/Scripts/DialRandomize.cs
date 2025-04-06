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
        SetRandomGoal();
    }

    private void Update()
    {
        CheckGoalReached();
    }

    public void SetRandomGoal()
    {
        float current = knobControl.knobValue;

        // Tolerance for float comparison
        float epsilon = 0.001f;

        // Create a list of valid options excluding the current (and 1 if current is 0, or 0 if current is 1)
        List<float> validOptions = new List<float>(knobSteps);
        validOptions.RemoveAll(val => Mathf.Abs(val - current) < epsilon || (IsZeroOnePair(val, current, epsilon)));

        // Pick a random goal from the filtered list
        goalValue = validOptions[Random.Range(0, validOptions.Count)];

        Debug.Log($"Knob is at {current}, new goal is {goalValue}");
    }

    public void CheckGoalReached()
    {
        if (knobControl.knobValue == 1)
            knobControl.knobValue = 0;
        if (goalValue == knobControl.knobValue)
        {
            goalReached = true;
            Debug.Log("Dial solved");
        }
    }

    // Helper to check if one value is 0 and the other is 1 (or vice versa)
    private bool IsZeroOnePair(float a, float b, float epsilon)
    {
        return (Mathf.Abs(a) < epsilon && Mathf.Abs(b - 1f) < epsilon)
            || (Mathf.Abs(b) < epsilon && Mathf.Abs(a - 1f) < epsilon);
    }
}
