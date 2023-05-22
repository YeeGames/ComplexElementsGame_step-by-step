using Unity.Collections;
using UnityEngine;

namespace CAG_04
{
    /// <summary>
    /// Yee个体组件
    /// </summary>
    public class YeeAgent : MonoBehaviour
    {
        /// <summary>
        /// 个体外形组件
        /// </summary>
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// 个体刚体组件 
        /// </summary>
        private Rigidbody2D agentRigidbody2D;

        // /// <summary>
        // /// 自带的点效应器组件  //NOTE 删除
        // /// </summary>
        // private PointEffector2D pointEffector;

        /// <summary>
        /// 刚体圆形碰撞器组件
        /// </summary>
        private CircleCollider2D rigidbodyCircleCollider2D;

        // /// <summary>
        // /// 自带的点效应器圆形碰撞器组件  //NOTE 删除
        // /// </summary>
        // private CircleCollider2D effectorCircleCollider2D;

        /// <summary>
        /// Yee规则圆形碰撞器组件
        /// </summary>
        private CircleCollider2D ruleCircleCollider2D;

        /// <summary>
        /// 个体物理材质
        /// </summary>
        private PhysicsMaterial2D physicsMaterial2D;

        /// <summary>
        /// Yee规则效应器范围外形呈现组件
        /// </summary>
        private SpriteRenderer agentRuleEffectorAreaSpriteRenderer;

        /// <summary>
        /// 个体设置项
        /// </summary>
        [HideInInspector] public AgentSettings aset;

        /// <summary>
        /// Game设置项
        /// </summary>
        [HideInInspector] public GameSettings gset;

        /// <summary>
        /// Rule设置项 //NOTE 新增
        /// </summary>
        [HideInInspector] public RuleSettings rset;

        /// <summary>
        /// id
        /// </summary>
        [ReadOnly] [SerializeField] private string id;

        /// <summary>
        /// 最大线速度标量值
        /// </summary>
        [SerializeField] private float maxSpeed;

        /// <summary>
        /// 最大角速度标量值
        /// </summary>
        [SerializeField] private float maxAngularSpeed;

        /// <summary>
        /// Yee类型 //NOTE 新增
        /// </summary>
        [ReadOnly] [SerializeField] internal string yeeType;

        /// <summary>
        /// Yee交互组件 //NOTE 新增
        /// </summary>
        private YeeInteraction inter;

        /// <summary>
        /// 净规则力 //NOTE 新增
        /// </summary>
        [ReadOnly] [SerializeField] private Vector2 netRuleForce;

