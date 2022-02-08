using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]

public class Task : ScriptableObject
{
    [Header("Text")]
    [SerializeField]
    private string codeName; 
    [SerializeField]
    private string description;

    [Header("Action")]
    [SerializeField]
    private TaskAction action;

    [Header("Target")]
    [SerializeField]
    private TaskTarget[] targets;

    [Header("Setting")]
    [SerializeField]
    private InitialSuccessValue initialSuccessValue;
    [SerializeField]
    private int needSuccessToComplete;

    public int CurrentSuccess { get; private set; }
    public string CodeName => codeName;
    public string Description => description;
    public int NeedSuccessToComplete => needSuccessToComplete;



    public void ReceieveReport(int successCount)
    {
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
        
    }

    public bool IsTarget(object target) => targets.Any(x => x.IsEqual(target));

        
     


}
