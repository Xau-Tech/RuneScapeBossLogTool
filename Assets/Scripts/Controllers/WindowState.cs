﻿//  State for new windows set active or opened
public static class WindowState
{
    public enum states { None, AddToLog, AddLog, DeleteLog, AddSetup, AddSetupItemQuantity, RenameLog }
    public static states currentState;
}
