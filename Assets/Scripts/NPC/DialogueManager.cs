using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterAvatar;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;

    private Queue<DialogueLine> lines;

    public float typingSpeed = 0.02f;

    public Animator animator;

    private bool isOpen = false;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.F))
            DisplayNextLine();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0f;
        isOpen = true;
        animator.SetBool("isOpen", true);

        if (lines == null)
            lines = new Queue<DialogueLine>();
        else
            lines.Clear();

        foreach (DialogueLine line in dialogue.lines)
            lines.Enqueue(line);

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterAvatar.sprite = currentLine.character.avatar;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();
        StartCoroutine(TypeLine(currentLine));
    }
    
    IEnumerator TypeLine(DialogueLine line)
    {
        dialogueText.text = "";
        foreach (char letter in line.line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isOpen = false;
        animator.SetBool("isOpen", false);
        Time.timeScale = 1f;
    }
}
