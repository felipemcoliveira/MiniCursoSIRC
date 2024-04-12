using UnityEngine;


public class PrototypeSpawner
{

}

public class SoldierAimIKSolver : MonoBehaviour
{
   [SerializeField]
   private Transform effector;

   [SerializeField]
   private Transform spine;

   [SerializeField]
   private Transform head;

   [SerializeField]
   private Transform target;

   private void LateUpdate()
   {
      if (target != null)
      {
         AimGunAtTarget();
      }
   }

   void AimGunAtTarget()
   {
      var targetDirection = new Vector3(target.position.x, effector.position.y, target.position.z) - effector.position;

      float angle = Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;

      var targetRotation = Quaternion.Euler(0, -angle, 0);

      var currentGunBarrelRotation = Quaternion.Euler(0, effector.eulerAngles.y, 0);
      var difference = Quaternion.Inverse(currentGunBarrelRotation) * targetRotation;

      spine.rotation = difference * spine.rotation;
   }
}
