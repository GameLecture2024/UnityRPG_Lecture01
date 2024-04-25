using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    public DialogueSystem dialogueSystem01;
    public DialogueSystem dialogueSystem02;

    
    IEnumerator Start()
    {
        yield return new WaitUntil(()=> dialogueSystem01.UpdateDialog());

        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(()=> dialogueSystem02.UpdateDialog());
    }
}
