using UnityEngine;

public class OptionsDemo : MonoBehaviour
{
    OptionController options;
    [SerializeField] private OptionUI script;

    // Start is called before the first frame update
    void Awake()
    {
        options = new OptionController(new Options(), script);
        options.Setup();
    }
}
