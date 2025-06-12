using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    private string saveFolder;

    private void Awake()
    {
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
            Debug.LogWarning($"파일이 존재하지 않습니다: {fileName}");
            return null;
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
