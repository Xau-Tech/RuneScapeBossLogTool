using UnityEngine;
using System.Collections.Generic;

public static class CacheManager
{
    public static BossInfo currentBoss
    {
        get
        {
            if (ProgramState.CurrentState == ProgramState.states.Drops)
                return DropsTab.currentBoss;
            else if (ProgramState.CurrentState == ProgramState.states.Logs)
                return LogsTab.currentBoss;
            else
                return DataController.Instance.bossInfoDictionary.FirstBossAlphabetically();
        }
        set
        {
            if (ProgramState.CurrentState == ProgramState.states.Drops)
            {
                DropsTab.currentBoss = value;
                Debug.Log($"Drops boss set to {value.bossName}");
            }
            else if (ProgramState.CurrentState == ProgramState.states.Logs)
            {
                LogsTab.currentBoss = value;
                Debug.Log($"Logs boss set to {value.bossName}");
            }
            else
            {
                DropsTab.currentBoss = value;
                LogsTab.currentBoss = value;
                Debug.Log($"All bosses set to {value.bossName}");
            }
        }
    }
    public static string currentLog
    {
        get
        {
            if (ProgramState.CurrentState == ProgramState.states.Drops)
                return DropsTab.currentLog;
            else if (ProgramState.CurrentState == ProgramState.states.Logs)
                return LogsTab.currentLog;
            else
                return null;
        }
        set
        {
            if(ProgramState.CurrentState == ProgramState.states.Drops)
            {
                DropsTab.currentLog = value;
                Debug.Log($"Drops log set to {value}");
            }
            else if(ProgramState.CurrentState == ProgramState.states.Logs)
            {
                LogsTab.currentLog = value;
                Debug.Log($"Logs logs set to {value}");
            }
            else
            {
                Debug.Log($"Log not set in ProgramState {ProgramState.CurrentState}");
            }
        }
    }

    public struct DropsTab
    {
        public enum Elements { ItemDropdown, LogDropdown };
        public static string currentLog { set; get; }
        public static BossInfo currentBoss { set; get; }

        private static bool itemsLoaded;
        private static bool logsLoaded;

        //  Determine if UI elements have been properly loaded/filled with data
        public static bool IsUILoaded(in Elements element, in bool flag)
        {
            if (element == Elements.ItemDropdown)
                itemsLoaded = flag;
            else if (element == Elements.LogDropdown)
                logsLoaded = flag;

            if (itemsLoaded && logsLoaded)
            {
                itemsLoaded = false;
                logsLoaded = false;
                return true;
            }
            else
                return false;
        }
    }
    public struct LogsTab
    {
        public enum Elements { LogDropdown };
        public static string currentLog { get; set; }
        public static BossInfo currentBoss { set; get; }

        private static string _currentLog;
        private static bool logsLoaded;

        public static bool IsUILoaded(in Elements element, in bool flag)
        {
            if (element == Elements.LogDropdown)
                logsLoaded = flag;

            if (logsLoaded)
            {
                logsLoaded = false;
                return true;
            }
            else
                return false;
        }
    }
    public struct SetupTab
    {
        public static SetupMVC Setup { get; set; }
        //public static List<BossCombatData> CurrentSubBossList { get; set; } = new List<BossCombatData>();
        //public static sbyte currentSubBossIndex { get; set; } = 0;
        //public static BossCombatData CurrentSubBoss { get { return CurrentSubBossList[currentSubBossIndex]; } }
    }
    public struct BossInfoTab
    {
        public static List<BossCombatData> CurrentSubBossList { get; set; } = new List<BossCombatData>();
        public static sbyte currentSubBossIndex { get; set; } = 0;
        public static BossCombatData CurrentSubBoss { get { return CurrentSubBossList[currentSubBossIndex]; } }
    }
}
