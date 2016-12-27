using System;
using System.Diagnostics;
using log4net;
using log4net.Config;
using Topshelf;

// Configure log4net using the .config file

[assembly: XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file
// called TestApp.exe.config in the application base
// directory (i.e. the directory containing TestApp.exe)
// The config file will be watched for changes.

namespace MiNET.Service
{
	public class MiNetService
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (MiNetServer));

		public MiNetServer _server;

		/// <summary>
		///     Starts this instance.
		/// </summary>
		public void Start()
		{
			Log.Info("Starting RaNET as user " + Environment.UserName);
			_server = new MiNetServer();
			//_server.LevelManager = new SpreadLevelManager(Environment.ProcessorCount * 4);
			_server.StartServer();
		}

		/// <summary>
		///     Stops this instance.
		/// </summary>
		public void Stop()
		{
			Log.Info("Stopping RaNET");
			_server.StopServer();
		}

		/// <summary>
		///     The programs entry point.
		/// </summary>
		/// <param name="args">The arguments.</param>
		private static void Main(string[] args)
		{
			//Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
			if (IsRunningOnMono())
			{
				var service = new MiNetService();
				service.Start();
				Console.WriteLine("RaNET running. Press <enter> to stop service.");
				Console.ReadLine();
				service.Stop();
			}
			else
			{
				HostFactory.Run(host =>
				{
					host.Service<MiNetService>(s =>
					{
						s.ConstructUsing(construct => new MiNetService());
						s.WhenStarted(service => service.Start());
						s.WhenStopped(service => service.Stop());
					});

					host.RunAsLocalService();
					host.SetDisplayName("RaNET Service");
					host.SetDescription("RaNET Minecraft Pocket Edition server.");
					host.SetServiceName("RaNET");
				});
			}
		}

		/// <summary>
		///     Determines whether is running on mono.
		/// </summary>
		/// <returns></returns>
		public static bool IsRunningOnMono()
		{
			return Type.GetType("Mono.Runtime") != null;
		}
	}
}
