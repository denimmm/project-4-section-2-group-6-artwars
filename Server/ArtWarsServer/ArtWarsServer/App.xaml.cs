using ArtWarsServer.Model;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ArtWarsServer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public Server server;



	public App()
	{
		InitializeComponent();
		server = new Server();
	}


	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);
		server.Start();
	}

	protected override void OnExit(ExitEventArgs e)
	{
		server.Stop(); // Cleanup when app closes
		base.OnExit(e);
	}

}

