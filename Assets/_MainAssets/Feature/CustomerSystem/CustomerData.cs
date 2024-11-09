using UnityEngine;

namespace CustomerSystem
{
    [CreateAssetMenu(fileName = "CustomerData", menuName = "RestaurantGame/CustomerData")]
    public class CustomerData : MyScriptableObject
    {
        public float patience = 10;
        public Sprite customerSprite;
    }

    public struct CustomerLeaveData
    {
        public bool isLastCustomer;
        public CustomerLeaveReason leaveReason;

        public CustomerLeaveData(bool isLastCustomer = false,
            CustomerLeaveReason leaveReason = CustomerLeaveReason.Served)
        {
            this.isLastCustomer = isLastCustomer;
            this.leaveReason = leaveReason;
        }
    }

    public enum CustomerLeaveReason
    {
        Served,
        Angry
    }
}