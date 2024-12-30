using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public Transform raycastOrigin;
    public LayerMask npcLayer;
    public GameObject dialogUI;
    public TextMeshProUGUI interactPrompt;

    private bool isLookingAtNPC = false;
    private GameObject currentNPC;

    void Update()
    {
        CheckForNPC();

        if (isLookingAtNPC && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDialog();
        }
    }

    void CheckForNPC()
    {
        // Raycast from the raycast origin
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, interactionRange, npcLayer))
        {
            if (hit.collider != null)
            {
                isLookingAtNPC = true;
                currentNPC = hit.collider.gameObject;

                // Optional: Display an "Interact" prompt here
                if (interactPrompt != null && !dialogUI.activeSelf)
                {
                    interactPrompt.text = "Press E to Interact";
                    interactPrompt.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            isLookingAtNPC = false;
            currentNPC = null;

            if (interactPrompt != null)
            {
                interactPrompt.gameObject.SetActive(false);
            }
        }
    }

    void ToggleDialog()
    {
        // Toggle dialog UI visibility
        if (dialogUI != null)
        {
            bool isDialogActive = !dialogUI.activeSelf;
            dialogUI.SetActive(isDialogActive);

            // Pause the game when dialog is active
            if (isDialogActive)
            {
                Time.timeScale = 0f; // Pause the game

                // Show the cursor
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                if (interactPrompt != null)
                {
                    interactPrompt.gameObject.SetActive(false); // Hide the prompt
                }
            }
            else
            {
                Time.timeScale = 1f; // Resume the game

                // Hide the cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
