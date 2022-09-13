using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager ins;
    [SerializeField] List<CurrencySC> Currencies = new List<CurrencySC>();
    const string LogOutKey = "LogOutTime";
    float timer;
    float delay;

    private void Awake()
    {
        if (ins == null)
            ins = this;

        Load();
    }

    private void Start()
    {
        //PlayerPrefs.SetString(LogOutKey, "20.02.2022 11:53:00");
    }

    private void LateUpdate()
    {
        
    }

    private void OnApplicationPause(bool pause)
    {
        Save();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    
    private void SaveDateAndTime()
    {
        if(timer <= Time.time)
        {
            timer = Time.time + delay;
            Save();
        }
    }

    private void Save()
    {
        // Currencies
        if(Currencies.Count > 0)
            for (int i = 0; i < Currencies.Count; i++)
                PlayerPrefs.SetInt(Currencies[i].name, Currencies[i].Value);

        // Log out date and time
        PlayerPrefs.SetString(LogOutKey, System.DateTime.UtcNow.ToLocalTime().ToString());
    }

    private void Load()
    {
        // Currencies
        for (int i = 0; i < Currencies.Count; i++)
            Currencies[i].Value =  PlayerPrefs.GetInt(Currencies[i].name);
    }
    
    public double GetPassedTime(TimeReturnType type)
    {
        System.DateTime oldDate = StringToDate(PlayerPrefs.GetString(LogOutKey));
        double result;

        switch (type)
        {
            case TimeReturnType.Seconds:
                result = (System.DateTime.UtcNow.ToLocalTime() - oldDate).TotalSeconds;
                break;
            case TimeReturnType.Minutes:
                result = (System.DateTime.UtcNow.ToLocalTime() - oldDate).TotalMinutes;
                break;
            case TimeReturnType.Hours:
                result = (System.DateTime.UtcNow.ToLocalTime() - oldDate).TotalHours;
                break;
            default:
                result = 0.0;
                break;
        }

            return result;
    }
    
    private System.DateTime StringToDate(string dateString)
    {
        if(dateString.Trim() != "")
        {
            string[] subStr = dateString.Split(' ');
            string[] date = subStr[0].Split('.');
            string[] time = subStr[1].Split(':');

            return new System.DateTime(
                int.Parse(date[2]), // year
                int.Parse(date[1]), // month
                int.Parse(date[0]), // day
                int.Parse(time[0]), // hour
                int.Parse(time[1]), // minute
                int.Parse(time[2])); // second
        }
        else
        {
            throw new System.Exception("Date string is null.");
        }
        
    }
}

public enum TimeReturnType
{
    Seconds,
    Minutes,
    Hours
}
