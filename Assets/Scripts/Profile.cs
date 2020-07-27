using System;
using System.Collections.Generic;
using UnityEngine;

public static class Profile
{
    [System.Serializable]
    private class MainData
    {
        public List<int> LevelStars;
        public int Money = 10;
    }

    [System.Serializable]
    private class PlayerData
    {
        public bool Sound = true;
        public bool Music = true;
    }

    private static MainData mainData;
    private static PlayerData playerData;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CheckMainData()
    {
        Debug.Log("Init profile main data");
        if (mainData != null)
        {
        }

        if (!PlayerPrefs.HasKey("MainData"))
        {
            mainData = new MainData();
            PlayerPrefs.SetString("MainData", JsonUtility.ToJson(mainData));
            return;
        }

        mainData = JsonUtility.FromJson<MainData>(PlayerPrefs.GetString("MainData"));
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CheckPlayerData()
    {
        Debug.Log("Init profile player data");
        if (playerData != null)
        {
        }

        if (!PlayerPrefs.HasKey("PlayerData"))
        {
            playerData = new PlayerData();
            PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(mainData));
            return;
        }

        playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("PlayerData"));
    }

    private static void CheckData<T>(T data) where T : new()
    {
        Debug.Log("Init profile main data");
        if (data != null)
        {
        }

        if (!PlayerPrefs.HasKey(typeof(T).ToString()))
        {
            mainData = new MainData();
            PlayerPrefs.SetString(typeof(T).ToString(), JsonUtility.ToJson(mainData));
            return;
        }

        mainData = JsonUtility.FromJson<MainData>(PlayerPrefs.GetString(typeof(T).ToString()));
    }

    public static void Save(bool main = true, bool player = true)
    {
        if (main)
        {
            PlayerPrefs.SetString("MainData", JsonUtility.ToJson(mainData));
        }

        if (player)
        {
            PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(playerData));
        }
    }

    public static int Money
    {
        get => mainData.Money;
        set
        {
            mainData.Money = value;
            if (mainData.Money < 0)
            {
                mainData.Money = 0;
            }

            Save(player: false);
        }
    }

    public static int GetOpenedLevelCount => mainData.LevelStars.Count;

    public static int GetLevelStars(int level)
    {
        if (level >= mainData.LevelStars.Count)
        {
            return -1;
        }

        return mainData.LevelStars[level];
    }

    public static void SetLevelStars(int level, int stars)
    {
        if (level > mainData.LevelStars.Count)
        {
            Debug.LogError($"Level {level} is not opened");
            return;
        }

        if (level == mainData.LevelStars.Count)
        {
            stars = Mathf.Clamp(stars, 0, 3);
            mainData.LevelStars.Add(stars);
            Save(player: false);
            return;
        }

        if (stars > mainData.LevelStars[level])
        {
            mainData.LevelStars[level] = stars;
            Save(player:false);
        }
        
    }
    
}