using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueDataSO myDialogue;

    public DialogueManager DialogueManager;

    // Start is called before the first frame update
   
    

    void OnMouseDown()
    {
        Debug.Log("Ŭ�� ����");

        if (DialogueManager == null) return;
        if (DialogueManager.isdialogueActive()) return;
        if (myDialogue == null) return; 
        
        DialogueManager.startDialogue(myDialogue);

        Debug.Log("Ŭ�� �ν�");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
