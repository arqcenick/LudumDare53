public class Timer
{
    public bool IsPassed => _isPassed;

    public float TimePassed => _timePassed;

    public float TimeLimit;
    private float _timePassed;
    private bool _isPassed;
    public Timer(float timeLimit)
    {
        TimeLimit = timeLimit;
    }
    public void Tick(float dt)
    {
        _timePassed += dt;
        if(_timePassed > TimeLimit)
        {
            _isPassed = true;
        }
    }

    public void Reset()
    {
        _timePassed = 0;
        _isPassed = false;
    }
}
