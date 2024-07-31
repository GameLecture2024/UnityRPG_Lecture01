using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueSystem))]
public class TutorialDialogue : TutorialBase
{
    private DialogueSystem dialogueSystem;

    public override void Enter()
    {
        dialogueSystem = GetComponent<DialogueSystem>();
        dialogueSystem.Setup();                           // UI세팅
    }

    public override void Execute(TutorialController controller)
    {
        bool isCompleted = dialogueSystem.UpdateDialog(); // false, true반환 dialogue Data가 최대 Index가 되면 true를 반환

        if (isCompleted)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
       
    }
}
