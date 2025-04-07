using UnityEngine;
using TMPro;

public class DropdownPuzzle : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Use TMP_Dropdown for TextMeshPro

    private int winIndex;
    private int currentSelectedIndex;
    private bool hasWon = false;

    void Start()
    {
        // Store the current selected option
        currentSelectedIndex = dropdown.value;
    }

    public string GenerateRandomWinCondition()
    {
        hasWon = false;
        int optionCount = dropdown.options.Count;

        currentSelectedIndex = dropdown.value;

        // Step 1: Pick random win index, but make sure it is not the current selected index
        do
        {
            winIndex = Random.Range(0, optionCount);
        } while (winIndex == currentSelectedIndex);

        string winOption = $"{dropdown.options[winIndex].text}";

        // Step 2: Hook up the listener for dropdown selection
        dropdown.onValueChanged.RemoveListener(CheckSelection);
        dropdown.onValueChanged.AddListener(CheckSelection);
        return winOption;
    }

    public void CheckSelection(int selectedIndex)
    {
        if (selectedIndex == winIndex)
        {
            Debug.Log("Correct choice! Puzzle solved!");
            hasWon = true;
        }
    }
    public bool CheckWin()
    {
        if (hasWon)
        {
            return true;
        }
        return false;
    }
}
