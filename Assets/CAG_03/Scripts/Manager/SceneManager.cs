using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace CAG_03
{
    public class SceneManager : MonoBehaviour
    {
        //TODO 前面会用到
        // public GameSettings gset;
        // public AgentSettings aset;
        // public RuleSettings rset;
        // public CameraManager CameraManager;
        // public WallManager WallManager;
        // public AgentsManager AgentsManager;

        private void Awake()
        {
            // TODO 前面会用到
            // var gset = Resources.Load<GameSettings>("CAG_03/Settings/Game Settings");
            // var aset = Resources.Load<AgentSettings>("CAG_03/Settings/Agent Settings");
            // var rset = Resources.Load<RuleSettings>("CAG_03/Settings/Rule Settings");

            if (!File.Exists("Assets/Resources/CAG_03/Settings/Game Settings.asset"))
            {
                ScriptableObject gset = ScriptableObject.CreateInstance<GameSettings>();
                AssetDatabase.CreateAsset(gset, "Assets/Resources/CAG_03/Settings/Game Settings.asset");
            }

            if (!File.Exists("Assets/Resources/CAG_03/Settings/Agent Settings.asset"))
            {
                ScriptableObject aset = ScriptableObject.CreateInstance<AgentSettings>();
                AssetDatabase.CreateAsset(aset, "Assets/Resources/CAG_03/Settings/Agent Settings.asset");
            }

            // if (!File.Exists("Assets/Resources/CAG_03/Settings/Rule Settings.asset"))
            // {
            //     ScriptableObject rset = ScriptableObject.CreateInstance<RuleSettings>();
            //     AssetDatabase.CreateAsset(rset, "Assets/Resources/CAG_03/Settings/Rule Settings.asset");
            // }

            var Wall_prefab = Resources.Load("CAG_03/Prefabs/Wall");
            Wall_prefab.AddComponent<WallManager>();
            GameObject.Instantiate(Wall_prefab);
            var MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            MainCamera.AddComponent<CameraManager>();
            var AgentsManager_prefab = Resources.Load("CAG_03/Prefabs/AgentsManager");
            AgentsManager_prefab.AddComponent<AgentsManager>();
            GameObject.Instantiate(AgentsManager_prefab);
        }
    }
}