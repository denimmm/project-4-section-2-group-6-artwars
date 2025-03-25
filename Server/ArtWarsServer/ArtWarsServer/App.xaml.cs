using ArtWarsServer.Model;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Diagnostics;

namespace ArtWarsServer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public Server server { get; private set; }



	public App()
	{
		InitializeComponent();
        server = new Server();
    }


	protected override void OnStartup(StartupEventArgs e)
	{


		base.OnStartup(e);


        server.Start();
		Debug.WriteLine("Starting server");
	}

	protected override void OnExit(ExitEventArgs e)
	{
		server.Stop(); // Cleanup when app closes
		base.OnExit(e);
	}

}

