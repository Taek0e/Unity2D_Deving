using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    #region Events
    public delegate void QuestRegisteredHandler(Quest newQuest);
    public delegate void QuestCompletedHandler(Quest quest);
    public delegate void QuestCanceledHandler(Quest quest);
    #endregion

    private static QuestSystem instance;
    private static bool isApplicationQuitting;

    public static QuestSystem Instance
    {
        get
        {
            if (!isApplicationQuitting && instance == null)
            {
                instance = FindObjectOfType<QuestSystem>();
                if (instance == null)
                {
                    instance = new GameObject("Quest System").AddComponent<QuestSystem>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }


    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();

    private List<Quest> activeAchievements = new List<Quest>();
    private List<Quest> completedAchievements = new List<Quest>();

    private QuestDatabase questDatabase;
    private QuestDatabase achievementDatabase;






    public event QuestRegisteredHandler onQuestReigstered;
    public event QuestCompletedHandler onQuestCompleted;
    public event QuestCanceledHandler onQuestCanceled;

    public event QuestRegisteredHandler onAchievementRegistered;
    public event QuestCompletedHandler onAchievementCompleted;
    public event QuestCanceledHandler onAchievementCanceled;

    public IReadOnlyList<Quest> ActiveQuests => activeQuests;
    public IReadOnlyList<Quest> CompletedQuests => completedQuests;
    public IReadOnlyList<Quest> ActiveAchievements => activeAchievements;
    public IReadOnlyList<Quest> CompletedAchievements => completedAchievements;








    private void Awake()
    {
        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        achievementDatabase = Resources.Load<QuestDatabase>("AchievementDatabase");
    }

    public Quest Register(Quest quest)
    {
        var newQuest = quest.Clone();

        if (newQuest is Achievement)
        {
            newQuest.onCompleted += OnAchievementCompleted;

            activeAchievements.Add(newQuest);

            newQuest.OnRegister();
            onAchievementRegistered?.Invoke(newQuest);
        }
        else
        {
            newQuest.onCompleted += OnQuestCompleted;
            newQuest.onCanceled += OnQuestCanceled;

            activeQuests.Add(newQuest);

            newQuest.OnRegister();
            onQuestReigstered?.Invoke(newQuest);
        }

        return newQuest;
    }

    #region Callback
    private void OnQuestCompleted(Quest quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        onQuestCompleted?.Invoke(quest);
    }

    private void OnQuestCanceled(Quest quest)
    {
        activeQuests.Remove(quest);
        onQuestCanceled?.Invoke(quest);

        Destroy(quest, Time.deltaTime);
    }

    private void OnAchievementCompleted(Quest achievement)
    {
        activeAchievements.Remove(achievement);
        activeAchievements.Add(achievement);

        onAchievementCompleted?.Invoke(achievement);
    }

    #endregion
}
