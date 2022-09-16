using System;
using System.Numerics;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Rendering.Composition;

namespace SpringDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        Border1.AttachedToVisualTree += BorderOnAttachedToVisualTree;
    }

    private void BorderOnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        var compositionVisual = ElementComposition.GetElementVisual(Border1);
        if (compositionVisual is null)
        {
            return;
        }

        var compositor = compositionVisual.Compositor;

        var easing = new SpringEasing();
        
        var animation = compositor.CreateVector3KeyFrameAnimation();
        animation.InsertKeyFrame(0f, new Vector3(-300f, 0f, 0f), easing);
        animation.InsertKeyFrame(1f, new Vector3(0f, 0f, 0f), easing);
        animation.Duration = TimeSpan.FromMilliseconds(900);
        animation.Direction = PlaybackDirection.Normal;
        animation.IterationCount = int.MaxValue;

        compositionVisual.StartAnimation("Offset", animation);
    }
}
