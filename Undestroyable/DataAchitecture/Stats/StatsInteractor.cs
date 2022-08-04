namespace DataArchitecture
{
    public class StatsInteractor : Interactor
    {
        private StatsRepository _statsRepository;
        public int Crystals => _statsRepository.Crystals;
        public int LevelProgress => _statsRepository.LevelProgress;
        public float CharacterMoveSpeed => _statsRepository.CharacterMoveSpeed;
        public float CharacterJumpForce => _statsRepository.CharacterJumpForce;

        public StatsInteractor(StatsRepository _statsRepository)
        {
            this._statsRepository = _statsRepository;
        }
        public bool IsEnoughCrystals(int value)
        {
            return Crystals >= value;
        }
        public bool IsEnoughCharacterMoveSpeed(int value)
        {
            return CharacterMoveSpeed > value;
        }
        public bool IsEnoughCharacterJumpForce(int value)
        {
            return CharacterJumpForce > value;
        }
        public void AddCrystals(object sender, int value)
        {
            if (sender is GameManager)
            {
                _statsRepository.Crystals += value;
                _statsRepository.Save();
            }
        }
        public void SpendCrystals(object sender, int value)
        {
            if (sender is GameManager)
            {
                _statsRepository.Crystals -= value;
                _statsRepository.Save();
            }
        }
        public void CharacterMoveSpeedUp(object sender, float value)
        {
            if (sender is GameManager)
            {
                _statsRepository.CharacterMoveSpeed += value;
                _statsRepository.Save();
            }
        }
        public void CharacterJumpForceUp(object sender, int value)
        {
            if (sender is GameManager)
            {
                _statsRepository.CharacterJumpForce += value;
                _statsRepository.Save();
            }
        }
        public void ResetAllStats()
        {
            _statsRepository.Crystals = 0;
            _statsRepository.CharacterMoveSpeed = 6f;
            _statsRepository.CharacterJumpForce = 6f;
            _statsRepository.LevelProgress = 1;
            _statsRepository.Save();
        }
        public void LevelProgressUp()
        {
            _statsRepository.LevelProgress++;
            _statsRepository.Save();
        }
    }
}