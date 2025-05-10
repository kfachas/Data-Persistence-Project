using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string nickname;
    public int highScore;
    private string saveFilePath = Application.persistentDataPath + "/savefile.json";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSaveData();
    }

    [System.Serializable]
    public class SaveData
    {
        public int highScore;
        public string nickname;
    }

    public void SaveHighScore(int currentScore)
    {
        SaveData data = new SaveData();

        if (File.Exists(saveFilePath))
        {
            string savedJson = File.ReadAllText(saveFilePath);
            SaveData savedData = JsonUtility.FromJson<SaveData>(savedJson);

            if (currentScore > savedData.highScore)
            {
                data.highScore = currentScore;
            }
            data.nickname = savedData.nickname;
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(saveFilePath, json);
    }
    public void SaveNickname()
    {
        SaveData data = new SaveData { nickname = nickname };


        if (File.Exists(saveFilePath))
        {
            string savedJson = File.ReadAllText(saveFilePath);
            SaveData savedData = JsonUtility.FromJson<SaveData>(savedJson);

            data.highScore = savedData.highScore;
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(saveFilePath, json);
    }

    public void LoadSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            string savedJson = File.ReadAllText(saveFilePath);
            SaveData savedData = JsonUtility.FromJson<SaveData>(savedJson);

            highScore = savedData.highScore;
            nickname = savedData.nickname;
        }
    }
}
