using QFramework;
using UnityEngine;

namespace Survivor
{
    public class SaveSystem : AbstractSystem
    {
        public void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
        
        public bool LoadBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }
        
        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        
        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        
        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        
        public string LoadString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
        
        protected override void OnInit()
        {
            
        }
    }
}