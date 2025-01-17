using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Canvas mainUI;

    public Image characterAvatar;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;

    public Queue<DialogueLine> lines;

    public float typingSpeed = 0.02f;

    public Animator animator;

    public bool isOpen = false;

    public delegate void DialogueEnded();
    public event DialogueEnded OnDialogueEnded;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.F))
            DisplayNextLine();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        mainUI.gameObject.SetActive(false);
        isOpen = true;
        animator.SetBool("isOpen", true);

        if (lines == null)
            lines = new Queue<DialogueLine>();
        else
            lines.Clear();

        foreach (DialogueLine line in dialogue.lines)
            lines.Enqueue(line);

        DisplayNextLine();
        Time.timeScale = 0f;
    }

    public void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            StartCoroutine(EndDialogue());
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
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    IEnumerator EndDialogue()
    {
        animator.SetBool("isOpen", false);
        yield return new WaitForSecondsRealtime(0.5f);
        mainUI.gameObject.SetActive(true);
        Time.timeScale = 1f;
        isOpen = false;
        OnDialogueEnded?.Invoke();
    }
}
