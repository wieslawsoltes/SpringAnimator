using System;
using System.Numerics;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Rendering.Composition;
using SpringDemo.Animation;

namespace SpringDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        Border1.AttachedToVisualTree += BorderOnAttachedToVisualTree;

        SpringGraph.PropertyChanged += SpringGraphOnPropertyChanged;
    }

    private void SpringGraphOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (Resources["SpringEasing2"] is SpringEasing springEasing2)
        {
            springEasing2.Mass = SpringGraph.Mass;
            springEasing2.Stiffness = SpringGraph.Stiffness;
            springEasing2.Damping = SpringGraph.Damping;
            springEasing2.InitialVelocity = SpringGraph.InitialVelocity;

            Resources["SpringEasing2"] = springEasing2;
        }
    }

    private void BorderOnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        var compositionVisual = ElementComposition.GetElementVisual(Border1);
        if (compositionVisual is null)
        {
            return;
        }

        var compositor = compositionVisual.Compositor;

        var easing = new SpringEasing
        {
            Mass = 1,
            Stiffness = 2000,
            Damping = 20,
            InitialVelocity = 0
        };

        var animation = compositor.CreateVector3KeyFrameAnimation();
        animation.InsertKeyFrame(0f, new Vector3(-300f, 0f, 0f), easing);
        animation.InsertKeyFrame(1f, new Vector3(0f, 0f, 0f), easing);
        animation.Duration = TimeSpan.FromMilliseconds(900);
        animation.Direction = PlaybackDirection.Normal;
        animation.IterationCount = int.MaxValue;

        compositionVisual.StartAnimation("Offset", animation);
    }
}
