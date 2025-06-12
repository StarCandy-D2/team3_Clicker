using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    private string saveFolder;

    public UserDataManager Instance;

    private void Awake()
    {
        // 싱글톤 생성
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환에도 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        saveFolder = Application.persistentDataPath;
    }

    // 저장
    public void SaveUserData(UserData data, string fileName)
    {
        string path = Path.Combine(saveFolder, $"{fileName}.json");
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log($"[저장 완료] {path}");
    }

    // 불러오기
    public UserData LoadUserData(string fileName)
    {
        string path = Path.Combine(saveFolder, $"{fileName}.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            UserData data = JsonUtility.FromJson<UserData>(json);
            Debug.Log($"[불러오기 완료] {fileName}");
            return data;
        }
        else
        {
            Debug.LogWarning("파일 없음. 새로 생성");
            return new UserData { userName = fileName }; // 기본값 포함
        }
    }

    // 세이브 파일 목록 불러오기
    public List<string> GetSaveFileNames()
    {
        List<string> fileNames = new List<string>();
        string[] files = Directory.GetFiles(saveFolder, "*.json");

        foreach (var file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            fileNames.Add(fileName);
        }

        return fileNames;
    }
}
