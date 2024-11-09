using System;
using Utilities.SaveLoad;

namespace Currency
{
    [Serializable]
    public class BaseCurrency: ISaveable
    {
        public string id;
        public string name;
        public string description;
        public int value;

        public BaseCurrency()
        {
            id = "";
            name = "";
            value = 0;
        }

        public BaseCurrency(string id, string name, string description, int value)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.value = value;
        }

        public virtual int Add(int value)
        {
            this.value += value;
            return this.value;
        }

        public virtual int Subtract(int value)
        {
            this.value -= value;
            return this.value;
        }

        public virtual bool IsEnough(int value)
        {
            return this.value >= value;
        }

        public override string ToString()
        {
            return $"{name}: {value}";
        }

        public virtual string GetUniqueIdentifier()
        {
            return id;
        }

        public virtual object CaptureState()
        {
            return this;
        }

        public virtual void RestoreState(object state)
        {
            BaseCurrency currency = (BaseCurrency)state;
            value = currency.value;
        }
    }

    [Serializable]
    public class Gold : BaseCurrency
    {
        public Gold()
        {
            id = "gold";
            name = "Gold";
            description = "Gold is the main currency in the game.";
            value = 0;
        }
    }
}