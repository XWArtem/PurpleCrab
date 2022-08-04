using UnityEngine;

namespace DataArchitecture
{
    public class StatsRepository : Repository
    {
        private const string KEY_CRYSTALS = "KEY_CRYSTALS";
        private const string KEY_CHARACTERMOVESPEED = "KEY_CHARACTERMOVESPEED";
        private const string KEY_CHARACTERJUMPFORCE = "KEY_CHARACTERJUMPFORCE";
        private const string KEY_LEVEL_PROGRESS = "KEY_LEVEL_PROGRESS";

        public int Crystals { get; set; }
        public int LevelProgress;

        public float CharacterMoveSpeed = 6f;
        public float CharacterJumpForce = 6f;

        public override void Initialize()
        {
            Crystals = PlayerPrefs.GetInt(KEY_CRYSTALS, 0);
            LevelProgress = PlayerPrefs.GetInt(KEY_LEVEL_PROGRESS, 2);
            CharacterMoveSpeed = PlayerPrefs.GetFloat(KEY_CHARACTERMOVESPEED, 6f);
            CharacterJumpForce = PlayerPrefs.GetFloat(KEY_CHARACTERJUMPFORCE, 6f);
        }
        public override void Save()
        {
            PlayerPrefs.SetInt(KEY_CRYSTALS, Crystals);
            PlayerPrefs.SetInt(KEY_LEVEL_PROGRESS, LevelProgress);
            PlayerPrefs.SetFloat(KEY_CHARACTERMOVESPEED, CharacterMoveSpeed);
            PlayerPrefs.SetFloat(KEY_CHARACTERJUMPFORCE, CharacterJumpForce);
        }
    }
}
