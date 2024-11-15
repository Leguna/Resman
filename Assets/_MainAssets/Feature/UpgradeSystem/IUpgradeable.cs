namespace UpgradeSystem
{
    public interface IUpgradeable
    {
        public int Cost { get; }
        public string Name { get; }
        public int Level { get; }
        int MaxLevel { get; }
        void Upgrade();
        void Downgrade();
    }
}