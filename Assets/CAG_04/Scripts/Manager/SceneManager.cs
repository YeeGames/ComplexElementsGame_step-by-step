using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace CEG_04
{
    public class SceneManager : MonoBehaviour
    {
        private void Awake()
        {
            if (!File.Exists("Assets/Resources/CAG_04/Settings/Game Settings.asset"))
            {
                ScriptableObject gset = ScriptableObject.CreateInstance<GameSettings>();
                AssetDatabase.CreateAsset(gset, "Assets/Resources/CAG_04/Settings/Game Settings.asset");
            }

            if (!File.Exists("Assets/Resources/CAG_04/Settings/Agent Settings.asset"))
            {
                ScriptableObject aset = ScriptableObject.CreateInstance<AgentSettings>();
                AssetDatabase.CreateAsset(aset, "Assets/Resources/CAG_04/Settings/Agent Settings.asset");
            }

            if (!File.Exists("Assets/Resources/CAG_04/Settings/Rule Settings.asset"))
            {
                ScriptableObject rset = ScriptableObject.CreateInstance<RuleSettings>();
                AssetDatabase.CreateAsset(rset, "Assets/Resources/CAG_04/Settings/Rule Settings.asset");
            }

            var Wall_prefab = Resources.Load("CAG_04/Prefabs/Wall");
            Wall_prefab.AddComponent<WallManager>();
            GameObject.Instantiate(Wall_prefab);
            var MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            MainCamera.AddComponent<CameraManager>();
            var AgentsManager_prefab = Resources.Load("CAG_04/Prefabs/AgentsManager");
            AgentsManager_prefab.AddComponent<AgentsManager>();
            GameObject.Instantiate(AgentsManager_prefab);
        }
    }
}