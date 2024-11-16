using UnityEngine;

public class SignFunc : MonoBehaviour
{
    private void Start()
    {
        var nums = new[] { -1, -2, -3, -4, 3, 2, 1 };
        var nums2 = new[] { 1, 5, 0, 2, -3 };
        print(Multiply(nums));
        print(Multiply(nums2));
        print(SignFun(Multiply(nums)));
        print(SignFun(Multiply(nums2)));
    }

    public int Multiply(int[] nums) {
        int multiplied = 1;
        for (int i = 0; i < nums.Length; i++)
        {
            multiplied *= nums[i];
        }

        return multiplied;
    }

    public int SignFun(int values) => values > 0 ? 1 : values < 0 ? -1 : 0;
}