//using Microsoft.SemanticKernel;
//using System.ComponentModel;

//namespace AgentAPI.Plugins
//{
//	public class ButtonPlugin
//	{
//		private readonly ButtonStatusService _statusService;

//		public ButtonPlugin(ButtonStatusService statusService)
//		{
//			_statusService = statusService;
//		}

//		[KernelFunction, Description("Arduino'daki butona basılıp basılmadığını bildirir.")]
//		public string IsButtonPressed()
//		{
//			return _statusService.CurrentStatus == "BASILI" ? "Butona basıldı." : "Butona basılmadı.";
//		}
//	}
//}
