//using AgentAPI.Hubs;
//using Microsoft.AspNetCore.SignalR;
//using System.IO.Ports;

//public class ButtonStatusService : BackgroundService
//{
//	private readonly string _portName = "COM5"; // portunu yaz!
//	private readonly int _baudRate = 9600;
//	private readonly IHubContext<ButtonHub> _hubContext;

//	public volatile string CurrentStatus = "BOS";
//	private string _lastSentStatus = "";

//	public ButtonStatusService(IHubContext<ButtonHub> hubContext)
//	{
//		_hubContext = hubContext;
//	}

//	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//	{
//		using (var serialPort = new SerialPort(_portName, _baudRate))
//		{
//			serialPort.Open();
//			while (!stoppingToken.IsCancellationRequested)
//			{
//				try
//				{
//					string line = serialPort.ReadLine().Trim();
//					if (line != CurrentStatus)
//					{
//						CurrentStatus = line;

						
//						await _hubContext.Clients.All.SendAsync("ButtonStatusChanged", CurrentStatus);
//					}
//				}
//				catch { }
//				await Task.Delay(50, stoppingToken);
//			}
//		}
//	}
//}
