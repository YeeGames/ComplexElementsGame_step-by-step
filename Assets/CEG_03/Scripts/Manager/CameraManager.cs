using UnityEngine;

namespace CEG_03
{
    public class CameraManager : MonoBehaviour
    {
        public GameSettings gset;

        // Start is called before the first frame update
        void Start()
        {
            /// 载入资源
            gset = Resources.Load<GameSettings>("CEG_03/Settings/Game Settings");

            /// 设置主摄像机视口大小
            this.gameObject.GetComponent<Camera>().orthographicSize = gset.stageRadius + 2;
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}