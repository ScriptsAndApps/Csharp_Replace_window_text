using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Diagnostics;

using System.Windows.Forms;
namespace Program {
	
	internal static class Program
    {
		
		[DllImport("user32.dll")]
		static extern int SetWindowText(IntPtr hWnd, string text);
		

		[DllImport("user32.dll")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		static public IntPtr GetHandleWindow(string title){return FindWindow(null, title);} 

		[DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
		extern static int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);

		
		static StringBuilder vlcText = new StringBuilder();
		static IntPtr hWnd = IntPtr.Zero;
		static Process[] processCollection;
    
    
    static string customtextfront = " Now playing: ";
    static string customtextafter = " - bla bla bla custom text ";
    static string messageOnIdleNotPlaying =  "Hi! If vlc is pauzed this text appiers! ";
    
		static void replaceNameAutomatic(){
		
    ///IntPtr hWnd = FindWindowInProcess(p, s => s.Contains("VLC m"));
			
			
		  processCollection = Process.GetProcesses();  
			foreach (Process p in processCollection)  
			{  
      //needs a chexk if its not its own hWnd did not make it yet.. just added to look if it's not the same name
				if(p.ProcessName.ToLower().Contains("vlc") && !p.ProcessName.ToLower().Contains("vlcremovebranding") ){
					hWnd = p.MainWindowHandle;
				}					
			} 
		
			if (hWnd != IntPtr.Zero) 
			{
				vlcText = new StringBuilder(200);
				GetWindowText(hWnd, vlcText, 200);
				if(
						vlcText.ToString().Contains("VLC m") && 
						!vlcText.ToString().StartsWith(" VLC m")  && 
						!vlcText.ToString().StartsWith("VLC m") && 
						!vlcText.ToString().StartsWith("- VLC m") && 
						!vlcText.ToString().Contains(customtextfront) //avoids it adding itself over and over (crashes windows)
				)
				{
					SetWindowText(hWnd, customtextfront + vlcText.ToString().Replace(" - VLC media player","") + customtextafter );
				}else{
					Thread.Sleep(10); // check if vlc is realy not changing to the song text with a delay. to be sure no errors happen
					vlcText = new StringBuilder(200);
					GetWindowText(hWnd, vlcText, 200);
                  if(
                    vlcText.ToString().Contains("VLC m") && 
                    !vlcText.ToString().StartsWith(" VLC m")  && 
                    !vlcText.ToString().StartsWith("VLC m") && 
                    !vlcText.ToString().StartsWith("- VLC m") && 
                    !vlcText.ToString().Contains(customtextfront) //avoids it adding itself over and over (crashes windows)
                )
                {
                  SetWindowText(hWnd, customtextfront + vlcText.ToString().Replace(" - VLC media player","") + customtextafter );
                }else if(
                          vlcText.ToString().StartsWith(" VLC m")  ||
                          vlcText.ToString().StartsWith("VLC m") ||
                          vlcText.ToString().StartsWith("- VLC m")
                      )
                      {
                        SetWindowText(hWnd, messageOnIdleNotPlaying);
                      }
                }
				
			}
		}
		static void Main (){
			while(true){
				Thread.Sleep(100);
				replaceNameAutomatic();	
				Thread.Sleep(100);
				Application.DoEvents();
			}
		}
		

	/*

		 //usefull functions.....................
     
     
     
	   delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
				
		[DllImport("user32", SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
	    extern static bool EnumThreadWindows(int threadId, EnumWindowsProc callback, IntPtr lParam);

		[DllImport("user32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
		






	public static IntPtr FindWindowInProcess(Process process, Func<string, bool> compareTitle)
		{
		  IntPtr windowHandle = IntPtr.Zero;

		  foreach (ProcessThread t in process.Threads)
		  {
			windowHandle = FindWindowInThread(t.Id, compareTitle);
			if (windowHandle != IntPtr.Zero)
			{
			  break;
			}
		  }

		  return windowHandle;
		}

		private static IntPtr FindWindowInThread(int threadId, Func<string, bool> compareTitle)
		{
		  IntPtr windowHandle = IntPtr.Zero;
		  EnumThreadWindows(threadId, (hWnd, lParam) =>
		  {
			StringBuilder text = new StringBuilder(200);
			GetWindowText(hWnd, text, 200);
			if (compareTitle(text.ToString()))
			{
			  windowHandle = hWnd;
			  return false;
			}
			return true;
		  }, IntPtr.Zero);

		  return windowHandle;
		}
    
		private static IntPtr FindWindowInThread(int threadId, Func<string, bool> compareTitle)
{
  IntPtr windowHandle = IntPtr.Zero;
  EnumThreadWindows(threadId, (hWnd, lParam) =>
  {
    StringBuilder text = new StringBuilder(200);
    GetWindowText(hWnd, text, 200);        
    if (compareTitle(text.ToString()))
    {
      windowHandle = hWnd;
      return false;
    }
    else
    {
      windowHandle = FindChildWindow(hWnd, compareTitle);
      if (windowHandle != IntPtr.Zero)
      {
        return false;
      }
    }
    return true;
  }, IntPtr.Zero);

  return windowHandle;
}

private static IntPtr FindChildWindow(IntPtr hWnd, Func<string, bool> compareTitle)
{
  IntPtr windowHandle = IntPtr.Zero;
  EnumChildWindows(hWnd, (hChildWnd, lParam) =>
  {
    StringBuilder text = new StringBuilder(200);
    GetWindowText(hChildWnd, text, 200);        
    if (compareTitle(text.ToString()))
    {
      windowHandle = hChildWnd;
      return false;
    }
    return true;
  }, IntPtr.Zero);

  return windowHandle;
}
		
		*/
		
		
		
		
	}
}
