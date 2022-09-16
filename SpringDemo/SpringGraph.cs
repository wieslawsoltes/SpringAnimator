using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls.Shapes;
using SpringDemo.Animation;

namespace SpringDemo;

public class SpringGraph : Polyline
{
    public static readonly StyledProperty<double> MassProperty = 
        AvaloniaProperty.Register<SpringGraph, double>("Mass");

    public static readonly StyledProperty<double> StiffnessProperty = 
        AvaloniaProperty.Register<SpringGraph, double>("Stiffness");

    public static readonly StyledProperty<double> DampingProperty = 
        AvaloniaProperty.Register<SpringGraph, double>("Damping");

    public static readonly StyledProperty<double> InitialVelocityProperty = 
        AvaloniaProperty.Register<SpringGraph, double>("InitialVelocity");

    private readonly Spring _spring;
    private readonly int _partitions;
    private bool _isDirty;

    public SpringGraph()
    {
        _spring = new Spring();
        _partitions = 1000;
        _isDirty = true;
    }

    public double Mass
    {
        get => GetValue(MassProperty);
        set => SetValue(MassProperty, value);
    }

    public double Stiffness
    {
        get => GetValue(StiffnessProperty);
        set => SetValue(StiffnessProperty, value);
    }

    public double Damping
    {
        get => GetValue(DampingProperty);
        set => SetValue(DampingProperty, value);
    }

    public double InitialVelocity
    {
        get => GetValue(InitialVelocityProperty);
        set => SetValue(InitialVelocityProperty, value);
    }

    private IEnumerable<double> LinSpace(double start, double end, int partitions)
    {
        foreach (var i in Enumerable.Range(0, partitions + 1))
        {
            var range = end - start;
            yield return i != partitions ? start + range / partitions * i : end;
        }
    }

    private void Generate()
    {
        if (_isDirty)
        {
            Points = LinSpace(0d, 1d, _partitions)
                .Select(x => new Point(x, 1d - _spring.GetSpringProgress(x)))
                .ToList();

            _isDirty = false;
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == MassProperty)
        {
            _spring.Mass = change.GetNewValue<double>();
            _isDirty = true;
            Generate();
            InvalidateVisual();
        }

        if (change.Property == StiffnessProperty)
        {
            _spring.Stiffness = change.GetNewValue<double>();
            _isDirty = true;
            Generate();
            InvalidateVisual();
        }

        if (change.Property == DampingProperty)
        {
            _spring.Damping = change.GetNewValue<double>();
            _isDirty = true;
            Generate();
            InvalidateVisual();
        }

        if (change.Property == InitialVelocityProperty)
        {
            _spring.InitialVelocity = change.GetNewValue<double>();
            _isDirty = true;
            Generate();
            InvalidateVisual();
        }
    }
}
