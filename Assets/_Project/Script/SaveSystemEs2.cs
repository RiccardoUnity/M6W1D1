using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveSystemEs2 : MonoBehaviour
{
    private PlayerSave _playerSave;
    private string _dataString;
    private string _path;

    [SerializeField] private Transform _player;

    void Awake()
    {
        _playerSave = new PlayerSave();
        _path = Application.persistentDataPath + "/save.txt";
        Debug.Log(_path);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Save())
            {
                Debug.Log("<color=green> Corretto Save </color>");
            }
            else
            {
                Debug.Log("<color=red> Errore Save </color>");
            }
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            if (Load())
            {
                Debug.Log("<color=green> Corretto Load </color>");
            }
            else
            {
                Debug.Log("<color=red> Errore Load </color>");
            }
        }
    }

    private bool Save()
    {
        float[] pos = new float[3];
        pos[0] = _player.position.x;
        pos[1] = _player.position.y;
        pos[2] = _player.position.z;

        float[] rot = new float[4];
        rot[0] = _player.rotation.x;
        rot[1] = _player.rotation.y;
        rot[2] = _player.rotation.z;
        rot[3] = _player.rotation.w;

        _playerSave = new PlayerSave(pos, rot);
        _dataString = JsonConvert.SerializeObject(_playerSave, Formatting.Indented);
        try
        {
            File.WriteAllText(_path, _dataString);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool Load()
    {
        try
        {
            if (File.Exists(_path))
            {
                _dataString = File.ReadAllText(_path);
                _playerSave = JsonConvert.DeserializeObject<PlayerSave>(_dataString);
                _player.transform.position = new Vector3(_playerSave.position[0], _playerSave.position[1], _playerSave.position[2]);
                _player.transform.rotation = new Quaternion(_playerSave.rotation[0], _playerSave.rotation[1], _playerSave.rotation[2], _playerSave.rotation[3]);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
