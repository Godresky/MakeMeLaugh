using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    private static string _key = "BakerGameData";
    private Storage _storage = new Storage();

    private void Awake(){
        if(PlayerPrefs.HasKey(_key))
            _storage = JsonUtility.FromJson<Storage>(PlayerPrefs.GetString(_key));
        else
            PlayerPrefs.SetString(_key, JsonUtility.ToJson(_storage));
    }

    [System.Serializable]
    public class Storage{
        //                                        Clients,                             
        public List<int> Info = new List<int>(2) {0, 0};
    }

    public enum DataType{
        Clients = 0,
    }
}
