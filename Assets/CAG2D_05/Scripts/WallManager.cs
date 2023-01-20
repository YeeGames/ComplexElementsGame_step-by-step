using Unity.VisualScripting;
using UnityEngine;

namespace CAG2D_05
{
    public class WallManager : MonoBehaviour
    {
        /// <summary>
        /// game settings
        /// </summary>
        public GameSettings gameSettings;

        /// <summary>
        /// 线渲染组件
        /// </summary>
        public LineRenderer lineRenderer; //LineRenderer组件

        /// <summary>
        /// 边碰撞器
        /// </summary>
        public EdgeCollider2D edgeCollider2D;

        /// <summary>
        /// 刚体
        /// </summary>
        public new Rigidbody2D rigidbody2D;

        /// <summary>
        /// 物理材质
        /// </summary>
        public PhysicsMaterial2D physicsMaterial2D;

        /// <summary>
        /// 边界墙圆心
        /// </summary>
        private Vector3 centerOfCircle;

        /// <summary>
        /// 边界墙半径
        /// </summary>
        private float boundingWallRadius;

        /// <summary>
        /// 完成一个圆的总点数
        /// </summary>
        private int positionCount;

        /// <summary>
        /// 转角，三个点形成的两段线之间的夹角
        /// </summary>
        private float angle;

        /// <summary>
        /// Quaternion四元数
        /// </summary>
        public Quaternion quaternion;

        /// <summary>
        /// 边界墙宽度
        /// </summary>
        private float boundingWallWidth;


        void Awake()
        {
            /// 生成外部围墙
            BuildBoundingWall();
        }


        void Update()
        {
        }


        void BuildBoundingWall()
        {
            centerOfCircle = new Vector2(0, 0);
            boundingWallWidth = gameSettings.stageRadiu * 2;
            boundingWallRadius = gameSettings.stageRadiu + boundingWallWidth / 2;
            positionCount = 360;
            angle = 360f / (positionCount - 1);
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = boundingWallWidth;
            lineRenderer.positionCount = positionCount;
            edgeCollider2D = GetComponent<EdgeCollider2D>();

            physicsMaterial2D = new PhysicsMaterial2D(); // 自行新建2D物理材质
            if (physicsMaterial2D != null)
            {
                physicsMaterial2D.friction = gameSettings.physicsMaterialsFriction;
                physicsMaterial2D.bounciness = gameSettings.physicsMaterialsBounciness;
            }
            else
            {
                Debug.Log("没有正确设置Wall Physics Material 2D.physicsMaterial2D");
            }

            rigidbody2D.sharedMaterial = physicsMaterial2D;
            edgeCollider2D.sharedMaterial = physicsMaterial2D;

            DrawCircle();
        }

        LineRenderer DrawCircle()
        {
            for (int i = 0; i < positionCount; i++)
            {
                if (i != 0)
                {
                    //默认围着z轴画圆，所以z值叠加，叠加值为每两个点到圆心的夹角
                    quaternion = Quaternion.Euler(quaternion.eulerAngles.x, quaternion.eulerAngles.y,
                        quaternion.eulerAngles.z + angle);
                }

                //Quaternion与Vector3的右乘操作（*）返回一个将原有向量做旋转操作后的新向量.列如：Quaternion.Euler(0, 90, 0) * Vector3(0.0, 0.0, -10) 相当于把向量Vector3(0.0, 0.0, -10)绕y轴旋转90度，得到的结果为Vector3(-10, 0.0.0.0)
                Vector3 forwardPosition = centerOfCircle + quaternion * Vector3.down * boundingWallRadius;
                lineRenderer.SetPosition(i, forwardPosition);

                // 添加圆形碰撞
                var circleCollider = gameObject.AddComponent<CircleCollider2D>();
                circleCollider.offset = forwardPosition;
                circleCollider.radius = lineRenderer.startWidth / 2f;

                // 边界碰撞体的点
                if (positionCount > 1)
                {
                    edgeCollider2D.points[i] = forwardPosition;
                }
            }

            return lineRenderer;
        }
    }
}