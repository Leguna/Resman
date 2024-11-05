using SO;
using UnityEditor;

namespace Editor.site.arksana.resman
{
    public abstract class CreateScriptableObjectsMenu
    {
        [MenuItem("Assets/Create/RestaurantGame/FoodItem")]
        public static void CreateFoodItem()
        {
            ScriptableObjectUtility.CreateAsset<FoodItemData>();
        }
    }
}