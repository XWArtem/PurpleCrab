using UnityEngine;

namespace DataArchitecture
{
    public class StatsRepository : Repository
    {

        private const string KEY_CRYSTALS = "KEY_CRYSTALS";
        private const string KEY_CHARACTERMOVESPEED = "KEY_CHARACTERMOVESPEED";
        private const string KEY_CHARACTERJUMPFORCE = "KEY_CHARACTERJUMPFORCE";
        private const string KEY_LEVEL_PROGRESS = "KEY_LEVEL_PROGRESS";

        public int crystals { get; set; }
        public int levelProgress;

        public float characterMoveSpeed = 6f;
        public float characterJumpForce = 6f;

        public override void Initialize()
        {
            this.crystals = PlayerPrefs.GetInt(KEY_CRYSTALS, 0);
            this.levelProgress = PlayerPrefs.GetInt(KEY_LEVEL_PROGRESS, 2);
            this.characterMoveSpeed = PlayerPrefs.GetFloat(KEY_CHARACTERMOVESPEED, 6f);
            this.characterJumpForce = PlayerPrefs.GetFloat(KEY_CHARACTERJUMPFORCE, 6f);
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(KEY_CRYSTALS, this.crystals);
            PlayerPrefs.SetInt(KEY_LEVEL_PROGRESS, this.levelProgress);
            PlayerPrefs.SetFloat(KEY_CHARACTERMOVESPEED, this.characterMoveSpeed);
            PlayerPrefs.SetFloat(KEY_CHARACTERJUMPFORCE, this.characterJumpForce);
        }
    }
}
