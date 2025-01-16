using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;


[System.Serializable]
public class  DialogueCharacter
{
    public string name;
    public Sprite avatar;
}
[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}
[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> lines = new List<DialogueLine>();
}
public class DialogueTrigger : MonoBehaviour
{
    public int tutorLabel = 0;
    public Dialogue dialogue;
    private bool dialogued = false;
    private bool isNear = false;

    public TMP_Text interactGuide;
    public string interactMessage;

    private void Start()
    {
        if (interactGuide != null)
            interactGuide.enabled = false;
    }
    private void Update()
    {
        if (isNear && !DialogueManager.Instance.isOpen && dialogued && Input.GetKeyDown(KeyCode.F))
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = true;
            if (!dialogued)
            {
                dialogued = true;
                TriggerDialogue();
                switch(tutorLabel)
                {
                    case 1:
                    Counter.counterLearned = true;
                    break;
                    case 2:
                    CastBloodWave.bloodWaveLearned = true;
                    break;
                    default:
                    break;
                }
            }
            else
            {
                if (interactGuide != null)
                {
                    interactGuide.enabled = true;
                    interactGuide.text = interactMessage;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = false;
            if (interactGuide != null)
                interactGuide.enabled = false;
        }
    }

}
