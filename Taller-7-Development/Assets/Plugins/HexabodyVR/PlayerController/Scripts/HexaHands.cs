using UnityEngine;

namespace HexabodyVR.PlayerController
{
    [RequireComponent(typeof(Rigidbody))]
    public class HexaHands : MonoBehaviour
    {
        public Rigidbody ParentRigidBody;

        [Tooltip("Target transform for position and rotation tracking")]
        public Transform Target;
        public Transform Camera;
        public Transform Shoulder;

        public float MaxDistance = .85f;

        [Header("Joint Settings")]
        public float Spring = 5000;
        public float Damper = 1000;
        public float MaxForce = 1500;

        public float SlerpSpring = 3000;
        public float SlerpDamper = 200;
        public float SlerpMaxForce = 1500;

        private Quaternion _startingRotation;
        private Vector3 _previousCamera;
        private Vector3 _previousControllerPosition;

        public Rigidbody RigidBody { get; private set; }

        public ConfigurableJoint Joint { get; protected set; }

        void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            SetupJoint();
        }

        public void Start()
        {
        }

        void FixedUpdate()
        {
            UpdateJointAnchors();
            UpdateTargetVelocity();
            UpdateJointRotation();
            UpdateJoint();
        }

        public void SetupJoint()
        {
            //Debug.Log($"{name} joint created.");
            //this joint needs to be created before any offsets are applied to the controller target
            //due to how joints snapshot their initial rotations on creation
            Joint = ParentRigidBody.transform.gameObject.AddComponent<ConfigurableJoint>();
            Joint.connectedBody = RigidBody;
            Joint.autoConfigureConnectedAnchor = false;
            Joint.anchor = Vector3.zero;
            Joint.connectedAnchor = Vector3.zero;

            Joint.enableCollision = false;
            Joint.enablePreprocessing = false;

            var drive = new JointDrive();
            drive.positionSpring = Spring;
            drive.positionDamper = Damper;
            drive.maximumForce = MaxForce;
            var slerpDrive = new JointDrive();
            slerpDrive.positionSpring = SlerpSpring;
            slerpDrive.positionDamper = SlerpDamper;
            slerpDrive.maximumForce = SlerpMaxForce;
            Joint.rotationDriveMode = RotationDriveMode.Slerp;
            Joint.xDrive = Joint.yDrive = Joint.zDrive = drive;
            Joint.slerpDrive = slerpDrive;

        }

        private void UpdateJoint()
        {
            var drive = Joint.xDrive;
            drive.positionSpring = Spring;
            drive.positionDamper = Damper;
            drive.maximumForce = MaxForce;
            var slerpDrive = Joint.slerpDrive;
            slerpDrive.positionSpring = SlerpSpring;
            slerpDrive.positionDamper = SlerpDamper;
            slerpDrive.maximumForce = SlerpMaxForce;
            Joint.xDrive = Joint.yDrive = Joint.zDrive = drive;
            Joint.slerpDrive = slerpDrive;
        }

        protected void UpdateJointAnchors()
        {
            var targetLocalPosition = ParentRigidBody.transform.InverseTransformPoint(Target.position);
            if (Shoulder)
            {
                var localAnchor = ParentRigidBody.transform.InverseTransformPoint(Shoulder.position);
                var dir = targetLocalPosition - localAnchor;
                dir = Vector3.ClampMagnitude(dir, MaxDistance);
                var point = localAnchor + dir;
                Joint.targetPosition = point;
            }
            else
            {
                if (Vector3.Distance(transform.position, Target.position) > MaxDistance)
                {
                    transform.position = Target.position;
                }
                Joint.targetPosition = targetLocalPosition;
            }
        }

        protected void UpdateJointRotation()
        {
            Joint.targetRotation = Quaternion.Inverse(ParentRigidBody.rotation) * Target.rotation;
        }

        public void UpdateTargetVelocity()
        {
            var camVelocity = (Camera.localPosition - _previousCamera) / Time.fixedDeltaTime;
            camVelocity.y = 0f;
            _previousCamera = Camera.localPosition;

            var local = ParentRigidBody.transform.InverseTransformPoint(Target.position);
            var velocity = (local - _previousControllerPosition) / Time.fixedDeltaTime;
            _previousControllerPosition = local;
            Joint.targetVelocity = velocity + camVelocity;
        }
    }
}
