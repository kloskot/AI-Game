using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public Transform raycastOrigin;
    public LayerMask npcLayer;
    public GameObject dialogUI;

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
            }
        }
        else
        {
            isLookingAtNPC = false;
            currentNPC = null;
        }
    }

    void ToggleDialog()
    {
        // Toggle dialog UI visibility
        if (dialogUI != null)
        {
            dialogUI.SetActive(!dialogUI.activeSelf);
        }
    }
}
