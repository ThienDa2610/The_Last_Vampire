using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign_Torch : MonoBehaviour
{
    public GameObject tilemap;  
    public GameObject[] torches;  
    public GameObject toggleText; 
    private bool isPlayerNear = false; 

    void Start()
    {
        toggleText.SetActive(false); 
    }

    private void Update()
    {
        CheckTorchesStatus();
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F)) 
        {
            ToggleAllTorches();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {
            isPlayerNear = true;  
            toggleText.SetActive(true);  
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {
            isPlayerNear = false; 
            toggleText.SetActive(false);  
        }
    }

    void ToggleAllTorches()
    {
        foreach (var torch in torches)
        {
            Torch_OnOff torchScript = torch.GetComponent<Torch_OnOff>();
            if (!torchScript.IsOn())  
            {
                torchScript.ToggleTorch();
            }
        }

        foreach (var torch in torches)
        {
            torch.GetComponent<Torch_OnOff>().toggleText.SetActive(false);
        }
        toggleText.SetActive(false);
    }

    void CheckTorchesStatus()
    {
        bool allOff = true;

        foreach (int torchID in new int[] { 3, 5, 6, 8 })
        {
            Torch_OnOff torch = torches[torchID - 1].GetComponent<Torch_OnOff>(); 
            if (torch.IsOn())
            {
                allOff = false;
                break;  
            }
        }

        bool allOn = true;
        foreach (int torchID in new int[] { 1, 2, 4, 7 })
        {
            Torch_OnOff torch = torches[torchID - 1].GetComponent<Torch_OnOff>();
            if (!torch.IsOn())
            {
                allOn = false;
                break;  
            }
        }

      
        if (allOff && allOn)
        {
            tilemap.SetActive(true);
        }
        else
        {
            tilemap.SetActive(false);
        }
    }
}

