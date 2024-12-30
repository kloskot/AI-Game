using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_InputField playerInputField; // Reference to the input field
    public TextMeshProUGUI dialogBox;       // Reference to the dialog box text

    void Update()
    {
        // Check if Enter is pressed and input field is not empty
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(playerInputField.text))
        {
            SubmitPlayerText();
        }
    }

    void SubmitPlayerText()
    {
        // Append the player's input to the dialog box
        dialogBox.text += "\nPlayer: " + playerInputField.text;

        // Clear the input field
        playerInputField.text = "";

        // Optionally, refocus the input field for continuous typing
        playerInputField.ActivateInputField();
    }
}
