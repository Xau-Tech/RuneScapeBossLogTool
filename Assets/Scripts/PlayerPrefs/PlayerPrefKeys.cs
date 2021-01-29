public static class PlayerPrefKeys
{
    public enum KeyNamesEnum { Username };

    private static string[] keyNames = new string[] { "Username" };

    public static string GetKeyName(KeyNamesEnum keyNameEnum)
    {
        return keyNames[(int)keyNameEnum];
    }
}
