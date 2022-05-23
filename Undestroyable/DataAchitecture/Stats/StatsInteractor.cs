namespace DataArchitecture
{
    public class StatsInteractor : Interactor
    {
        private StatsRepository _statsRepository;
        public int crystals => this._statsRepository.crystals;
        public int levelProgress => this._statsRepository.levelProgress;
        public float characterMoveSpeed => this._statsRepository.characterMoveSpeed;
        public float characterJumpForce => this._statsRepository.characterJumpForce;
        

        public StatsInteractor(StatsRepository _statsRepository)
        {
            this._statsRepository = _statsRepository;
        }

        public bool IsEnoughCrystals(int value)
        {
            return crystals >= value;
        }
        public bool IsEnoughCharacterMoveSpeed(int value)
        {
            return characterMoveSpeed > value;
        }
        public bool IsEnoughCharacterJumpForce(int value)
        {
            return characterJumpForce > value;
        }
        public void AddCrystals(object sender, int value)
        {
            this._statsRepository.crystals += value;
            this._statsRepository.Save();
        }
        public void SpendCrystals(object sender, int value)
        {
            this._statsRepository.crystals -= value;
            this._statsRepository.Save();
        }
        public void CharacterMoveSpeedUp(object sender, int value)
        {
            this._statsRepository.characterMoveSpeed += value;
            this._statsRepository.Save();
        }
        public void CharacterJumpForceUp(object sender, int value)
        {
            this._statsRepository.characterJumpForce += value;
            this._statsRepository.Save();
        }
        public void ResetAllStats()
        {
            this._statsRepository.crystals = 0;
            this._statsRepository.characterMoveSpeed = 6f;
            this._statsRepository.characterJumpForce = 6f;
            this._statsRepository.levelProgress = 1;
            this._statsRepository.Save();
        }
        public void LevelProgressUp()
        {
            this._statsRepository.levelProgress++;
            this._statsRepository.Save();
        }
    }
}