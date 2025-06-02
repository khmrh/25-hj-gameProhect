using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI ��� - instector���� ����")]
    public GameObject DialoguePanel;
    public Image characterImage;
    public TextMeshProUGUI characternameText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    [Header("�⺻����")]
    public Sprite defaultCharacterImage;

    [Header("Ÿ���� ȿ�� ����")]   

    public float tyingSpeed = 0.05f;
    public bool skipTypingOnClick = true;

    //���� ������
    private DialogueDataSO currentDialogue;
    private int currentLineIndex;
    private bool isDalogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        DialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(HandleNextInput);
    }

    void Update()
    {
        if (isDalogueActive && Input.GetKeyUp(KeyCode.Space))
        {
            HandleNextInput();
        }
    }

    public void startDialogue(DialogueDataSO dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;

        //��ȭ ���� �غ�
        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDalogueActive = true;

        //UI������Ʈ
        DialoguePanel.SetActive(true);
        characternameText.text = dialogue.characterName;

        if (dialogue.characterImage != null)
        {
            if (characterImage != null)
            {
                characterImage.sprite = dialogue.characterImage;
            }
            else
            {
                characterImage.sprite = defaultCharacterImage;
            }
        }

        ShowCurrentLine();
    }

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        dialogueText.text = "";

        //�ؽ�Ʈ�� �� ���ھ� �߰�
        for (int i = 0; i < textToType.Length; i++)
        {
            dialogueText.text += textToType[i];
            yield return new WaitForSeconds(tyingSpeed);
        }

        isTyping = false ;


    }

    private void CompleteTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        isTyping =false;   

        if (currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
        }
    }

    void ShowCurrentLine()              //���� ��ȭ ���� ������ Ÿ���� ȿ���� �Բ� ȭ�鿡 ǥ���ϴ� �Լ�
    {
        if (currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)  //��ȭ �����Ϳ� �ε��߰� ��ȿ����Ȯ��
        {
            if (typingCoroutine != null)                //���� Ÿ���� ȿ���� �ִٸ� ����
            {
                StopCoroutine (typingCoroutine);
            }

            //���� ���� ��ȭ �������� Ÿ���� ȿ�� ����
            string currenTxet = currentDialogue.dialogueLines [currentLineIndex];
            typingCoroutine = StartCoroutine(TypeText(currenTxet));
        }
    }

    public void ShowNextLine()      //���� ��ȭ �ٷ� �̵� ��Ű�� �Լ�(Ÿ������ �Ϸ�� �Ŀ��� ȣ��
    {
        currentLineIndex++;         //���� �ٷ� �ε��� ����

        //������ ��ȭ������ Ȯ��
        if (currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();          //��ȭ�� �������� ������ ǥ��
        }
    }

    void EndDialogue()                              //��ȭ�� ������ �����ϴ� �Լ�
    {
        if (typingCoroutine != null)                //Ÿ���� ȿ�� ����
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isDalogueActive = false;                    //��ȭ ��Ȱ��ȭ
        isTyping = false;                           //Ÿ���� ���� ����
        DialoguePanel.SetActive(false);             //��ȭâ �����
        currentLineIndex = 0;                       //�ε��� �ʱ�ȭ
    }

    public void HandleNextInput()               //�����̽��ٳ� ��ư Ŭ���� ȣ��Ǵ� �Է� ó�� �Լ�
    {
        if (isTyping && skipTypingOnClick)
        {
            CompleteTyping();               //Ÿ���� ���̸� ��� �Ϸ�
        }
        else if (!isTyping)
        {
            ShowNextLine();                 //Ÿ���� �Ϸ� ���¸� ���� �ٷ�
        }
    }

    public void SkipDialogue()
    {
        EndDialogue ();
    }

    public bool isdialogueActive()
    {
        return isDalogueActive;
    }
}
