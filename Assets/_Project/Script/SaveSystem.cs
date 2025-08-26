using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class SaveMatch
    {
        public float volume;
        public float mouseSensibility;
        public float brightness;
        public bool blood;
        public bool pvp;

        public SaveMatch() { }

        public SaveMatch(float volume, float mouseSensibility, float brightness, bool blood, bool pvp)
        {
            this.volume = volume;
            this.mouseSensibility = mouseSensibility;
            this.brightness = brightness;
            this.blood = blood;
            this.pvp = pvp;
        }
    }

    public Slider volume;
    public Slider mouseSensibility;
    public Slider brightness;
    public Toggle blood;
    public Toggle pvp;

    public bool usePlayerPref;

    private SaveMatch _saveMatch;
    private string _saveMatchString;
    private string _path;

    void Awake()
    {
        _path = Application.persistentDataPath + "/saveMatch.txt";
        Debug.Log(Application.persistentDataPath);
    }

    public void Save()
    {
        if (usePlayerPref)
        {
            if (SavePlayerPref())
            {
                Debug.Log("<color=green> Salvataggio PlayerPref </color> eseguito correttamente");
            }
            else
            {
                Debug.Log("<color=red> Salvataggio PlayerPref </color> NON eseguito correttamente, errori");
            }
        }
        else
        {
            if (SaveJson())
            {
                Debug.Log("<color=green> Salvataggio Json </color> eseguito correttamente");
            }
            else
            {
                Debug.Log("<color=red> Salvataggio Json </color> NON eseguito correttamente, errori");
            }
        }
    }

    public void Load()
    {
        if (usePlayerPref)
        {
            if (LoadPlayerPref())
            {
                Debug.Log("<color=green> Caricamento PlayerPref </color> eseguito correttamente");
            }
            else
            {
                Debug.Log("<color=red> Caricamento PlayerPref </color> NON eseguito correttamente, errori");
            }
        }
        else
        {
            if (LoadJson())
            {
                Debug.Log("<color=green> Caricamento Json </color> eseguito correttamente, errori");
            }
            else
            {
                Debug.Log("<color=red> Caricamento Json </color> NON eseguito correttamente, errori");
            }
        }
    }

    private bool SavePlayerPref()
    {
        try
        {
            PlayerPrefs.SetFloat((nameof(volume)), volume.value);
            PlayerPrefs.SetFloat((nameof(mouseSensibility)), mouseSensibility.value);
            PlayerPrefs.SetFloat((nameof(brightness)), brightness.value);
            PlayerPrefs.SetInt((nameof(blood)), (blood.isOn ? 1 : 0));
            PlayerPrefs.SetInt((nameof(pvp)), (pvp.isOn ? 1 : 0));
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool LoadPlayerPref()
    {
        int value;
        try
        {
            volume.value = PlayerPrefs.GetFloat((nameof(volume)), 100f);
            mouseSensibility.value = PlayerPrefs.GetFloat((nameof(mouseSensibility)), 50f);
            brightness.value = PlayerPrefs.GetFloat((nameof(brightness)), 75f);
            value = PlayerPrefs.GetInt((nameof(blood)), 0);
            blood.isOn = (value == 0) ? false : true;
            value = PlayerPrefs.GetInt((nameof(pvp)), 1);
            pvp.isOn = (value == 0) ? false : true;
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool SaveJson()
    {
        try
        {
            _saveMatch = new SaveMatch(volume.value, mouseSensibility.value, brightness.value, blood.isOn, pvp.isOn);
            _saveMatchString = JsonUtility.ToJson(_saveMatch, true);
            File.WriteAllText(_path, _saveMatchString);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool LoadJson()
    {
        try
        {
            if (File.Exists(_path))
            {
                _saveMatchString = File.ReadAllText(_path);
                _saveMatch = JsonUtility.FromJson<SaveMatch>(_saveMatchString);
                Debug.Log(string.Join("\n", _saveMatchString));
                volume.value = _saveMatch.volume;
                mouseSensibility.value = _saveMatch.mouseSensibility;
                brightness.value = _saveMatch.brightness;
                blood.isOn = _saveMatch.blood;
                pvp.isOn = _saveMatch.pvp;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
