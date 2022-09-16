using Avalonia.Animation.Easings;

namespace SpringDemo;

public class SpringEasing : Easing
{
    private readonly SpringSolver _springSolver;

    public SpringEasing()
    {
        _springSolver = new SpringSolver(
            mass: 1,
            stiffness: 2000,
            damping: 20,
            initialVelocity: 0);
    }

    public override double Ease(double progress)
    {
        var t = _springSolver.Solve(progress);
        return t;
    }
}
