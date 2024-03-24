using System.Text;
using Markdig.Helpers;
using UnityEngine;
using UnityEditor;

namespace Survivor
{
    public class RenameUnityAssets : ScriptableWizard
    {
        public bool IsRetainOldName = true; // 是否应用原文件名
        
        public string Prefix = "null"; // 类型
        public string Name = "null"; // 名称
        public string Suffix = "null"; // 后缀
        
        public bool IsSingle = false; // 是否是单个Asset
        public int BeginID = 1; // 起始ID

        [MenuItem("Tools/Unity Assets/批量重命名")]
        static void CreateWizard()
        {
            DisplayWizard<RenameUnityAssets>("Unity Assets 批量重命名", "取消", "重命名");
        }

        private int mNowID;
        private void OnWizardOtherButton()
        {
            if (!IsRetainOldName && (Name == null || Name == "" || Name == "null" || Name.Trim().Length == 0))
            {
                errorString = "请输入正确的“m_Name”";
                return;
            }

            mNowID = BeginID; // 重置当前ID为起始ID

            int index = 1;
            foreach (var asset in Selection.objects)
            {
                if (IsRetainOldName)
                {
                    if (!CheckPreFixHasNumber(asset.name))
                    {
                        errorString = $"第{index}个文件前缀没有数字 请重新选择";
                        return; 
                    }
                }
                
                RenameObject(asset);
                
                if (!IsRetainOldName && !IsSingle)
                {
                    mNowID++;
                }

                index++;
            }
        }

        bool CheckPreFixHasNumber(string assetName) // 检查第一个"_"前是否有数字
        {
            int firstIndex = assetName.IndexOf("_");
            for (int i = 0; i < firstIndex; i++)
            {
                if (assetName[i].IsDigit())
                {
                    return true;
                }
            }

            return false;
        }
        
        private void OnWizardUpdate()
        {
            helpString = null;
            errorString = null;

            if (Selection.objects.Length > 0)
            {
                helpString = "当前选择了" + Selection.objects.Length + "Unity Object";
            }
            else
            {
                errorString = "请至少选择一个Unity Object";
            }
        }

        private void OnSelectionChange()
        {
            OnWizardUpdate();
        }

        private void RenameObject(Object asset)
        {
            string path = AssetDatabase.GetAssetPath(asset);

            StringBuilder newName = new StringBuilder();
            if (IsRetainOldName)
            {
                string oldName = asset.name;
                int firstIndex = oldName.IndexOf("_");
                newName.Append(oldName.Substring(firstIndex + 1)); // 抛弃第一个'_'以及之前的前缀
            }
            else
            {
                if (Prefix != null && Prefix != "" && Prefix.Trim() != "null") // 添加前缀
                {
                    newName.Append(Prefix.Trim());
                    newName.Append("_");    
                }
                
                newName.Append(Name);
                
                
                if (Suffix != null && Suffix != "" && Suffix.Trim() != "null") // 添加后缀
                {
                    newName.Append("_");
                    newName.Append(Suffix.Trim());
                }

                if (!IsSingle)
                {
                    newName.Append("_");
                    newName.Append(mNowID.ToString("d3")); // 添加ID    
                }
            }

            AssetDatabase.RenameAsset(path, newName.ToString());
        }
    }
}
