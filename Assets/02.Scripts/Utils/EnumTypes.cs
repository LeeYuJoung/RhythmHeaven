namespace Utils.EnumTypes
{
    // 플레이어 상태
    public enum PlayerState
    {
        Idle,
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
        Idle,      // 기본 상태
        Greet,     // 인사 기다리는 상태
        Calculate,  // 계산 기다리는 상태
        Leave   // 모든 행동 완료 상태
    }
    
    // 물건 타입
    public enum ProductType
    {
        Basic,
        Strange
    }
}