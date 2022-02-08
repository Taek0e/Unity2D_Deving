using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum TaskState
{
    Inactive,
    Running,
    Complete
}


[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    #region Events
    public delegate void StateChangedHandler(Task task, TaskState curentState, TaskState prevState);
    public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevSuccess);
    #endregion 


    [SerializeField]
    private Category category;

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


    private TaskState state;

    public int currentSuccess;

    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;

    public int CurrentSuccess
    {
        get => currentSuccess;
        set
        {
            int prevSuccess = currentSuccess;
            currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);

            if (currentSuccess != prevSuccess)
            {
                State = currentSuccess == needSuccessToComplete ? TaskState.Complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess);
            }
        }
    }

    public Category Category => category;
    public string CodeName => codeName;
    public string Description => description;
    public int NeedSuccessToComplete => needSuccessToComplete;

    public TaskState State
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            onStateChanged?.Invoke(this, state, prevState);  // onStateChanged가 null이 아니라면 Invoke() 실행
        }
    }

    public bool IsComplete => State == TaskState.Complete;

    public Quest Owner { get; private set; }

    public void Setup(Quest quest)
    {
        Owner = quest;
    }

    public void Start()
    {
        State = TaskState.Running; 
    }

    public void End()
    {
        onStateChanged = null;
        onSuccessChanged = null;
    }

    public void ReceieveReport(int successCount)
    {
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
        
    }

    public bool IsTarget(string category, object target)
        => Category == category ||
        targets.Any(x => x.IsEqual(target)) &&
        !IsComplete; 






}
