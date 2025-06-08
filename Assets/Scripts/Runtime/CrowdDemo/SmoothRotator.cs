using UnityEngine;

namespace Runtime.CrowdDemo
{
    public class SmoothRotator : MonoBehaviour
    {
        [Range(0, 720)] public float maxAngularSpeed;

        private Vector3 _desiredForward;

        private void Update()
        {
            if (_desiredForward == Vector3.zero)
            {
                return;
            }

            var diffAngle = Vector3.SignedAngle(transform.forward, _desiredForward, Vector3.up);
            var absDiffAngle = Mathf.Abs(diffAngle);
            if (absDiffAngle < 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(_desiredForward);
                return;
            }

            var absDeltaAngle = Mathf.Min(Time.deltaTime * maxAngularSpeed, absDiffAngle);
            transform.Rotate(Vector3.up, Mathf.Sign(diffAngle) * absDeltaAngle);
        }

        public void SetDesiredForward(Vector3 forward)
        {
            _desiredForward = forward;
        }
    }
}
