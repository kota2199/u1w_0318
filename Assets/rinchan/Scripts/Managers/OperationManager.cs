using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OperationManager : MonoBehaviour
{
    string[] operationMethodNames;
    string[] operationMethodName;

    string currentSceneName;

    // Start is called before the first frame update
    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        operationMethodNames = (string[])Enum.GetNames(typeof(Operation_Method));
        ChangeOperation(currentSceneName);
        Debug.Log(operationMethodName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeOperation(string sceneName)
    {
        switch (sceneName)
        {
            case "Title&Menu":
                operationMethodName[0] = operationMethodNames[0];
                break;
            case "rinchan0323_Stage_Tutorial":
                Array.Copy(operationMethodNames, 0, operationMethodName, 0, 2);
                break;
            case "Stage01":
                operationMethodName[0] = operationMethodNames[2];
                operationMethodName[1] = operationMethodNames[3];
                break;
            case "Stage02":
                operationMethodName[0] = operationMethodNames[4];
                operationMethodName[1] = operationMethodNames[5];
                operationMethodName[2] = operationMethodNames[6];
                break;
            case "Stage03":
                
                break;
            default:
                break;
        }
    }
}
