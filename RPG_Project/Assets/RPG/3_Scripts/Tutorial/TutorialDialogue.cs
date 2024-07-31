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
        dialogueSystem.Setup();                           // UI����
    }

    public override void Execute(TutorialController controller)
    {
        bool isCompleted = dialogueSystem.UpdateDialog(); // false, true��ȯ dialogue Data�� �ִ� Index�� �Ǹ� true�� ��ȯ

        if (isCompleted)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
       
    }
}
