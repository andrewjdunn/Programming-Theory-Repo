using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class OptionsManager : MonoBehaviour
{

    [Serializable]
    public class OptionsData
    {
        public float DiggerRatio;
        public float FarmerRatio;
    }

    [SerializeField]
    private GameObject DiggerSlider;
    [SerializeField]
    private GameObject FarmerSlider;

    private OptionsData _optionsData;

    public OptionsData Options { get { return _optionsData; } }

    private static OptionsManager _optionsManager;

    public static OptionsManager Instance { get { return _optionsManager; } }

    // Start is called before the first frame update
    void Awake()
    {
        if (_optionsManager == null)
        {
            _optionsManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
    }

    public void LoadData()
    {
        string path = GetOptionDataFilePath();
        if (File.Exists(path))
        {
            var jsonString = File.ReadAllText(path);
            _optionsData = JsonUtility.FromJson<OptionsData>(jsonString);
        }
        else
        {
            Debug.LogWarning($"File {path} does not exist");
        }
    }

    private void OnDestroy()
    {
        var jsonString = JsonUtility.ToJson(_optionsData);
        File.WriteAllText(GetOptionDataFilePath(), jsonString);
    }

    private static string GetOptionDataFilePath()
    {
        return $"{Application.persistentDataPath}/options.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
