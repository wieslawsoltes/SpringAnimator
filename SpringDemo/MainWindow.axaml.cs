using System;
using System.Numerics;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Rendering.Composition;

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

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        Border.AttachedToVisualTree += BorderOnAttachedToVisualTree;
    }

    private void BorderOnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        var compositionVisual = ElementComposition.GetElementVisual(Border);
        if (compositionVisual is null)
        {
            return;
        }

        var compositor = compositionVisual.Compositor;

        var animation = compositor.CreateVector3KeyFrameAnimation();
        animation.InsertKeyFrame(0f, new Vector3(-300f, 0f, 0f));
        animation.InsertKeyFrame(1f, new Vector3(0f, 0f, 0f), new SpringEasing());
        animation.Duration = TimeSpan.FromMilliseconds(900);
        animation.Direction = PlaybackDirection.Normal;
        animation.IterationCount = int.MaxValue;

        compositionVisual.StartAnimation("Offset", animation);
    }
}
