using BattleshipLibrary.Core;

namespace BattleshipLibrary.Interfaces
{
    public interface ISquare
    {
        string CoOrdinates { get; }

        string GetPlayStatus();
        string GetSetupStatus();
        string GetStatus(Enums.GameMode inpGameMode);
    }
}