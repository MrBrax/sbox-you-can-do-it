using Sandbox;
using System;
using System.Linq;

namespace Editor;

public static class MotivationManager
{
	static RealTimeUntil Cooldown;

	static MotivationManager()
	{
		Game.SetRandomSeed( DateTime.Now.Second );
		Cooldown = 5;
	}

	private static bool _hasMotivation => NoticeManager.All.FirstOrDefault( x => x is MotivationNotice ) != null;

	private static SoundFile sound = SoundFile.Load("sounds/baka.wav");

	[EditorEvent.FrameAttribute]
	public static void Frame()
	{
		if ( NoticeManager.All.Any( x => x.GetType().ToString() == "Editor.CodeCompileNotice" && x is NoticeWidget widget && widget.BorderColor == Theme.Red ) )
		{
			ShowBaka();
		}
		else
		{
			HideBaka();
		}
	}
	
	private static void ShowBaka()
	{
		if (_hasMotivation) return;
		EditorUtility.PlayRawSound( "sounds/baka.wav" );
		var s = new MotivationNotice();
		NoticeManager.Remove( s, 30 );
	}
	
	private static void HideBaka()
	{
		if ( _hasMotivation )
		{
			NoticeManager.Remove( NoticeManager.All.FirstOrDefault( x => x is MotivationNotice ) );
		}
	}
	
}
