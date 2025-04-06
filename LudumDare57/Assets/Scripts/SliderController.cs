using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultiScrollbarController : MonoBehaviour
{
    [System.Serializable]
    public class ScrollbarGroup
    {
        public Scrollbar scrollbar;
        public TMP_Text numberText;
    }

    public ScrollbarGroup[] scrollbars;

    private int steps = 5;
    private int[] targetValues = new int[5]; // Random target values the user must match

    void Start()
    {
        GenerateTargetSequence();

        for (int i = 0; i < scrollbars.Length; i++)
        {
            SetupScrollbar(scrollbars[i], i);
        }
    }

    void GenerateTargetSequence()
    {
        for (int i = 0; i < targetValues.Length; i++)
        {
            targetValues[i] = Random.Range(1, steps + 1); // 1–5
        }
        Debug.Log($"Slider solution: {targetValues[0]}{targetValues[1]}{targetValues[2]}{targetValues[3]}{targetValues[4]}");
    }

    void SetupScrollbar(ScrollbarGroup group, int index)
    {
        // Set random starting value (does not have to match target)
        int randomStart = Random.Range(0, steps);
        float stepSize = 1f / (steps - 1);
        group.scrollbar.value = randomStart * stepSize;

        SnapAndDisplay(group);

        group.scrollbar.onValueChanged.AddListener((float value) =>
        {
            SnapAndDisplay(group);
            CheckWinCondition();
        });
    }

    void SnapAndDisplay(ScrollbarGroup group)
    {
        float stepSize = 1f / (steps - 1);
        int stepIndex = Mathf.RoundToInt(group.scrollbar.value / stepSize);
        group.scrollbar.value = stepIndex * stepSize;
        group.numberText.text = (stepIndex + 1).ToString();
    }

    void CheckWinCondition()
    {
        for (int i = 0; i < scrollbars.Length; i++)
        {
            float stepSize = 1f / (steps - 1);
            int currentValue = Mathf.RoundToInt(scrollbars[i].scrollbar.value / stepSize) + 1;

            if (currentValue != targetValues[i])
                return; // Not matching yet
        }

        Debug.Log("Puzzle solved!");
    }
}
