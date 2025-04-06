using UnityEngine;
using TMPro;

public class DropdownPuzzle : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Use TMP_Dropdown for TextMeshPro

    private int winIndex;
    private int currentSelectedIndex;

    void Start()
    {
        // Store the current selected option
        currentSelectedIndex = dropdown.value;

        // Generate the random win condition, avoiding the current selected option
        GenerateRandomWinCondition();
    }

    void GenerateRandomWinCondition()
    {
        int optionCount = dropdown.options.Count;

        // Step 1: Pick random win index, but make sure it is not the current selected index
        do
        {
            winIndex = Random.Range(0, optionCount);
        } while (winIndex == currentSelectedIndex);

        Debug.Log($"Win option is: {dropdown.options[winIndex].text}");

        // Step 2: Hook up the listener for dropdown selection
        dropdown.onValueChanged.AddListener(CheckSelection);
    }

    void CheckSelection(int selectedIndex)
    {
        if (selectedIndex == winIndex)
        {
            Debug.Log("Correct choice! Puzzle solved!");
            // Add your win logic here!
            GenerateRandomWinCondition();
        }
    }
}
