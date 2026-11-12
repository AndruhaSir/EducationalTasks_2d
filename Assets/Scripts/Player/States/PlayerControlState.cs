public class PlayerControlState : IState, IFixedTickState
{
    private readonly Player _player;

    public PlayerControlState(Player player)
    {
        _player = player;
    }

    public void Enter() { }

    public void Tick()
    {
        if (_player.AttackPressed)
        {
            _player.ChangeToAttack();
            return;
        }

        if (_player.AbilityPressed)
        {
            _player.UseAbility();
        }
        
        _player.UpdateMovementInput();
    }

    public void FixedTick()
    {
        _player.Move();
    }

    public void Exit() { }
}
