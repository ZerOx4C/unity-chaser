using UnityEngine;

namespace Runtime.CrowdDemo
{
    public class SmoothRotator : MonoBehaviour
    {
        [Range(0, 720)] public float maxAngularSpeed;
        [Range(-180, 180)] public float desiredAngle;

        public void SetDesiredForward(Vector3 forward)
        {
            desiredAngle = Vector3.SignedAngle(Vector3.forward, forward, Vector3.up);
        }

        private void Update()
        {
            var desiredForward = Quaternion.AngleAxis(desiredAngle, Vector3.up) * Vector3.forward;
            var diffAngle = Vector3.SignedAngle(transform.forward, desiredForward, Vector3.up);
            var deltaAngle = Mathf.Sign(diffAngle) * Mathf.Min(Time.deltaTime * maxAngularSpeed, Mathf.Abs(diffAngle));
            transform.rotation *= Quaternion.AngleAxis(deltaAngle, Vector3.up);
        }
    }
}
