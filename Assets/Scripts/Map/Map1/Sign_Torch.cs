using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign_Torch : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject tilemap;
    public GameObject dangeroustilemap;
=======
    public GameObject tilemap;  
>>>>>>> BaoDi
    public GameObject[] torches;
    public Image dialogImage;
    public TMP_Text dialogText;
    public string idleMessage;
    private bool isPlayerNear = false; 

    void Start()
    {
        dialogText.enabled = false;
        dialogImage.enabled = false;
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
            dialogText.enabled = true;
            dialogImage.enabled = true;
            dialogText.text = idleMessage;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {
            isPlayerNear = false;
            dialogText.enabled = false;
            dialogImage.enabled = false;
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
            torchScript.dialogText.enabled = false;
            torchScript.dialogImage.enabled = false;
        }
/*
        foreach (var torch in torches)
        {
            torch.GetComponent<Torch_OnOff>().toggleText.SetActive(false);
        }*/
        dialogText.enabled = false;
        dialogImage.enabled = false; 
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
<<<<<<< HEAD
            dangeroustilemap.SetActive(true);
=======
>>>>>>> BaoDi
        }
        else
        {
            tilemap.SetActive(false);
<<<<<<< HEAD
            dangeroustilemap.SetActive(false);
=======
>>>>>>> BaoDi
        }
    }
}

