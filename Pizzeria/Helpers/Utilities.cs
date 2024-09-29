using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Helpers
{
	public static class Utilities
	{
		/*public static string GenerateUniqueId()
		{
			var now = DateTime.UtcNow;
			var timestamp = now.Ticks.ToString("X8"); // Convert ticks to hexadecimal string
			var randomPart = Path.GetRandomFileName().Replace(".", string.Empty); // Generate random part

			// Combine timestamp and random part
			string uniqueId = $"{timestamp}{randomPart}";

			// Ensure 6-digit length by taking the last 6 characters
			return uniqueId.Substring(Math.Max(0, uniqueId.Length - 6));
		}*/
		public static string GenerateGuid()
		{
			return Guid.NewGuid().ToString().ToUpper();
		}
		
	}

}
