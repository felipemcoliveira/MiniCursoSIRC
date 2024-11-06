using UnityEngine;

public class AimIKSolver : MonoBehaviour
{
   public float Weight
   {
      get => m_Weight;
      set => m_Weight = value;
   }

   public Transform Target
   {
      get => m_Target;
      set => m_Target = value;
   }

   [SerializeField]
   private Transform m_Effector;

   [SerializeField]
   private Transform m_Spine;

   [SerializeField]
   private Transform m_Target;

   [SerializeField, Range(0, 1)]
   private float m_Weight = 1.0f;

   private void LateUpdate()
   {
      if (m_Target == null || m_Weight <= float.Epsilon)
      {
         return;
      }

      m_Weight = Mathf.Clamp01(m_Weight);
      AimAtTarget();
   }

   private void AimAtTarget()
   {
      Vector3 effectorDirection = m_Effector.forward;
      Vector3 targetDirection = m_Target.position - m_Effector.position;

      effectorDirection.y = 0;
      targetDirection.y = 0;

      effectorDirection = Vector3.Normalize(effectorDirection);
      targetDirection = Vector3.Normalize(targetDirection);

      Quaternion rotation = Quaternion.FromToRotation(effectorDirection, targetDirection);
      rotation = Quaternion.Slerp(Quaternion.identity, rotation, m_Weight);

      m_Spine.rotation = rotation * m_Spine.rotation;
   }
}
