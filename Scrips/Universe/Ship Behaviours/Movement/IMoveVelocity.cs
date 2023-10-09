using UnityEngine;

public interface IMoveVelocity
{
    void SetVelocity(Vector3 MovePositionDirection);
    public void WarpSpeedUp(bool boolean);

    public void WarpCompleteBoolean();
}