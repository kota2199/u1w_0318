using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private int tutorialCount = -1;

    [SerializeField]
    private GameObject[] tutorialObjs;

    public void CountTutorial()
    {
        tutorialCount++;

        for(int i = 0; i < tutorialObjs.Length; i++)
        {
            if(i == tutorialCount)
            {
                tutorialObjs[i].SetActive(true);
                StartCoroutine(autoDelete(tutorialObjs[i]));
            }
            else
            {
                tutorialObjs[i].SetActive(false);
            }
        }
    }

    private IEnumerator autoDelete(GameObject obj)
    {
        Debug.Log("false");
        yield return new WaitForSeconds(5);
        obj.SetActive(false);
    }
}
