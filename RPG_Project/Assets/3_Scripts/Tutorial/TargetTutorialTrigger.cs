using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTutorialTrigger : MonoBehaviour
{
    [SerializeField] TutorialTrigger trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger.isTrigger = true;
        }
    }
}
