namespace Utils.EnumTypes
{
    // 플레이어 상태
    public enum PlayerState
    {
        Idle
    }

    // 손님 타입
    public enum CustomerType
    {
        Basic,
        Strange
    }

    // 손님 상태
    public enum CustomerState
    {
        Idle,       // 기본 상태
        Greet,      // 인사 기다리는 상태
        Calculate,  // 계산 기다리는 상태
        Leave       // 모든 행동 완료 상태
    }
    
    // 물건 타입
    public enum ProductType
    {
        Basic,
        Strange
    }

    // BGM 타입
    public enum BGMType
    {
        Title, 
        Intro,
        Stage_1,
        Stage_2,
        Stage_3,
        Rule,
        DieEnding,
        HappyEnding
    }

    // SFX 타입
    public enum SFXType
    {
        Beat,        // 매트로놈 효과음
        StrageBeat,  // 이상현상 효과음
        Scanner,     // 계산 효과음
        Wrong,       // 틀렸을 때 효과음
        Siren,       // 경고 받았을 때 효과음
    }
}