namespace VideoToSM.Enums
{
    public enum ESMXDifficultyType
    {
        Beginner,
        Easy,
        EasyPlus,
        Hard,
        HardPlus,
        Wild,
        Edit
    }

    public static class SMXDifficultyTypeExtensions
    {
        public static string ToSM(this ESMXDifficultyType difficultyType)
        {
            switch (difficultyType)
            {
                case ESMXDifficultyType.Beginner:
                    return ESMXDifficultyType.Beginner.ToString();
                case ESMXDifficultyType.Easy:
                    return ESMXDifficultyType.Easy.ToString();
                case ESMXDifficultyType.EasyPlus:
                    return ESMXDifficultyType.Easy.ToString();
                case ESMXDifficultyType.Hard:
                    return "Medium";
                case ESMXDifficultyType.HardPlus:
                    return ESMXDifficultyType.Hard.ToString();
                case ESMXDifficultyType.Wild:
                    return "Challenge";
                case ESMXDifficultyType.Edit:
                    return ESMXDifficultyType.Edit.ToString();
            }

            return "";
        }

        public static string Print(this ESMXDifficultyType difficultyType)
        {
            switch (difficultyType)
            {
                case ESMXDifficultyType.Beginner:
                    return ESMXDifficultyType.Beginner.ToString();
                case ESMXDifficultyType.Easy:
                    return ESMXDifficultyType.Easy.ToString();
                case ESMXDifficultyType.EasyPlus:
                    return ESMXDifficultyType.Easy.ToString() + "+";
                case ESMXDifficultyType.Hard:
                    return ESMXDifficultyType.Hard.ToString();
                case ESMXDifficultyType.HardPlus:
                    return ESMXDifficultyType.Hard.ToString() + "+";
                case ESMXDifficultyType.Wild:
                    return ESMXDifficultyType.Wild.ToString();
                case ESMXDifficultyType.Edit:
                    return ESMXDifficultyType.Edit.ToString();
            }

            return "";
        }
    }
}
