using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Canvas mainUI;
    public GameObject chooseMenu;
    private Animator chooseAnimator;
    public Image characterAvatar;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;

    public Queue<DialogueLine> lines;

    public float typingSpeed = 0.02f;

    public Animator animator;

    public bool isOpen = false;

    public bool isNPCPaster = false;
    private bool isChoosing = false;
    public int chooseIndex = 0;


    private void Start()
    {
        if (Instance == null)
            Instance = this;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        chooseAnimator = chooseMenu.GetComponent<Animator>();
        chooseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        if (isOpen && !isChoosing && Input.GetKeyDown(KeyCode.F))
            DisplayNextLine();
    }

    public void StartDialogue(Dialogue dialogue, bool NPCPaster)
    {
        isOpen = true;
        isNPCPaster = NPCPaster;
        mainUI.gameObject.SetActive(false);
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

        if (!isChoosing)
        {
            DialogueLine currentLine = lines.Dequeue();

            characterAvatar.sprite = currentLine.character.avatar;
            characterName.text = currentLine.character.name;

            StopAllCoroutines();
            StartCoroutine(TypeLine(currentLine));
        }

        if (isNPCPaster && lines.Count == 1)
        {
            StartCoroutine(OpenChooseMenu());
        }
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
    }

    IEnumerator OpenChooseMenu()
    {
        isChoosing = true;
        chooseAnimator.SetBool("isOpen", true);
        yield return new WaitForSecondsRealtime(1f);
    }

    public void CloseChooseMenu(int nextLv)
    {
        chooseIndex = nextLv;
        chooseAnimator.SetBool("isOpen", false);
        isChoosing = false;
        DisplayNextLine();
    }
}
