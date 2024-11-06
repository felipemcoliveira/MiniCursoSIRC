using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AimIKSolver))]
public class AimIKAnimatorBinder : MonoBehaviour
{
   [SerializeField]
   private string m_WeightParameterName = "AimIKWeight";

   private int m_WeightParameterHash;
   private Animator m_Animator;
   private AimIKSolver m_AimIKSolver;

   private void Awake()
   {
      m_Animator = GetComponent<Animator>();
      m_AimIKSolver = GetComponent<AimIKSolver>();

      UpdateWeightParamterHash();
   }

   private void OnValidate()
   {
      UpdateWeightParamterHash();
   }

   private void Update()
   {
      if (m_Animator == null || m_AimIKSolver == null || string.IsNullOrEmpty(m_WeightParameterName))
      {
         return;
      }

      m_AimIKSolver.Weight = m_Animator.GetFloat(m_WeightParameterHash);
   }

   private void UpdateWeightParamterHash()
   {
      m_WeightParameterHash = Animator.StringToHash(m_WeightParameterName);
   }
}
