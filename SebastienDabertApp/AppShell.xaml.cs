﻿namespace SebastienDabertApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(TrickPage), typeof(TrickPage));
        Routing.RegisterRoute(nameof(ResortDetailPage), typeof(ResortDetailPage));
    }
}