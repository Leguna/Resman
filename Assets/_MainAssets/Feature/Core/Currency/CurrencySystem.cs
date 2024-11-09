using Utilities.SaveLoad;

namespace Currency
{
    public class CurrencySystem : ISaveable
    {
        public Gold gold;

        public void Init()
        {
            gold = new Gold();
        }

        public string GetUniqueIdentifier()
        {
            return "CurrencySystem";
        }

        public object CaptureState()
        {
            return gold;
        }

        public void RestoreState(object state)
        {
            gold = (Gold)state;
        }
    }
}