        /// <summary>
        /// 个体外表颜色（不透明）
        /// </summary>
        private Color Color
        {
            get => spriteRenderer.color;
            set
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = value;
                }
            }
        }

        /// <summary>
        /// 位置
        /// </summary>
        private Vector2 Position
        {
            get => this.transform.position;
            set
            {
                if (spriteRenderer != null)
                {
                    transform.position = value;
                }
            }
        }

        /// <summary>
        /// 速度
        /// </summary>
        private Vector2 Velocity
        {
            get => this.transform.position;
            set
            {
                if (spriteRenderer != null)
                {
                    agentRigidbody2D.velocity = value;
                }
            }
        }

        /// <summary>
        /// 初始化 //NOTE 变动
        /// </summary>
        /// <param name="agentSettings">个体设置项</param>
        /// <param name="ruleSettings">规则设置项</param>
        internal void Initialize(AgentSettings agentSettings, RuleSettings ruleSettings)
        {
            /// 添加Yee 2元素规则组件
            this.inter.yee2ERule = this.gameObject.AddComponent<Yee2ERule>();
            /// 设置YeeRule
            this.inter.yee2ERule.SetRule(ruleSettings);
            /// 设置Agent
            this.SetAgentSettings(agentSettings);
        }

        /// <summary>
        /// 设置agent
        /// </summary>
        /// <param name="agentSettings">agent设置项</param>
        internal void SetAgentSettings(AgentSettings agentSettings)
        {
            this.id = agentSettings.id;
            this.yeeType = agentSettings.YeeType; //NOTE 新增
            this.name = agentSettings.agentBaseName + agentSettings.YeeType + this.id; //NOTE 变动
            this.transform.gameObject.tag = agentSettings.tag;
            this.Color = agentSettings.color;
            this.Position = agentSettings.position;
            this.Velocity = agentSettings.velocity;
            this.rigidbodyCircleCollider2D.radius = agentSettings.collisionRadius;
            // this.effectorCircleCollider2D.radius = agentSettings.magnitudeForceRadius; //NOTE 删除
            // this.pointEffector.forceMagnitude = agentSettings.magnitudeForce; //NOTE 删除
            this.maxSpeed = agentSettings.maxSpeed;
            this.maxAngularSpeed = agentSettings.maxAngularSpeed;
            this.agentRigidbody2D.mass = agentSettings.mass;
            this.agentRigidbody2D.drag = agentSettings.linearDrag;
            this.agentRigidbody2D.angularDrag = agentSettings.angularDrag;

            /// 自定义2D物理材质。
            this.physicsMaterial2D = new PhysicsMaterial2D();
            if (this.physicsMaterial2D != null)
            {
                this.physicsMaterial2D.friction = this.aset.physicsMaterialFriction;
                this.physicsMaterial2D.bounciness = this.aset.physicsMaterialBounciness;
            }

            this.agentRigidbody2D.sharedMaterial = this.physicsMaterial2D;
            // this.rigidbodyCircleCollider2D.sharedMaterial = this.physicsMaterial2D;//NOTE 删除
            // this.effectorCircleCollider2D.sharedMaterial = this.physicsMaterial2D; //NOTE 删除
            this.ruleCircleCollider2D.sharedMaterial = this.physicsMaterial2D; //NOTE 新增

            /// 设置规则圆圈碰撞器之范围之半径  //NOTE 新增
            this.ruleCircleCollider2D.radius = this.inter.yee2ERule.ruleCircleColliderRadius;
            this.agentRuleEffectorAreaSpriteRenderer.transform.localScale = new Vector3(this.ruleCircleCollider2D.radius * 2, this.ruleCircleCollider2D.radius * 2, 1);
        }


        void Awake()
        {
            /// 载入资源
            this.aset = Resources.Load<AgentSettings>("CAG_04/Settings/Agent Settings");
            this.gset = Resources.Load<GameSettings>("CAG_04/Settings/Game Settings");
            this.rset = Resources.Load<RuleSettings>("CAG_04/Settings/Rule Settings"); //NOTE 新增
            Sprite agentMaterial = Resources.Load<Sprite>("CAG_04/Materials/Agent Material");
            Sprite agentRuleEffectorAreaMaterial = Resources.Load<Sprite>("CAG_04/Materials/Rule Effector Material"); //NOTE 新增
            PhysicsMaterial2D agentPhysicsMaterial = Resources.Load<PhysicsMaterial2D>("CAG_04/Materials/Agent Physics Material 2D");

            this.agentRigidbody2D = this.GetComponent<Rigidbody2D>();
            this.agentRigidbody2D.sharedMaterial = agentPhysicsMaterial;
            this.spriteRenderer = this.gameObject.transform.Find("AgentSpriteRenderer").GetComponent<SpriteRenderer>();
            this.spriteRenderer.sprite = agentMaterial;
            this.rigidbodyCircleCollider2D = this.gameObject.transform.Find("AgentCollider").GetComponent<CircleCollider2D>();
            this.rigidbodyCircleCollider2D.sharedMaterial = agentPhysicsMaterial;
            // this.effectorCircleCollider2D = this.gameObject.transform.Find("AgentEffector").GetComponent<CircleCollider2D>();  //NOTE 删除
            // this.pointEffector = this.gameObject.transform.Find("AgentEffector").GetComponent<PointEffector2D>();  //NOTE 删除
            this.ruleCircleCollider2D = this.gameObject.transform.Find("AgentRuleEffector").GetComponent<CircleCollider2D>(); //NOTE 新增
            this.ruleCircleCollider2D.sharedMaterial = agentPhysicsMaterial; //NOTE 新增
            this.inter = this.gameObject.AddComponent<YeeInteraction>(); //NOTE 新增
            this.agentRuleEffectorAreaSpriteRenderer = this.gameObject.transform.Find("AgentRuleEffectorArea").GetComponent<SpriteRenderer>(); //NOTE 新增
            agentRuleEffectorAreaSpriteRenderer.sprite = agentRuleEffectorAreaMaterial; //NOTE 新增
        }


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void FixedUpdate()
        {
            // this.transform.Translate(Vector2.zero * (Time.deltaTime));
            this.agentRigidbody2D.velocity = Vector2.ClampMagnitude(agentRigidbody2D.velocity, maxSpeed); /// 限制最大速度；
            this.agentRigidbody2D.angularVelocity = Mathf.Max(agentRigidbody2D.angularVelocity, maxAngularSpeed); /// 限制最大角速度；
        }

        private void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            /// 添加入Yee规则圆圈碰撞器范围内的邻居个体之YeeAgent、以及计算相关的、YeeInterType、添加Yee规则力。
            // if (otherCollider2D.GetComponentInParent<YeeAgent>() && rigidbodyCircleCollider2D.gameObject.name == otherCollider2D.gameObject.name) // YeeRuleEffector只与agent刚体碰撞器交互
            if (otherCollider2D.GetComponentInParent<YeeAgent>()) // 不与墙发生YeeRule交互
            {
                this.inter.AddYeeInterAgent(this, otherCollider2D.GetComponentInParent<YeeAgent>()); //NOTE 新增
                // Debug.Log(this.gameObject.name + " " + " trigger " + otherCollider2D.transform.parent.name + " " + otherCollider2D.gameObject.name);
            }
        }

        private void OnTriggerExit2D(Collider2D otherCollider2D) //NOTE 新增
        {
            /// 移除出Yee规则圆圈碰撞器范围外的邻居YeeAgent
            // if (otherCollider2D.GetComponentInParent<YeeAgent>() && rigidbodyCircleCollider2D.gameObject.name == otherCollider2D.gameObject.name) // YeeRuleEffector只与agent刚体碰撞器交互
            if (otherCollider2D.GetComponentInParent<YeeAgent>()) // 不与墙发生YeeRule交互
            {
                this.inter.RemoveYeeInterAgent(otherCollider2D.GetComponentInParent<YeeAgent>());
            }
        }

        private void OnTriggerStay2D(Collider2D otherCollider2D) //NOTE 新增
        {
            // if (otherCollider2D.GetComponentInParent<YeeAgent>() && rigidbodyCircleCollider2D.gameObject.name == otherCollider2D.gameObject.name) // YeeRuleEffector只与agent刚体碰撞器交互
            if (otherCollider2D.GetComponentInParent<YeeAgent>()) // 不与墙发生YeeRule交互
            {
                /// 更新YeeInterAgent
                inter.UpdateYeeInteractionStatus(this.Position, otherCollider2D.GetComponentInParent<YeeAgent>().Position);

                /// 计算规则力之净力
                this.netRuleForce = this.inter.ApplyRuleForce();

                /// 施加规则力
                this.agentRigidbody2D.AddForce(this.netRuleForce, ForceMode2D.Force);
            }
        }
    }
}