using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UserDataManager userDataManager;
    public UserData currentUser;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // 저장할 때
    void SaveCurrentUser()
    {
        userDataManager.SaveUserData(currentUser, currentUser.userName);
    }

    // 불러올 때
    void LoadUser(string fileName)
    {
        currentUser = userDataManager.LoadUserData(fileName);
    }
}