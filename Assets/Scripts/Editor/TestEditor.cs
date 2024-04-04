using UnityEditor;
using UnityEngine;

public class TestEditor
{
        [MenuItem("Tools/Test/删除存档数据")]
        public static void ClearAllData()
        {
                PlayerPrefs.DeleteAll();
                Debug.Log("存档数据删除完成");
        }
}
