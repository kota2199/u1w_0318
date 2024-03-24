using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCaller : MonoBehaviour
{
    [SerializeField]
    private TutorialController tutorialController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tutorialController.CountTutorial();
        Destroy(this.gameObject);
    }
}
