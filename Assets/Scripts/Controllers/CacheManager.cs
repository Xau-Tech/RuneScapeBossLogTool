﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CacheManager
{
    public static string currentBoss
    {
        get
        {
            if (ProgramState.CurrentState == ProgramState.states.Drops)
                return DropsTab.currentBoss;
            else if (ProgramState.CurrentState == ProgramState.states.Logs)
                return LogsTab.currentBoss;
            else
                return DataController.Instance.bossInfoDictionary.GetBossNames()[0];
        }
        set
        {
            if (ProgramState.CurrentState == ProgramState.states.Drops)
            {
                DropsTab.currentBoss = value;
                Debug.Log($"Drops boss set to {value}");
            }
            else if (ProgramState.CurrentState == ProgramState.states.Logs)
            {
                LogsTab.currentBoss = value;
                Debug.Log($"Logs boss set to {value}");
            }
            else
            {
                DropsTab.currentBoss = value;
                LogsTab.currentBoss = value;
                Debug.Log($"All bosses set to {value}");
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
    }

    public struct DropsTab
    {
        public enum Elements { ItemDropdown, LogDropdown };
        public static string currentLog { set; get; }
        public static string currentBoss { set; get; }

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
        public static string currentBoss { set; get; }

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
    }
}
