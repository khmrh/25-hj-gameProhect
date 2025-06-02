using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI 요소 - instector에서 연결")]
    public GameObject DialoguePanel;
    public Image characterImage;
    public TextMeshProUGUI characternameText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    [Header("기본설정")]
    public Sprite defaultCharacterImage;

    [Header("타이핑 효과 설정")]   

    public float tyingSpeed = 0.05f;
    public bool skipTypingOnClick = true;

    //내부 변수들
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

        //대화 시작 준비
        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDalogueActive = true;

        //UI업데이트
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

        //텍스트를 한 글자씩 추가
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

    void ShowCurrentLine()              //현재 대화 줄의 내용을 타이핑 효과와 함께 화면에 표시하는 함수
    {
        if (currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)  //대화 데이터와 인데긋가 유효한지확인
        {
            if (typingCoroutine != null)                //이전 타이핑 효과가 있다면 중지
            {
                StopCoroutine (typingCoroutine);
            }

            //현재 줄의 대화 내용으로 타이핑 효과 시작
            string currenTxet = currentDialogue.dialogueLines [currentLineIndex];
            typingCoroutine = StartCoroutine(TypeText(currenTxet));
        }
    }

    public void ShowNextLine()      //다음 대화 줄로 이동 시키는 함수(타이핑이 완료된 후에만 호출
    {
        currentLineIndex++;         //다음 줄로 인덱스 증가

        //마지막 대화였는지 확인
        if (currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();          //대화가 남았으면 다음줄 표시
        }
    }

    void EndDialogue()                              //대화를 완전히 종료하는 함수
    {
        if (typingCoroutine != null)                //타이핑 효과 정리
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isDalogueActive = false;                    //대화 비활성화
        isTyping = false;                           //타이핑 상태 해제
        DialoguePanel.SetActive(false);             //대화창 숨기기
        currentLineIndex = 0;                       //인덱스 초기화
    }

    public void HandleNextInput()               //스페이스바나 버튼 클릭시 호출되는 입력 처리 함수
    {
        if (isTyping && skipTypingOnClick)
        {
            CompleteTyping();               //타이핑 중이면 즉시 완료
        }
        else if (!isTyping)
        {
            ShowNextLine();                 //타이핑 완료 상태면 다음 줄로
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
